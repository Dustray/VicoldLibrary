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
                    var s = CmdTerminal.CurrentX._commandQueue._orderQueue;
                    var output = new System.Text.StringBuilder("");
                    if (cmdParams.PairParams == null || cmdParams.PairParams.Count == 0)
                    {
                        var index = 0;
                        foreach (var order in s)
                        {
                            if (index != 0)
                            {
                                output.Append("\r\n");
                            }
                            output.Append(order.Key);
                            output.Append("\t");
                            output.Append(order.Value.Description);
                            index++;
                        }
                    }
                    else
                    {
                        var o = cmdParams.PairParams.First().Key;
                        var index = 0;
                        foreach (var order in s)
                        {
                            if (!order.Key.StartsWith(o))
                                continue;
                            if (index != 0)
                            {
                                output.Append("\r\n");
                            }
                            output.Append(order.Key);
                            output.Append("\t");
                            output.Append(order.Value.Description);
                            index++;
                        }
                        if (index == 0)
                        {
                            output.Append($"No command [{o}] or starting with [{o}] was found.");
                        }
                    }
                    outputAction?.Invoke(output.ToString());
                }
            });
        }

    }
}
