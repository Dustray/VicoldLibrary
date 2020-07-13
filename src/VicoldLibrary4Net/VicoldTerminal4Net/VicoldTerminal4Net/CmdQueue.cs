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
    internal sealed class CmdQueue:IDisposable
    {
        /// <summary>
        /// 命令队列
        /// </summary>
        private ConcurrentDictionary<string, CmdDetailEtt> _orderQueue;
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
        public void AddOrder(string order, CmdDetailEtt detial)
        {
            if (_orderQueue.ContainsKey(order))
            {
                throw new CmdException($"已存在名称为{order}的命令。");
            }
            _orderQueue[order] = detial;
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
            if (_orderQueue.TryGetValue(cmdParam.Order, out var action))
            {
                if (null == action)
                {
                    return false;
                }
                await Task.Run(() => action.Callback.Invoke(cmdParam));
                return true;
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
