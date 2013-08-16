using Microsoft.Samples.Debugging.CorDebug;
using Microsoft.Samples.Debugging.MdbgEngine;
using BigBrother.Shells;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Serialization
{
    class FuncEvalSerializer : IParameterSerializer
    {
        MDbgEngine debugger;
        Dictionary<string, MDbgFunction> serializers = new Dictionary<string, MDbgFunction>();
        IShell shell;
        bool useLoadFrom;

        public FuncEvalSerializer(MDbgEngine debugger, bool useLoadFrom, IShell shell)
        {
            this.debugger = debugger;
            this.shell = shell;
            this.useLoadFrom = useLoadFrom;
        }

        public void Serialize(string path, IEnumerable<MDbgValue> valuesList)
        {
            var values = valuesList.ToList();
            CorEval eval = debugger.Processes.Active.Threads.Active.CorThread.CreateEval();
            var functionName = "BigBrother.Serialization.JsonSerializer.Serialize" + (values.Count + 1);

            CorAppDomain appDomain = debugger.Processes.Active.Threads.Active.CorThread.AppDomain;

            if (serializers.Count == 0)
            {
                //Load BigBrother into the AppDomain. Then we can call BigBrother.Serialization.JsonSerializer's methods.
                if (useLoadFrom)
                {
                    eval.NewString("BigBrother.dll");
                    debugger.Processes.Active.Go().WaitOne();
                    var evalAssemblyName = eval.Result;
                    var fnLoad = debugger.Processes.Active.ResolveFunctionNameFromScope("System.Reflection.Assembly.LoadFrom", appDomain);
                    eval.CallParameterizedFunction(fnLoad.CorFunction, null, new CorValue[] { evalAssemblyName });
                    debugger.Processes.Active.Go().WaitOne();
                }
                else
                {
                    eval.NewString("BigBrother");
                    debugger.Processes.Active.Go().WaitOne();
                    var evalAssemblyName = eval.Result;
                    var fnLoad = debugger.Processes.Active.ResolveFunctionNameFromScope("System.Reflection.Assembly.Load", appDomain);
                    eval.CallParameterizedFunction(fnLoad.CorFunction, null, new CorValue[] { evalAssemblyName });
                    debugger.Processes.Active.Go().WaitOne();
                }
            }
                
            if (!serializers.ContainsKey(functionName))
            {                
                serializers[functionName] = debugger.Processes.Active.ResolveFunctionNameFromScope(functionName, appDomain);
            }

            eval.NewString(path);
            debugger.Processes.Active.Go().WaitOne();
            CorValue fileName = (debugger.Processes.Active.StopReason as EvalCompleteStopReason).Eval.Result;
            var corValues = new List<CorValue>();
            corValues.Add(fileName);
            corValues.AddRange(values.Select(v => v.CorValue));
            eval.CallParameterizedFunction(serializers[functionName].CorFunction, corValues.Select(v => v.ExactType).ToArray(), corValues.ToArray());
            debugger.Processes.Active.Go().WaitOne();

            if (debugger.Processes.Active.StopReason is EvalExceptionStopReason)
            {
                var stopReason = (EvalExceptionStopReason)debugger.Processes.Active.StopReason;
                string message = new MDbgValue(debugger.Processes.Active, stopReason.Eval.Result).GetStringValue(true);
                shell.WriteLine(message);
            }

        }
    }
}
