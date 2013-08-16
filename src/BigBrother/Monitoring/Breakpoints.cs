using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Monitoring
{
    public enum BreakpointReason
    {
        MethodEntry,
        MethodExit
    }

    class BreakpointDetails
    {
        public Configuration.Method Method { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public int HitCount { get; set; }
    }

    class BreakpointMetadata
    {
        public BreakpointReason Reason { get; set; }
        public BreakpointDetails Details { get; set; }
    }
}
