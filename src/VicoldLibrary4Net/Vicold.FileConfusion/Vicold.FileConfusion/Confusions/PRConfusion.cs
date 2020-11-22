using System;
using System.Collections.Generic;
using System.Text;
using Vicold.FileConfusion.FileTools;
using Vicold.FileConfusion.Utilities;

namespace Vicold.FileConfusion.Confusions
{
    /// <summary>
    /// 分片逆转混淆（对称混淆）
    /// </summary>
    internal class PRConfusion : IConfusion
    {
        
        public void AntiConfuse(string sourceFilePath, string targetFilePath)
        {
            Change(sourceFilePath, targetFilePath);
        }

        public void Confuse(string sourceFilePath, string targetFilePath)
        {
            Change(sourceFilePath, targetFilePath);
        }

        private void Change(string sourceFilePath, string targetFilePath)
        {
            using (var slice = new ReadSlice(sourceFilePath))
            {
                using (var writer = new Writer(targetFilePath))
                {
                    while (slice.HasNext())
                    {
                        var slb = slice.Read();
                        slb = ArrayUtility.Invert(slb);
                        writer.Write(slb);
                    }
                }
            }
        }
    }
}
