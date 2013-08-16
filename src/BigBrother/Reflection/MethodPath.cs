using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Reflection
{
    class MethodPath
    {
        public string Type { get; set; }
        public string Method { get; set; }

        public static MethodPath Read(string methodPath)
        {
            if (methodPath.Contains("::"))
            {
                var methodPathArray = methodPath.Split(new[] { "::" }, StringSplitOptions.None);
                return new MethodPath { Method = methodPathArray[1], Type = methodPathArray[0] };
            }
            else
            {
                var methodPathArray = methodPath.Split('.');
                var typeName = string.Join(".", methodPathArray.Take(methodPathArray.Length - 1));
                var methodName = methodPathArray.Last();
                return new MethodPath { Method = methodName, Type = typeName };
            }
        }
    }
}
