using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.MDbg
{
    /// <summary>
    /// Defines generalized functions relating to source files that a class may implement to create situation-specific methods.
    /// </summary>
    public interface IMDbgSourceFile
    {
        /// <summary>
        /// Where is the file.
        /// </summary>
        /// <value>Where is the file.</value>
        string Path
        {
            get;
        }
        /// <summary>
        /// Allows for indexing into the file by line number. Index is 1-based. Highest valud index is Count property.
        /// </summary>
        /// <param name="lineNo">Which line number to get.</param>
        /// <returns>The text in the file at the requested line number.</returns>
        string this[int lineNo]
        {
            get;
        }
        /// <summary>
        /// How many lines are in the file.
        /// </summary>
        /// <value>How many lines are in the file.</value>
        int Count
        {
            get;
        }
    }
}
