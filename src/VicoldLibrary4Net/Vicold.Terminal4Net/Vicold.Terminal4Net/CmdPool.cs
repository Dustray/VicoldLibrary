using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vicold.Terminal4Net
{
    internal class CmdPool
    {
        public List<CmdDetailEtt> OrderCollection { get; private set; }
        public CmdPool()
        {
            OrderCollection = new List<CmdDetailEtt>();
            InitPool();
        }

        private void InitPool()
        {
            OrderCollection.Add(new CmdDetailEtt()
            {
                Order = "help",
                Description = "帮助，当前命令列表及命令描述。",
                Callback = (cmdParams) =>
                {
                    var s = CmdTerminal.CurrentInternal._commandQueue._orderQueue;
                    CmdOutPutType cmdOutPutType = CmdOutPutType.Info;
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
                        var o = cmdParams.PairParams.First();
                        if (o.Key != "-o")
                        {
                            return;
                        }
                        var index = 0;
                        foreach (var order in s)
                        {
                            if (!order.Key.Contains(o.Value))
                                continue;
                            if (index != 0)
                            {
                                output.Append("\r\n");
                            }
                            output.Append(order.Key);
                            output.Append("\t");
                            output.Append(order.Value.Description);
                            if (order.Value.ParamNames.Count > 0)
                            {
                                foreach (var param in order.Value.ParamNames)
                                {
                                    output.Append("\r\n");
                                    output.Append($"    {param.Key}\t{param.Value}");
                                }
                            }
                            index++;
                        }
                        //if (index == 0)
                        //{
                        //output.Append($"No command [{o.Key}] or starting with [{o.Key}] was found.");
                        //cmdOutPutType = CmdOutPutType.Error;
                        //}
                    }
                    CmdTerminal.CurrentInternal.InternalOutputCallback?.Invoke(output.ToString(), cmdOutPutType);
                }
            }.AddParam("o", "Displays all commands containing input string and their arguments."));

            OrderCollection.Add(new CmdDetailEtt()
            {
                Order = "admin",
                Description = "获取当前命令执行模式，当前命令列表及命令描述。",
                Callback = (cmdParams) =>
                {
                    foreach (var param in cmdParams.PairParams)
                    {
                        if (param.Key == "-state")
                        {
                            var mode = CmdTerminal.CurrentInternal.IsAdminMode ? "Admin" : "Normal";
                            var msg = $"Current is {mode} Mode.";
                            CmdTerminal.CurrentInternal.InternalOutputCallback?.Invoke(msg, CmdOutPutType.Info);
                        }

                        if (param.Key == "-active")
                        {
                            if (CmdTerminal.CurrentInternal.AdminPwd == param.Value)
                            {
                                CmdTerminal.CurrentInternal.IsAdminModeActive = true;
                                var msg = $"Admin mode has been activated.";
                                CmdTerminal.CurrentInternal.InternalOutputCallback?.Invoke(msg, CmdOutPutType.Info);
                            }
                            else
                            {
                                var msg = $"Password Error.";
                                CmdTerminal.CurrentInternal.InternalOutputCallback?.Invoke(msg, CmdOutPutType.Error);
                            }
                        }

                        if (param.Key == "-unactive")
                        {
                            CmdTerminal.CurrentInternal.IsAdminModeActive = false;
                            var msg = $"Admin mode has been unactivated.";
                            CmdTerminal.CurrentInternal.InternalOutputCallback?.Invoke(msg, CmdOutPutType.Info);
                        }
                    }
                }
            }.AddParam("state", "查看当前状态")
            .AddParam("active", "激活Admin模式")
            .AddParam("unactive", "反激活Admin模式"));
        }
    }
}
