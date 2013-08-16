using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.MDbg
{
    public class MdbgSymbol
    {
        public MdbgSymbol(int moduleNumber, string className, string method, int offset)
        {
            Debug.Assert(className != null & method != null);

            ModuleNumber = moduleNumber;
            ClassName = className;
            Method = method;
            Offset = offset;
        }

        public int ModuleNumber;
        public string ClassName;
        public string Method;
        public int Offset;
    }
}
