using Microsoft.Samples.Debugging.MdbgEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.MDbg
{
    /// <summary>
    /// Pluggable parser object to parse breakpoint location strings.
    /// </summary>
    /// <remarks>Breakpoint location object. Returns null if the parser doesn't recognize the format. 
    /// This allows parsers to be chained together. If a parser does recognize the format, then it can do more
    /// specific argument checking and throw an exception. </remarks>
    public interface IBreakpointParser
    {
        /// <summary>
        /// Parser string describing IL-level breakpoint.
        /// </summary>
        /// <param name="args">string argument representing breakpoint location syntax to parse</param>
        /// <returns>Breakpoint location object. Returns null if the parser doesn't recognize the format. 
        /// This allows parsers to be chained together. If a parser does recognize the format, then it can do more
        /// specific argument checking and throw an exception. 
        /// </returns>
        ISequencePointResolver ParseFunctionBreakpoint(string args);
    }
}
