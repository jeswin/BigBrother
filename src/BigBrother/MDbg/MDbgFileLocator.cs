using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.MDbg
{
    /// <summary>
    /// Interface for File Location
    /// </summary>
    public interface IMDbgFileLocator
    {
        /// <summary>
        /// Gets or Sets the path. Setting a path will clear any associations.
        /// </summary>
        /// <value>the path</value>
        string Path
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a file location
        /// </summary>
        /// <param name="file">the file</param>
        /// <returns></returns>
        string GetFileLocation(string file);

        /// <summary>
        /// Forces debugger to use different source files for displaying of sources.
        /// </summary>
        /// <param name="originalName">An original name that is stored in .pdb of debugged program.</param>
        /// <param name="newName">A new name that should be used as source file name instead.</param>
        void Associate(string originalName, string newName);
    }

    public class MDbgFileLocator : IMDbgFileLocator
    {
        public MDbgFileLocator()
        {
        }

        public string Path
        {
            get
            {
                return m_srcPath;
            }
            set
            {
                foreach (string pathPart in GetPathComponents(value))
                {
                    if (!Directory.Exists(pathPart))
                    {
                        throw new Exception("path doesn't exist: '" + pathPart + "'");
                    }
                }
                m_srcPath = value;
                m_fileLocations.Clear();
            }
        }

        public string GetFileLocation(string file)
        {
            string idx = String.Intern(file);
            if (m_fileLocations.Contains(idx))
            {
                return (string)m_fileLocations[idx];
            }

            string realPath = null;

            if (File.Exists(file))
            {
                realPath = file;
            }
            else
            {
                foreach (string p in GetPathComponents(m_srcPath))
                {
                    string filePath = file;
                    do
                    {
                        realPath = System.IO.Path.Combine(p, filePath);
                        if (File.Exists(realPath))
                            goto Found;

                        int i = filePath.IndexOfAny(new char[]{
                            System.IO.Path.DirectorySeparatorChar,System.IO.Path.AltDirectorySeparatorChar});
                        if (i != -1)
                            filePath = filePath.Substring(i + 1);
                        else
                            break;
                    }
                    while (true);
                }
                realPath = null;
            }
        Found:
            m_fileLocations.Add(idx, realPath);
            return realPath;
        }

        public void Associate(string originalName, string newName)
        {
            Debug.Assert(originalName != null && originalName.Length > 0);
            Debug.Assert(newName != null && newName.Length > 0);

            string idx = String.Intern(originalName);
            if (m_fileLocations.Contains(idx))
                m_fileLocations.Remove(idx);
            m_fileLocations.Add(idx, newName);
        }

        protected string[] GetPathComponents(string path)
        {
            if (path == null)
            {
                return new string[0];
            }
            string[] strs = path.Split(new char[] { ';' });
            return strs;
        }

        private Hashtable m_fileLocations = new Hashtable();
        private string m_srcPath;
    }
}
