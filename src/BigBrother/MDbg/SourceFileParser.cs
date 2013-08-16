using Microsoft.Samples.Debugging.CorDebug.NativeApi;
using Microsoft.Samples.Debugging.MdbgEngine;
using BigBrother.Shells;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.MDbg
{
    public static class SourceFileParser
    {
        public static string GetCurrentLocationInSource(MDbgEngine debugger, IShell shell)
        {
            if (!debugger.Processes.Active.Threads.HaveActive)
            {
                return "";                                     // we won't try to show current location
            }

            MDbgThread thr = debugger.Processes.Active.Threads.Active;

            MDbgSourcePosition pos = thr.CurrentSourcePosition;
            if (pos == null)
            {
                MDbgFrame f = thr.CurrentFrame;
                if (f.IsManaged)
                {
                    CorDebugMappingResult mappingResult;
                    uint ip;
                    f.CorFrame.GetIP(out ip, out mappingResult);
                    string s = "IP: " + ip + " @ " + f.Function.FullName + " - " + mappingResult;
                    return s;
                }
                else
                {
                    return "<Located in native code.>";
                }
            }
            else
            {
                string fileLoc = shell.FileLocator.GetFileLocation(pos.Path);
                if (fileLoc == null)
                {
                    // Using the full path makes debugging output inconsistant during automated test runs.
                    // For testing purposes we'll get rid of them.
                    //CommandBase.WriteOutput("located at line "+pos.Line + " in "+ pos.Path);
                    return "located at line " + pos.Line + " in " + System.IO.Path.GetFileName(pos.Path);
                }
                else
                {
                    IMDbgSourceFile file = shell.SourceFileMgr.GetSourceFile(fileLoc);
                    string prefixStr = pos.Line.ToString(CultureInfo.InvariantCulture) + ":";

                    if (pos.Line < 1 || pos.Line > file.Count)
                    {
                        shell.WriteLine("located at line " + pos.Line + " in " + pos.Path);
                        throw new Exception(string.Format("Could not display current location; file {0} doesn't have line {1}.",
                                                                   file.Path, pos.Line));
                    }
                    Debug.Assert((pos.Line > 0) && (pos.Line <= file.Count));
                    return file[pos.Line];
                }
            }
        }
    }
}
