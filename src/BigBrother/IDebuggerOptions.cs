using System;
namespace BigBrother
{
    public interface IDebuggerOptions
    {
        string CLRVersion { get; set; }
        string Config { get; set; }
        bool EnumProcesses { get; set; }
        string Executable { get; set; }
        string Export { get; set; }
        string GetUsage();
        int PID { get; set; }
        string PrintDebuggerVersionForAssembly { get; set; }
        string PrintRuntimeVersionForAssembly { get; set; }
    }
}
