using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VicoldTerminal4Net
{
    public class CommandTerminal
    {
        #region 单例模式

        /// <summary>
        /// 模块引擎
        /// </summary>
        private CommandTerminal()
        {
            _commandQueue = new CommandQueue();
        }

        /// <summary>
        /// 模块引擎拥有者
        /// </summary>
        private static class ModEngineHolder
        {
            internal static readonly CommandTerminal INSTANCE = new CommandTerminal();
        }

        /// <summary>
        /// 模块引擎
        /// <para>当前的</para>
        /// </summary>
        internal static CommandTerminal Current => ModEngineHolder.INSTANCE;

        #endregion

        private CommandQueue _commandQueue;
        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="order"></param>
        /// <param name="action"></param>
        public void AddOrder(string order, Action<string[]> action)
        {
            _commandQueue.AddOrder( order, action);
        }
        /// <summary>
        /// 尝试执行命令
        /// </summary>
        /// <param name="order"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public bool TryExecuteOrder(string order, string[] ps)
        {
            return _commandQueue.TryExecuteOrder(order, ps);
        }
    }
}
