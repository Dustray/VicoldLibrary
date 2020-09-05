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
    public interface ICommander : IDisposable
    {
        /// <summary>
        /// 开启或关闭管理员模式（默认关闭）
        /// </summary>
        /// <param name="isActive">是否激活</param>
        void SetAdminMode(bool isActive);

        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="order">命令</param>
        /// <param name="action">执行方法</param>
        CmdDetailEtt AddOrder(string order, Action<CmdParams> action);

        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="order">命令</param>
        /// <param name="action">执行方法</param>
        CmdDetailEtt AddOrder(string order, string description, Action<CmdParams> action);

        /// <summary>
        /// 尝试执行指定命令
        /// </summary>
        /// <param name="orderLine">完整命令</param>
        /// <param name="customerContent">自定义实体</param>
        /// <returns>执行结果：是否成功</returns>
        Task<bool> TryExecuteOrder(string orderLine, object customerContent = null);

        /// <summary>
        /// 绑定内置输出器
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        Task BindingInternalOutput(Action<string, CmdOutPutType> action);


        /// <summary>
        /// 查阅历史上翻
        /// </summary>
        /// <returns>命令</returns>
        string FlipHistoryUp();

        /// <summary>
        /// 查阅历史下翻
        /// </summary>
        /// <returns>命令</returns>
        string FlipHistoryDown();
    }
}
