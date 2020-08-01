using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VicoldTerminal4Net
{
    /// <summary>
    /// 命令队列
    /// </summary>
    internal sealed class CmdQueue : IDisposable
    {
        /// <summary>
        /// 命令队列
        /// </summary>
        internal ConcurrentDictionary<string, CmdDetailEtt> _orderQueue { get; private set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        internal CmdQueue()
        {
            _orderQueue = new ConcurrentDictionary<string, CmdDetailEtt>();
        }
        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="order">命令</param>
        /// <param name="action">执行方法</param>
        public void AddOrder(CmdDetailEtt detial)
        {
            if (_orderQueue.ContainsKey(detial.Order))
            {
                throw new CmdException($"已存在名称为{detial.Order}的命令。");
            }
            _orderQueue[detial.Order] = detial;
        }


        /// <summary> 
        /// 尝试执行命令
        /// </summary>
        /// <param name="cmdParam">命令参数实体</param>
        /// <returns>执行结果：是否成功</returns>
        public async Task<bool> TryExecuteOrder(CmdParams cmdParam)
        {
            if (cmdParam == null)
            {
                throw new CmdException($"命令行格式错误：{cmdParam.OrderLine}");
            }
            if (cmdParam.Order == null)
            {
                throw new CmdException($"找不到命令头：{cmdParam.OrderLine}");
            }
            if (_orderQueue.TryGetValue(cmdParam.Order, out var cmdDetial))
            {
                if (cmdDetial == null || cmdDetial.Callback == null)
                {
                    CmdTerminal.CurrentInternal.InternalOutputCallback?.Invoke("命令获取失败，使用help查看所有命令。", CmdOutPutType.Error);
                    return false;
                }
                if (!CheckMode(cmdDetial.CmdType))
                {
                    CmdTerminal.CurrentInternal.InternalOutputCallback?.Invoke($"命令[cmdParam.Order]执行失败，不在可执行范围内。", CmdOutPutType.Error);
                    return false;
                }
                var newBackParams = new List<KeyValuePair<string, string>>();
                //newBackParam = 
                //foreach (var p in cmdDetial.ParamNames)
                //{
                //    newBackParams.Add(new KeyValuePair<string, string>(p.Key, null));
                //}
                var fakeParam = cmdDetial.ParamNames.ToArray();
                var index = 0;
                foreach (var pPair in cmdParam.PairParams)
                {
                    if (pPair.Key == null)
                    {
                        if (index >= cmdDetial.ParamNames.Count)
                        {
                            CmdTerminal.CurrentInternal.InternalOutputCallback?.Invoke("参数过多，使用help -o [command]查看命令参数说明。", CmdOutPutType.Error);
                            return false;
                        }
                        var key = fakeParam[index].Key;

                        var newBPCount = newBackParams.Count(v => v.Key == key);
                        if (newBPCount > 0)
                        {
                            CmdTerminal.CurrentInternal.InternalOutputCallback?.Invoke("参数重复赋值，使用help -o [command]查看命令参数说明。", CmdOutPutType.Error);
                            return false;
                        }
                        newBackParams.Add(new KeyValuePair<string, string>(key, pPair.Value));
                    }
                    else if (pPair.Value == null)
                    {
                        var pCount = cmdDetial.ParamNames.Count(v => v.Key == pPair.Key);
                        if (pCount == 0)
                        {
                            CmdTerminal.CurrentInternal.InternalOutputCallback?.Invoke($"命令【{cmdDetial.Order}】没有参数【{pPair.Key}】，使用help -o [command]查看命令参数说明。", CmdOutPutType.Error);
                            return false;
                        }
                        newBackParams.Add(new KeyValuePair<string, string>(pPair.Key, null));
                    }
                    else
                    {
                        var newBPCount = newBackParams.Count(v => v.Key == pPair.Key);
                        if (newBPCount > 0)
                        {
                            CmdTerminal.CurrentInternal.InternalOutputCallback?.Invoke("参数重复赋值，使用help -o [command]查看命令参数说明。", CmdOutPutType.Error);
                            return false;
                        }
                        var pCount = cmdDetial.ParamNames.Count(v => v.Key == pPair.Key);
                        if (pCount == 0)
                        {
                            CmdTerminal.CurrentInternal.InternalOutputCallback?.Invoke($"命令【{cmdDetial.Order}】没有参数【{pPair.Key}】，使用help -o [command]查看命令参数说明。", CmdOutPutType.Error);
                            return false;
                        }
                        newBackParams.Add(new KeyValuePair<string, string>(pPair.Key, pPair.Value));
                    }
                    index++;
                }
                cmdParam.PairParams = newBackParams;
                await Task.Run(() => cmdDetial.Callback.Invoke(cmdParam));
                return true;
            }
            CmdTerminal.CurrentInternal.InternalOutputCallback?.Invoke("未找到命令，使用help查看所有命令。", CmdOutPutType.Error);
            return false;
        }

        private bool CheckMode(ExecutableType executableType)
        {
            var isDebug = false;
#if DEBUG
            isDebug = true;
#endif
            switch (executableType)
            {
                case ExecutableType.All:
                    return true;
                case ExecutableType.Debug:
                    return isDebug;
                case ExecutableType.Release:
                    return !isDebug;
                case ExecutableType.Admin:
                    return CmdTerminal.CurrentInternal.IsAdminMode;
                case ExecutableType.Debug & ExecutableType.Admin:
                    return isDebug & CmdTerminal.CurrentInternal.IsAdminMode;
                case ExecutableType.Release & ExecutableType.Admin:
                    return !isDebug & CmdTerminal.CurrentInternal.IsAdminMode;
            }
            return false;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _orderQueue.Clear();
            _orderQueue = null;
        }
    }
}
