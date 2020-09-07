using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vicold.Terminal4Net
{
    /// <summary>
    /// 内置命令行解释器
    /// </summary>
    public class CmdInterpreter : ICmdInterpreter
    {
        /// <summary>
        /// 解释器执行解释命令行分割操作
        /// </summary>
        /// <param name="order"></param>
        /// <returns>返回null表示命令行不正确</returns>
        public CmdParams Execute(string orderStr)
        {
            //格式化命令行
            orderStr = new Regex("[\\s]+").Replace(orderStr, " ");
            orderStr = orderStr.Trim();
            var orderArray = orderStr.Split(' ');
            var cmdParam = new CmdParams();
            cmdParam.OrderLine = orderStr;
            if (orderArray.Length == 0 || (orderArray.Length == 1 && orderArray[0] == ""))
                return null;
            cmdParam.Order = orderArray[0];
            if (orderArray.Length == 1)
            {
                return cmdParam;
            }
            //分离指令参数
            var pairParams = new List<KeyValuePair<string, string>>();
            for (var i = 1; i < orderArray.Length; i++)
            {
                var para = orderArray[i];
                if (para.Length == 0) continue;
                if (para[0] == '-')
                {
                    string value = null;
                    if (i < orderArray.Length - 1)
                    {
                        var nextPara = orderArray[i + 1];
                        if (nextPara.Length == 0) continue;
                        if (nextPara[0] != '-')
                        {
                            value = nextPara;
                            i++;
                        }
                    }
                    var pair = new KeyValuePair<string, string>(para, value);
                    pairParams.Add(pair);
                }
                else
                {
                    pairParams.Add(new KeyValuePair<string, string>(null, para));
                }

            }
            cmdParam.PairParams = pairParams;
            return cmdParam;
        }
    }
}
