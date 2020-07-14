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
        public CmdPool(Action<string> outputAction)
        {
            _orderCollection = new Dictionary<string, Action<CmdParams>>();
            InitPool(outputAction);
        }

        private void InitPool(Action<string> outputAction)
        {
            _orderCollection["help"] = (cmdParams) =>
             {
                 outputAction?.Invoke("");
             };
        }

    }
}
