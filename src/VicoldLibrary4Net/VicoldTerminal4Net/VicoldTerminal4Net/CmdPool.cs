using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VicoldTerminal4Net
{
    internal class CmdPool
    {
        public List<CmdDetailEtt> OrderCollection { get; private set; }
        public CmdPool(Action<string> outputAction)
        {
            OrderCollection = new List<CmdDetailEtt>();
            InitPool(outputAction);
        }

        private void InitPool(Action<string> outputAction)
        {
            OrderCollection.Add(new CmdDetailEtt()
            {
                Order = "help",
                Description = "帮助，当前命令列表及命令描述。",
                Callback = (cmdParams) =>
                {
                    var output = new System.Text.StringBuilder("");
                    var s = CmdTerminal.CurrentX._commandQueue._orderQueue;
                    foreach (var order in s)
                    {
                        output.Append(order.Key);
                        output.Append("\t");
                        output.Append(order.Value.Description);
                        output.Append("\r\n");
                    }
                    outputAction?.Invoke(output.ToString());
                }
            });
        }

    }
}
