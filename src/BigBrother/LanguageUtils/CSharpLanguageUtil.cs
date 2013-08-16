using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.LanguageUtils
{
    public class CSharpLanguageUtil : ILanguageUtil
    {
        public string GetClrType(string type)
        {
            switch(type)
            {
                case "byte":
                    return "System.Boolean";
                case "sbyte":
                    return "System.Byte";
                case "char":
                    return "System.Char";
                case "decimal":
                    return "System.Decimal";
                case "double":
                    return "System.Double";
                case "float":
                    return "System.Single";
                case "int":
                    return "System.Int32";
                case "uint":
                    return "System.UInt32";
                case "long":
                    return "System.Int64";
                case "ulong":
                    return "System.UInt64";
                case "object":
                    return "System.Object";
                case "short":
                    return "System.Int16";
                case "ushort":
                    return "System.UInt16";
                case "string":
                    return "System.String";
                default:
                    return type;
            }
        }

        public string Language
        {
            get { return "C#"; }
        }

        /// <summary>
        /// This doesn't work reliably. TODO: See if there is another way to do this.
        /// 
        /// If it fails, define a separate variable (eg: "result") and specify this is configuration as follows.
        /// eg: { name: "ExampleApp.PersonService.GetPerson", resultField: "result" },
        /// </summary>
        /// <returns></returns>
        public string GuessResultFieldName()
        {
            return "CS$1$0000";
        }
    }
}
