using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vicold.Terminal4Net
{
    /// <summary>
    /// 可执行类型枚举
    /// </summary>
    public enum ExecutableType
    {
        /// <summary>
        /// 任何情况可执行
        /// </summary>
        All = 15,
        /// <summary>
        /// Debug可执行
        /// </summary>
        Debug = 1034,
        /// <summary>
        /// Release可执行
        /// </summary>
        Release = 2430,
        /// <summary>
        /// 管理员可执行
        /// </summary>
        Admin = 8923,
    }
}
