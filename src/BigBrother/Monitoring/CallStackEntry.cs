using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Monitoring
{    
    class CallStackEntry
    {
        public string Identifier { get; private set; }
        public BreakpointMetadata BreakpointMetadata { get; set; }

        public CallStackEntry()
        {
            Identifier = GenerateIdentifier();
        }

        private string GenerateIdentifier()
        {
            Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < 32; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}
