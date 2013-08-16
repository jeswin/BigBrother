using Microsoft.Samples.Debugging.MdbgEngine;
using BigBrother.LanguageUtils;
using BigBrother.MDbg;
using BigBrother.Reflection;
using System;
namespace BigBrother.Shells
{
    public interface IShell
    {
        IBreakpointParser BreakpointParser { get; set; }
        MDbgEngine Debugger { get; set; }
        IMDbgFileLocator FileLocator { get; set; }
        IMDbgSourceFileMgr SourceFileMgr { get; set; }
        MDbgSymbolCache SymbolCache { get; set; }
        PdbSymbolCache PdbSymbolCache { get; set; }
        MDbgProcess Process { get; set; }
        ILanguageUtil GetLanguageUtil(string language);
        MethodLocator GetMethodLocator(string assemblyPath);
        void WriteLine(string what);
        void WriteLine(string text, params object[] args);
    }
}
