using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VicoldTerminal4Net
{
    /// <summary>
    /// 命令队列
    /// </summary>
    internal sealed class CommandQueue
    {
        private Dictionary<string, Action<string[]>> _orderQueue;

        internal CommandQueue()
        {
            _orderQueue = new Dictionary<string, Action<string[]>>();
        }
        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="order">命令</param>
        /// <param name="action"></param>
        public void AddOrder(string order, Action<string[]> action)
        {
            if (_orderQueue.ContainsKey(order))
            {
                throw new CommandException($"已存在名称为{order}的命令。");
            }
            _orderQueue[order] = action;
        }
        /// <summary>
        /// 尝试执行命令
        /// </summary>
        /// <param name="order">命令</param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public bool TryExecuteOrder(string order,string[] ps)
        {
            if (_orderQueue.TryGetValue(order ,out var action))
            {
                if (null == action) return false;
                action.Invoke(ps);
                return true;
            }
            return false;
        }
    }
}
