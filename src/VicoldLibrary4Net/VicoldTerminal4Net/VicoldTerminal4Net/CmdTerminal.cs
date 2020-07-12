using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VicoldTerminal4Net
{
    public class CmdTerminal : ICommander
    {
        #region 单例模式

        /// <summary>
        /// 模块引擎
        /// </summary>
        private CmdTerminal()
        {
            _commandQueue = new CmdQueue();
            _interpreter = new CmdInterpreter();
        }

        /// <summary>
        /// 模块引擎拥有者
        /// </summary>
        private static class ModEngineHolder
        {
            internal static readonly CmdTerminal INSTANCE = new CmdTerminal();
        }

        /// <summary>
        /// 模块引擎
        /// <para>当前的</para>
        /// </summary>
        public static ICommander Current => ModEngineHolder.INSTANCE;

        #endregion

        private CmdQueue _commandQueue;
        private ICmdInterpreter _interpreter;
        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="order"></param>
        /// <param name="action"></param>
        public void AddOrder(string order, Action<CmdParams> action)
        {
            _commandQueue.AddOrder(order, action);
        }
        /// <summary>
        /// 尝试执行指定命令
        /// </summary>
        /// <param name="orderLine">完整命令</param>
        /// <param name="customerContent">自定义实体</param>
        /// <returns>执行结果：是否成功</returns>
        public async Task<bool> TryExecuteOrder(string orderLine, object customerContent = null)
        {
            var para = _interpreter.Execute(orderLine);
            para.CustomerContent = customerContent;
            var result = await _commandQueue.TryExecuteOrder(para);
            return result;
        }

    }
}
