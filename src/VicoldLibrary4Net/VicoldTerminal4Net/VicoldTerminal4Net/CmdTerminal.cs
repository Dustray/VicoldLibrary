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
        internal static CmdTerminal CurrentInternal => ModEngineHolder.INSTANCE;

        #endregion

        internal CmdQueue _commandQueue;
        private ICmdInterpreter _interpreter;
        internal Action<string, CmdOutPutType> InternalOutputCallback;
        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="order"></param>
        /// <param name="action"></param>
        public CmdDetailEtt AddOrder(string order, Action<CmdParams> action)
        {
            //var detail = new CmdDetailEtt()
            //{
            //    Order = order,
            //    Description = "",
            //    Callback = action
            //};
            //_commandQueue.AddOrder(order, detail);
            return AddOrder(order, "", action);
        }

        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="order"></param>
        /// <param name="description"></param>
        /// <param name="action"></param>
        public CmdDetailEtt AddOrder(string order, string description, Action<CmdParams> action)
        {
            var detail = new CmdDetailEtt()
            {
                Order = order,
                Description = description,
                Callback = action
            };
            _commandQueue.AddOrder(detail);
            return detail;
        }
        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="order"></param>
        /// <param name="description"></param>
        /// <param name="action"></param>
        internal void AddOrder(CmdDetailEtt detail)
        {
            _commandQueue.AddOrder(detail);
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

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _commandQueue.Dispose();
        }

        public Task BindingInternalOutput(Action<string, CmdOutPutType> action)
        {
            InternalOutputCallback = action;
            var pool = new CmdPool().OrderCollection;
            foreach (var order in pool)
            {
                AddOrder(order);
            }
            return Task.CompletedTask;
        }
    }
}
