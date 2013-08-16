using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigBrother;
using Pdb2Xml;
using System.IO;
using System.Reflection;
using Microsoft.Samples.Debugging.CorSymbolStore;
using BigBrother.Shells;

namespace BigBrother.Reflection
{
    /// <summary>
    /// Caches symbols from the PDB File.
    /// </summary>
    public class PdbSymbolCache
    {
        IShell shell;
        Dictionary<string, SymbolData> symbols;

        public PdbSymbolCache(IEnumerable<string> assemblies, IShell shell)
        {
            this.shell = shell;

            symbols = new Dictionary<string, SymbolData>();

            foreach (var path in assemblies)
            {
                symbols[path] = ReadPDB(path);
            }
        }


        public SymbolData this[string assemblyPath]
        {
            get
            {
                return symbols[assemblyPath];
            }
        }


        private SymbolData ReadPDB(string asmPath)
        {
            //This is needed because fusion loader wouldn't find assemblies outside the Debugger's path.
            AppDomain currentDomain = AppDomain.CurrentDomain;
            string folderPath = Path.GetFullPath(Path.GetDirectoryName(asmPath));
            string assemblyName = Path.GetFileName(asmPath);
            ResolveEventHandler resolver = (sender, _args) =>
            {
                string assemblyPath = Path.Combine(folderPath, new AssemblyName(_args.Name).Name + ".dll");
                if (File.Exists(assemblyPath) == false)
                    return null;
                Assembly assembly = Assembly.ReflectionOnlyLoadFrom(assemblyPath);
                return assembly;
            };
            currentDomain.ReflectionOnlyAssemblyResolve += resolver;
            SymbolDataReader reader = new SymbolDataReader(asmPath, SymbolFormat.PDB, true);
            var result = reader.ReadSymbols();

            currentDomain.ReflectionOnlyAssemblyResolve -= resolver;

            return result;
        }
    }
}
