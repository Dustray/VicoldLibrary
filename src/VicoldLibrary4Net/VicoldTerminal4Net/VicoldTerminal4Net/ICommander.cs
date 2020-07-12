using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VicoldTerminal4Net
{
    /// <summary>
    /// 命令工具接口
    /// </summary>
    public interface ICommander
    {
        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="order">命令</param>
        /// <param name="action">执行方法</param>
        void AddOrder(string order, Action<CmdParams> action);
        /// <summary>
        /// 尝试执行指定命令
        /// </summary>
        /// <param name="orderLine">完整命令</param>
        /// <param name="customerContent">自定义实体</param>
        /// <returns>执行结果：是否成功</returns>
        Task<bool> TryExecuteOrder(string orderLine, object customerContent = null);
    }
}
