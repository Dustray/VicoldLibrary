﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vicold.Terminal4Net
{
    /// <summary>
    /// 命令返回参数
    /// </summary>
    public class CmdParams
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public CmdParams()
        {
            PairParams = new List<KeyValuePair<string, string>>();
        }
        /// <summary>
        /// 命令
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// 参数对
        /// </summary>
        public List<KeyValuePair<string, string>> PairParams { get; set; }
        /// <summary>
        /// 完整命令
        /// </summary>
        public string OrderLine { get; set; }
        /// <summary>
        /// 自定义实体
        /// </summary>
        public object CustomerContent { get; set; }
    }
}
