using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VicoldTerminal4Net
{
    public class CmdDetailEtt
    {
        public CmdDetailEtt()
        {
            ParamNames = new Dictionary<string, string>();
        }

        /// <summary>
        /// 命令
        /// </summary>
        public string Order { get; internal set; }
        /// <summary>
        /// 命令描述
        /// </summary>
        public string Description { get; internal set; }
        /// <summary>
        /// 命令执行回调
        /// </summary>
        public Action<CmdParams> Callback { get; internal set; }
        /// <summary>
        /// 以“-”开头的参数名
        /// </summary>
        internal Dictionary<string, string> ParamNames { get; set; }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameter">参数名（添加时可不带“-”前缀）</param>
        /// <param name="description">参数说明</param>
        /// <returns></returns>
        public CmdDetailEtt AddParam(string parameter, string description)
        {
            if (string.IsNullOrWhiteSpace(parameter))
            {
                throw new CmdException($"参数名不能为空。");
            }
            if (ParamNames.ContainsKey(parameter))
            {
                throw new CmdException($"已存在一个相同名称的参数：{parameter}。");
            }

            if (!parameter.StartsWith("-"))
            {
                parameter = $"-{parameter}";
            }
            ParamNames[parameter] = description??"";
            return this;
        }
    }
}
