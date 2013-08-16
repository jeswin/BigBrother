using BigBrother.LanguageUtils;
using BigBrother.Shells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Reflection
{
    public class MethodLocator
    {
        string assemblyPath;
        IShell shell;
        Dictionary<Type, List<MethodInfo>> methodsInType;
        ILanguageUtil languageUtil;

        public MethodLocator(string assemblyPath, string language, IShell shell)
        {
            this.assemblyPath = assemblyPath;
            this.methodsInType = new Dictionary<Type,List<MethodInfo>>();
            this.shell = shell;
            this.languageUtil = shell.GetLanguageUtil(language);
        }

        public MethodInfo GetMethod(Configuration.Method methodConfiguration)
        {
            var methodPath = MethodPath.Read(methodConfiguration.name);
            var type = Assembly.ReflectionOnlyLoadFrom(assemblyPath).GetType(methodPath.Type);

            if (!methodsInType.ContainsKey(type))
            {
                methodsInType[type] = new List<MethodInfo>();
                methodsInType[type].AddRange(type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static));
            }

            var matchingMethods = methodsInType[type].Where(m => m.Name == methodPath.Method).ToList();

            //Overload resolution, if matchingMethods.Count > 1
            if (matchingMethods.Count > 1)
            {
                if (string.IsNullOrEmpty(methodConfiguration.parameters))
                {
                    shell.WriteLine(string.Format("Method {0} has overloads. Specify parameters."));
                    return null;
                }
                else
                {
                    foreach (var method in matchingMethods)
                    {
                        var matches = true;
                        var parameterTypes = methodConfiguration.parameters.Split(',').Select(p => p.Trim()).ToArray();
                        var parameterInfoList = method.GetParameters();
                        for (int i = 0; i < parameterTypes.Length; i++)
                        {
                            if (parameterInfoList[i].ParameterType.FullName != languageUtil.GetClrType(parameterTypes[i]))
                            {
                                matches = false;
                                break;
                            }
                        }
                        if (matches)
                        {
                            return method;
                        }
                    }
                    return null;
                }
            }
            else if (matchingMethods.Count == 1)
            {
                return matchingMethods.Single();
            }
            else
            {
                return null;
            }
        }
    }
}
