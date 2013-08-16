using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snoopy
{
    public class CommandLineOptions
    {
        [Option('p', "pid", HelpText = "PID of proces to debug.")]
        public int PID { get; set; }

        [Option('c', "config", HelpText = "Config file path.")]
        public string Config { get; set; }

        [Option("clrversion", HelpText = "CLR Version.")]
        public string CLRVersion { get; set; }

        [Option('e', "exec", HelpText = "Executable file name.")]
        public string Executable { get; set; }

        [Option("enumprocesses", HelpText = "List processes which can be attached to.")]
        public bool EnumProcesses { get; set; }

        [Option("assemblylaunchversion", HelpText = "Find debugger version for assembly.")]
        public string PrintDebuggerVersionForAssembly { get; set; }

        [Option("assemblyruntimeversion", HelpText = "Find runtime version for assembly.")]
        public string PrintRuntimeVersionForAssembly { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
