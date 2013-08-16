using Microsoft.Samples.Debugging.MdbgEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Serialization
{
    interface IParameterSerializer
    {
        void Serialize(string path, IEnumerable<MDbgValue> values);
    }
}
