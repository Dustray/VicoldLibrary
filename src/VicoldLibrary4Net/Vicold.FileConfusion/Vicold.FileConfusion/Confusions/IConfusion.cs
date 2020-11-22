using System;
using System.Collections.Generic;
using System.Text;

namespace Vicold.FileConfusion.Confusions
{
    internal interface IConfusion
    {
        /// <summary>
        /// 混淆
        /// </summary>
        /// <param name="sourceFilePath">源文件</param>
        /// <param name="targetFilePath">输出文件</param>
        void Confuse(string sourceFilePath, string targetFilePath);

        /// <summary>
        /// 反混淆
        /// </summary>
        /// <param name="sourceFilePath">源文件</param>
        /// <param name="targetFilePath">输出文件</param>
        void AntiConfuse(string sourceFilePath, string targetFilePath);
    }
}
