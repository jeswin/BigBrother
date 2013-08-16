using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.MDbg
{
    public class MDbgSymbolCache
    {
        public MdbgSymbol Retrieve(int symbolNumber)
        {
            if (symbolNumber < 0 || symbolNumber >= m_list.Count)
            {
                throw new ArgumentException();
            }
            return (MdbgSymbol)m_list[symbolNumber];
        }

        public void Clear()
        {
            m_list.Clear();
        }

        public int Add(MdbgSymbol symbol)               // will return symbol id.
        {
            m_list.Add(symbol);
            return m_list.Count - 1;
        }

        ArrayList m_list = new ArrayList();
    }
}
