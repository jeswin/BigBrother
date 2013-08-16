using Microsoft.Samples.Debugging.CorDebug;
using Microsoft.Samples.Debugging.CorDebug.NativeApi;
using Microsoft.Samples.Debugging.CorMetadata;
using Microsoft.Samples.Debugging.CorSymbolStore;
using Microsoft.Samples.Debugging.MdbgEngine;
using Pdb2Xml;
using BigBrother.LanguageUtils;
using BigBrother.Reflection;
using BigBrother.Serialization;
using BigBrother.Shells;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace BigBrother.Monitoring
{
    public class Monitor
    {
        IShell shell;
        Configuration configuration;
        Dictionary<MDbgBreakpoint, BreakpointMetadata> breakpointMetadataList;
        IParameterSerializer parameterSerializer;

        public Monitor(IShell shell, Configuration configuration)
        {
            this.shell = shell;
            this.configuration = configuration;
            this.breakpointMetadataList = new Dictionary<MDbgBreakpoint, BreakpointMetadata>();
            this.parameterSerializer = new FuncEvalSerializer(shell.Debugger, configuration.appType != "web", shell);
        }

        public void Begin()
        {
            CreateBreakpoints();
            shell.WriteLine("Ctrl-C to exit.");
                
            //Don't start if none of the methods resolved.
            if (breakpointMetadataList.Count > 0)
                WaitForBreakpointHit(new Stack<CallStackEntry>());
        }


        private void WaitForBreakpointHit(Stack<CallStackEntry> bpStack)
        {
            while (true)
            {
                try
                {
                    shell.Debugger.Processes.Active.Go().WaitOne();

                    if (shell.Debugger.Processes.Active.StopReason is BreakpointHitStopReason)
                    {
                        var bp = ((BreakpointHitStopReason)shell.Debugger.Processes.Active.StopReason).Breakpoint;
                        var breakpointMetadata = breakpointMetadataList[bp];
                        var languageUtil = shell.GetLanguageUtil(breakpointMetadata.Details.Method.assembly.language);

                        CallStackEntry stackItem;
                        switch (breakpointMetadata.Reason)
                        {
                            case BreakpointReason.MethodEntry:
                                stackItem = new CallStackEntry { BreakpointMetadata = breakpointMetadata };
                                bpStack.Push(stackItem);
                                breakpointMetadata.Details.HitCount++;
                                SerializeArguments(stackItem);
                                break;

                            case BreakpointReason.MethodExit:
                                stackItem = bpStack.Pop();
                                IEnumerable<MDbgValue> locals = GetActiveLocalVariables();
                                string resultFieldName = string.IsNullOrEmpty(breakpointMetadata.Details.Method.resultField) ? languageUtil.GuessResultFieldName() : breakpointMetadata.Details.Method.resultField;
                                var result = locals.Where(l => l.Name == resultFieldName).FirstOrDefault();
                                if (result != null)
                                    SerializeReturnValue(result, stackItem);

                                //Write out the method info
                                if (breakpointMetadata.Details.HitCount == breakpointMetadata.Details.Method.maxCaptures)
                                {
                                    var removables = breakpointMetadataList.Where(p => p.Value.Details.Method == breakpointMetadata.Details.Method).ToList();
                                    foreach (var r in removables)
                                    {
                                        r.Key.Delete();
                                        breakpointMetadataList.Remove(r.Key);

                                        //No more breakpoints left. Exit.
                                        if (breakpointMetadataList.Count == 0)
                                        {
                                            shell.WriteLine("Finished capturing. Debugger can now detach.");
                                            return;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
                catch(Exception ex)
                {
                    shell.WriteLine(ex.ToString());
                    return;
                }
            }
        }


        private void CreateBreakpoints()
        {
            foreach (var assembly in configuration.assemblies)
            {
                var assemblySymbols = shell.PdbSymbolCache[assembly.path];
                var methodLocator = shell.GetMethodLocator(assembly.path);
                
                foreach (var methodConfiguration in assembly.methods)
                {
                    var methodInfo = methodLocator.GetMethod(methodConfiguration);
                    var methodPath = MethodPath.Read(methodConfiguration.name);

                    if (methodInfo != null)
                    {
                        var methodSymbols = assemblySymbols.methods.Where(s => MethodPath.Read(s.name).Type == methodPath.Type
                            && MethodPath.Read(s.name).Method == methodPath.Method && Convert.ToInt32(s.token, 16) == methodInfo.MetadataToken).Single();

                        var bpDetails = new BreakpointDetails { Method = methodConfiguration, MethodInfo = methodInfo };

                        //Create method entry break point.                    
                        var entrySequencePoint = methodSymbols.sequencePoints.First(p => !p.hidden);
                        var methodEntryFile = assemblySymbols.sourceFiles.Where(s => s.id == entrySequencePoint.sourceId).Single();
                        var bpEntry = CreateBreakpointAtPosition(methodEntryFile.url + ":" + entrySequencePoint.startRow);
                        var bpEntryInfo = new BreakpointMetadata { Reason = BreakpointReason.MethodEntry, Details = bpDetails };
                        breakpointMetadataList[bpEntry] = bpEntryInfo;
                        shell.WriteLine(bpEntry.ToString());
                        
                        //Create method exit break point.                    
                        var exitSequencePoint = methodSymbols.sequencePoints.Last();
                        var methodExitFile = assemblySymbols.sourceFiles.Where(s => s.id == exitSequencePoint.sourceId).Single();
                        var bpExit = CreateBreakpointAtPosition(methodExitFile.url + ":" + exitSequencePoint.startRow);
                        var bpExitInfo = new BreakpointMetadata { Reason = BreakpointReason.MethodExit, Details = bpDetails };
                        breakpointMetadataList[bpExit] = bpExitInfo;
                        shell.WriteLine(bpExit.ToString());

                        shell.WriteLine("Monitoring {0}", methodConfiguration.name);
                    }
                    else
                    {
                        shell.WriteLine("WARNING: Unresolved {0}", methodConfiguration.name);
                    }
                }
            }
        }


        private MDbgBreakpoint CreateBreakpointAtPosition(string location)
        {
            ISequencePointResolver bploc = shell.BreakpointParser.ParseFunctionBreakpoint(location);
            if (bploc == null)
            {
                throw new Exception("Invalid breakpoint syntax.");
            }
            MDbgBreakpoint bpnew = shell.Debugger.Processes.Active.Breakpoints.CreateBreakpoint(bploc);
            return bpnew;
        }


        private IEnumerable<MDbgValue> GetActiveLocalVariables()
        {
            MDbgFrame frame = shell.Debugger.Processes.Active.Threads.Active.CurrentFrame;

            MDbgFunction f = frame.Function;

            var vars = new List<MDbgValue>();
            var vals = f.GetActiveLocalVars(frame);
            if (vals != null)
            {
                vars.AddRange(vals);
            }
            return vars;
        }


        private IEnumerable<MDbgValue> GetArguments()
        {
            MDbgFrame frame = shell.Debugger.Processes.Active.Threads.Active.CurrentFrame;

            MDbgFunction f = frame.Function;

            var vars = new List<MDbgValue>();
            var vals = f.GetArguments(frame);
            if (vals != null)
            {
                vars.AddRange(vals.Where(v => v.Name != "this"));
            }

            return vars;
        }


        private void SerializeArguments(CallStackEntry callStackEntry)
        {
            var methodInfo = callStackEntry.BreakpointMetadata.Details.MethodInfo;
            shell.WriteLine("Captured arguments for {0}", callStackEntry.BreakpointMetadata.Details.Method.name);
            var path = Path.Combine(GetWorkingDirectory(), string.Format("{0}.{1}-{2}-{3}-arguments.txt", methodInfo.DeclaringType.FullName, methodInfo.Name, methodInfo.MetadataToken, callStackEntry.Identifier));
            parameterSerializer.Serialize(path, GetArguments());
        }

        
        private void SerializeReturnValue(Microsoft.Samples.Debugging.MdbgEngine.MDbgValue value, CallStackEntry callStackEntry)
        {
            var methodInfo = callStackEntry.BreakpointMetadata.Details.MethodInfo;
            shell.WriteLine("Captured return value for {0}", methodInfo.Name);
            var path = Path.Combine(GetWorkingDirectory(), string.Format("{0}.{1}-{2}-{3}-returnvalue.txt", methodInfo.DeclaringType.FullName, methodInfo.Name, methodInfo.MetadataToken, callStackEntry.Identifier));
            parameterSerializer.Serialize(path, new MDbgValue[] { value });
        }


        private string GetWorkingDirectory()
        {
            return Path.GetFullPath(configuration.workingDirectory);
        }
    }
}
