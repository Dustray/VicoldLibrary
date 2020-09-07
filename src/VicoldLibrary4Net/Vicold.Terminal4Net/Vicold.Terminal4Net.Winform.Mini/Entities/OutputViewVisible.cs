using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vicold.Terminal4Net.Winform.Mini.Entities
{
    /// <summary>
    /// 输出框显示
    /// </summary>
    public enum OutputViewVisible
    {
        /// <summary>
        /// 自动
        /// <para>有内容显示，无内容隐藏</para>
        /// </summary>
        Auto,
        /// <summary>
        /// 始终显示
        /// </summary>
        Visible,
        /// <summary>
        /// 始终隐藏
        /// </summary>
        Hidden,
    }
}
