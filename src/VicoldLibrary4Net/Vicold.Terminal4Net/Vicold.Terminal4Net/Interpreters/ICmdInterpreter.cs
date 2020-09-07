using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vicold.Terminal4Net
{
    /// <summary>
    /// 命令解释器接口
    /// </summary>
    public interface ICmdInterpreter
    {
        /// <summary>
        /// 解释器执行解释命令行分割操作
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        CmdParams Execute(string order);
    }
}
