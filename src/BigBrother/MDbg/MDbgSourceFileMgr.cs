using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.MDbg
{
    //////////////////////////////////////////////////////////////////////////////////
    //
    // MDbgSourceFile display support
    //
    //////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Interface for mdbg Source File Management
    /// </summary>
    public interface IMDbgSourceFileMgr
    {
        /// <summary>
        /// Clears the document cache.
        /// </summary>
        void ClearDocumentCache();
        /// <summary>
        /// Gets a source file.
        /// </summary>
        /// <param name="path">Where to get the source file from.</param>
        /// <returns>An IMDbgSourceFile from the given url.</returns>
        IMDbgSourceFile GetSourceFile(string path);
    }

    public class MDbgSourceFileMgr : IMDbgSourceFileMgr
    {
        public IMDbgSourceFile GetSourceFile(string path)
        {
            String s = String.Intern(path);
            MDbgSourceFile source = (MDbgSourceFile)m_sourceCache[s];

            if (source == null)
            {
                source = new MDbgSourceFile(s);
                m_sourceCache.Add(s, source);
            }
            return source;
        }

        public void ClearDocumentCache()
        {
            m_sourceCache.Clear();
        }

        private Hashtable m_sourceCache = new Hashtable();


        class MDbgSourceFile : IMDbgSourceFile
        {
            public MDbgSourceFile(string path)
            {
                m_path = path;
                try
                {
                    Initialize();
                }
                catch (FileNotFoundException)
                {
                    throw new Exception("Could not find source: " + m_path);
                }
            }

            public string Path
            {
                get
                {
                    return m_path;
                }
            }

            public string this[int lineNumber]
            {
                get
                {
                    if (m_lines == null)
                    {
                        Initialize();
                    }
                    if ((lineNumber < 1) || (lineNumber > m_lines.Count))
                        throw new Exception(string.Format("Could not retrieve line {0} from file {1}.",
                                                                   lineNumber, this.Path));

                    return (string)m_lines[lineNumber - 1];
                }
            }

            public int Count
            {
                get
                {
                    if (m_lines == null)
                    {
                        Initialize();
                    }
                    return m_lines.Count;
                }
            }

            protected void Initialize()
            {
                StreamReader sr = null;
                try
                {
                    // Encoding.Default doesn’t port between machines, but it's used just in case the source isn’t Unicode
                    sr = new StreamReader(m_path, System.Text.Encoding.Default, true);
                    m_lines = new ArrayList();

                    string s = sr.ReadLine();
                    while (s != null)
                    {
                        m_lines.Add(s);
                        s = sr.ReadLine();
                    }
                }
                finally
                {
                    if (sr != null)
                        sr.Close(); // free resources in advance
                }
            }

            private ArrayList m_lines;
            private string m_path;

        }
    }
}
