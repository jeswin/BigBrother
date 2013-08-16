using Microsoft.Samples.Debugging.CorDebug;
using Microsoft.Samples.Debugging.MdbgEngine;
using Newtonsoft.Json;
using BigBrother.LanguageUtils;
using BigBrother.MDbg;
using BigBrother.Monitoring;
using BigBrother.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BigBrother.Shells;
using BigBrother;

namespace Snoopy
{
    public class TextShell : IShell
    {
        public MDbgSymbolCache SymbolCache { get; set; }
        public MDbgEngine Debugger { get; set; }

        public IBreakpointParser BreakpointParser { get; set; }
        public IMDbgFileLocator FileLocator { get; set; }
        public IMDbgSourceFileMgr SourceFileMgr { get; set; }
        public PdbSymbolCache PdbSymbolCache { get; set; }

        public MDbgProcess Process { get; set; }

        Configuration configuration;
        List<ILanguageUtil> languageUtils;
        Dictionary<string, MethodLocator> methodLocators;
        CommandLineOptions options;

        public TextShell(CommandLineOptions options)
        {
            this.options = options;
            ProcessArgs();
        }

        private void InitDebugging()
        {
            this.configuration = ReadConfiguration(options.Config);
            SymbolCache = new MDbgSymbolCache();
            Debugger = new MDbgEngine();
            BreakpointParser = new DefaultBreakpointParser(this);
            FileLocator = new MDbgFileLocator();
            SourceFileMgr = new MDbgSourceFileMgr();
            PdbSymbolCache = new PdbSymbolCache(configuration.assemblies.Select(a => a.path), this);
            languageUtils = new List<ILanguageUtil>(new[] { new CSharpLanguageUtil() });

            methodLocators = new Dictionary<string, MethodLocator>();
            foreach (var assembly in configuration.assemblies)
            {
                methodLocators[assembly.path] = new MethodLocator(assembly.path, assembly.language, this);
            }            
        }

        private void ProcessArgs()
        {
            if (options.EnumProcesses)
            {
                EnumProcesses();
            }
            else if (!string.IsNullOrEmpty(options.PrintDebuggerVersionForAssembly))
            {
                Console.WriteLine(MdbgVersionPolicy.GetDefaultLaunchVersion(options.PrintDebuggerVersionForAssembly));
            }
            else if (!string.IsNullOrEmpty(options.PrintRuntimeVersionForAssembly))
            {
                Console.WriteLine(MdbgVersionPolicy.GetDefaultRuntimeForFile(options.PrintRuntimeVersionForAssembly));
            }
            else
            {
                InitDebugging();
                StartProcess();
                StartMonitoring();
            }
        }


        private void StartMonitoring()
        {
            var monitor = new Monitor(this, configuration);
            monitor.Begin();
            Cleanup();
        }


        public void StartProcess()
        {
            Debugger.Options.StopOnLogMessage = false;
            if (options.PID != 0)
            {
                string clrVersion = string.IsNullOrEmpty(options.CLRVersion) ? MdbgVersionPolicy.GetDefaultAttachVersion(options.PID) : options.CLRVersion;
                Process = Debugger.Attach(options.PID, null, clrVersion);
                WriteLine("Attached to " + options.PID);
                Process.Go().WaitOne();
            }
            else if (!string.IsNullOrEmpty(options.Executable))
            {
                var clrVersion = MdbgVersionPolicy.GetDefaultLaunchVersion(options.Executable);
                Process = Debugger.Processes.CreateLocalProcess(new CorDebugger(clrVersion));
                if (Process == null)
                {
                    throw new Exception("Could not create debugging interface for runtime version " + clrVersion);
                }
                Process.DebugMode = DebugModeFlag.Debug;
                Process.CreateProcess(options.Executable, configuration.arguments != null ? configuration.arguments : "");
                Process.Go().WaitOne();
            }
            else
            {
                WriteLine(options.GetUsage());
                return;
            }

            Console.CancelKeyPress += OnAbort;
        }



        private void EnumProcesses()
        {
            Console.WriteLine("Active processes on current machine:");
            foreach (Process p in System.Diagnostics.Process.GetProcesses())
            {

                if (System.Diagnostics.Process.GetCurrentProcess().Id == p.Id)  // let's hide our process
                {
                    continue;
                }

                //list the loaded runtimes in each process, if the ClrMetaHost APIs are available
                CLRMetaHost mh = null;
                try
                {
                    mh = new CLRMetaHost();
                }
                catch (System.EntryPointNotFoundException)
                {
                    // Intentionally ignore failure to find GetCLRMetaHost().
                    // Downlevel we don't have one.
                    continue;
                }

                IEnumerable<CLRRuntimeInfo> runtimes = null;
                try
                {
                    runtimes = mh.EnumerateLoadedRuntimes(p.Id);
                }
                catch (System.ComponentModel.Win32Exception e)
                {
                    if ((e.NativeErrorCode != 0x0) &&           // The operation completed successfully.
                        (e.NativeErrorCode != 0x3f0) &&         // An attempt was made to reference a token that does not exist.
                        (e.NativeErrorCode != 0x5) &&           // Access is denied.
                        (e.NativeErrorCode != 0x57) &&          // The parameter is incorrect.
                        (e.NativeErrorCode != 0x514) &&         // Not all privileges or groups referenced are assigned to the caller.
                        (e.NativeErrorCode != 0x12))            // There are no more files.
                    {
                        // Unknown/unexpected failures should be reported to the user for diagnosis.
                        Console.WriteLine("Error retrieving loaded runtime information for PID " + p.Id
                            + ", error " + e.ErrorCode + " (" + e.NativeErrorCode + ") '" + e.Message + "'");
                    }

                    // If we failed, don't try to print out any info.
                    if ((e.NativeErrorCode != 0x0) || (runtimes == null))
                    {
                        continue;
                    }
                }
                catch (System.Runtime.InteropServices.COMException e)
                {
                    if (e.ErrorCode != (int)HResult.E_PARTIAL_COPY)  // Only part of a ReadProcessMemory or WriteProcessMemory request was completed.
                    {
                        // Unknown/unexpected failures should be reported to the user for diagnosis.
                        Console.WriteLine("Error retrieving loaded runtime information for PID " + p.Id
                            + ", error " + e.ErrorCode + "\n" + e.ToString());
                    }

                    continue;
                }

                //if there are no runtimes in the target process, don't print it out
                if (!runtimes.GetEnumerator().MoveNext())
                {
                    continue;
                }

                Console.WriteLine("(PID: " + p.Id + ") " + p.MainModule.FileName);
                foreach (CLRRuntimeInfo rti in runtimes)
                {
                    Console.WriteLine("\t" + rti.GetVersionString());
                }
            }
        }


        private Configuration ReadConfiguration(string file)
        {
            var configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(file));

            if (string.IsNullOrEmpty(configuration.appType))
            {
                configuration.appType = "web";
            }

            foreach (var assembly in configuration.assemblies)
            {
                foreach (var method in assembly.methods)
                {
                    method.assembly = assembly;
                }
            }

            return configuration;
        }


        public void WriteLine(string what)
        {
            Console.WriteLine(what);
        }


        public void WriteLine(string text, params object[] args)
        {
            Console.WriteLine(text, args);
        }


        public ILanguageUtil GetLanguageUtil(string language)
        {
            return languageUtils.Single(l => l.Language == language);
        }


        public MethodLocator GetMethodLocator(string assemblyPath)
        {
            return methodLocators[assemblyPath];
        }


        public void OnAbort(object sender, ConsoleCancelEventArgs args)
        {
            Cleanup();
        }

        private void Cleanup()
        {
            MDbgProcess active = Debugger.Processes.Active;
            active.Breakpoints.DeleteAll();

            active.CorProcess.Stop(0);
            active.Detach();
        }
    }
}
