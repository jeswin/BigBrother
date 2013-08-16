using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother
{
    public class Configuration
    {
        public class Method
        {
            public string name { get; set; }
            public string resultField { get; set; }
            public string parameters { get; set; }
            public AssemblyElement assembly { get; set; }

            public int _maxCaptures = 1;
            public int maxCaptures { get { return _maxCaptures; } set { _maxCaptures = value; } }
        }

        public class AssemblyElement
        {
            public string language { get; set; }
            public string path { get; set; }

            List<Method> _methods = new List<Method>();
            public List<Method> methods { get { return _methods; } }

            public AssemblyElement()
            {
                this.language = "C#";
            }
        }

        public string appType { get; set; }
        public List<AssemblyElement> assemblies = new List<AssemblyElement>();
        public string workingDirectory { get; set; }
        public string arguments { get; set; }
    }
}
