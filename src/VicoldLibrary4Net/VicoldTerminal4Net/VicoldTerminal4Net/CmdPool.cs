using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VicoldTerminal4Net
{
    internal class CmdPool
    {
        public Dictionary<string, Action<CmdParams>> _orderCollection;
        public CmdPool()
        {
            _orderCollection = new Dictionary<string, Action<CmdParams>>();
            InitPool();
        }

        private void InitPool()
        {
            _orderCollection["help"] = (cmdParams) =>
             {



             };
        }

    }
}
