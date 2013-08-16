using Microsoft.Samples.Debugging.MdbgEngine;
using BigBrother.Shells;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BigBrother.MDbg
{
    // Default breakpoint parser for the MDbg shell. 
    // We don't expose this class publically, but extensions can grab the interface pointer from the Breakpoint Collection's parser property.
    public class DefaultBreakpointParser : IBreakpointParser
    {
        IShell shell;

        public DefaultBreakpointParser(IShell shell)
        {
            this.shell = shell;
        }

        // Parse a function breakpoint.
        ISequencePointResolver IBreakpointParser.ParseFunctionBreakpoint(string arguments)
        {
            Regex r;
            Match m;
            // maybe it's in the form:
            // "b ~number"
            r = new Regex(@"^~(\d+)$");
            m = r.Match(arguments);
            string symNum = m.Groups[1].Value;
            if (symNum.Length > 0)
            {
                int intSymNum = Int32.Parse(symNum, CultureInfo.CurrentUICulture);
                MdbgSymbol symbol = shell.SymbolCache.Retrieve(intSymNum);

                return new BreakpointFunctionLocation(
                    string.Format(CultureInfo.InvariantCulture, ":{0}", symbol.ModuleNumber),
                    symbol.ClassName,
                    symbol.Method,
                    symbol.Offset);
            }

            // maybe it's in the form:
            // "b Mdbg.cs:34"
            r = new Regex(@"^(\S+:)?(\d+)$");
            m = r.Match(arguments);
            string fname = m.Groups[1].Value;
            string lineNo = m.Groups[2].Value;
            int intLineNo = 0;

            if (lineNo.Length > 0)
            {
                if (fname.Length > 0)
                {
                    fname = fname.Substring(0, fname.Length - 1);
                }
                else
                {
                    MDbgSourcePosition pos = null;

                    MDbgThread thr = shell.Debugger.Processes.Active.Threads.Active;
                    if (thr != null)
                    {
                        pos = thr.CurrentSourcePosition;
                    }

                    if (pos == null)
                    {
                        throw new Exception("Cannot determine current file");
                    }

                    fname = pos.Path;
                }
                intLineNo = Int32.Parse(lineNo, CultureInfo.CurrentUICulture);

                return new BreakpointLineNumberLocation(fname, intLineNo);
            }

            // now, to be valid, it must be in the form:
            //    "b mdbg!Mdbg.Main+3"
            //    
            // Note that this case must be checked after the source-file case above because 
            // we want to assume that a number by itself is a source line, not a method name.
            // This is the most general form, so check this case last. (Eg, "Mdbg.cs:34" could
            // match as Class='MDbg', Method = 'cs:34'. )
            //
            // The underlying metadata is extremely flexible and allows almost anything to be
            // in a method name, including spaces. Both C#, VB and MDbg's parsing are more restrictive.
            // Note we allow most characters in class and method names, except those we are using for separators
            // (+, ., :), <> since those are typically used to represent generics which we don't
            // support, and spaces since those are usually command syntax errors.  
            // We exclude '*' for sanity reasons. 
            // 
            // Other caveats:
            // - we must allow periods in the method name for methods like ".ctor". 
            // - be sure to allow $ character in the method and class names. Some compilers
            // like to use this in function names.
            // - Classes can't start with a number, but can include and end with numbers.
            // 
            // Ideally we'd have a quoting mechanism and a more flexible parsing system to 
            // handle generics, method overloads, etc. across all of MDbg.  At least we have the 'x' 
            // command and ~ shortcuts as a work-around.

            r = new Regex(@"^" +
                @"([^\!]+\!)?" + // optional module
                @"((?:[^.*+:<> ]+\.)*)" +  // optional class
                @"([^*+:<>\d ][^*+:<> ]*)" +  // method
                @"(\+\d+)?" +  // optional offset
                @"$");

            m = r.Match(arguments);
            string module = m.Groups[1].Value;
            string className = m.Groups[2].Value;
            string method = m.Groups[3].Value;
            string offset = m.Groups[4].Value;
            int intOffset = 0;

            if (method.Length > 0)
            {
                if (module.Length > 0)
                {
                    module = module.Substring(0, module.Length - 1);
                }

                if (className.Length > 0)
                {
                    // The class/module separator character is captured as part of className.
                    // Chop it off to get just the classname.
                    className = className.Substring(0, className.Length - 1);
                }

                if (offset.Length > 0)
                {
                    intOffset = Int32.Parse(offset.Substring(1), CultureInfo.CurrentUICulture);
                }

                return new BreakpointFunctionLocation(module, className, method, intOffset);
            }

            // We don't recognize the syntax. Return null. If the parser is chained, it gives 
            // our parent a chance to handle it.
            return null;

        } // end function ParseFunctionBreakpoint
    } // end class DefaultBreakpointParser
}
