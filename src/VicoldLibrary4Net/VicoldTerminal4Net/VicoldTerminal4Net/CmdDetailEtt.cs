using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VicoldTerminal4Net
{
    class CmdDetailEtt
    {
        /// <summary>
        /// 命令
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// 命令描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 命令执行回调
        /// </summary>
        public Action<CmdParams> Callback { get; set; }
    }
}
