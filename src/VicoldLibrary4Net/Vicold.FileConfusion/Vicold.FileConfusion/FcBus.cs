using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Vicold.FileConfusion.Confusions;
using Vicold.FileConfusion.FileTools;
using Vicold.FileConfusion.Utilities;

namespace Vicold.FileConfusion
{
    public class FcBus
    {
        private IConfusion _confusion;
        public FcBus()
        {
            _confusion = new PRConfusion();
        }

        public void Confuse(string sourceFilePath, string targetFilePath)
        {
            if (!File.Exists(sourceFilePath))
            {
                throw new Exception("File not found.");
            }

            _confusion.Confuse(sourceFilePath, targetFilePath);
        }

        public void AntiConfuse(string sourceFilePath, string targetFilePath)
        {
            if (!File.Exists(sourceFilePath))
            {
                throw new Exception("File not found.");
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); //  开始监视代码运行时间

            _confusion.AntiConfuse(sourceFilePath, targetFilePath);

            stopwatch.Stop(); //  停止监视
            Console.WriteLine($"用时{stopwatch.Elapsed}");
        }
    }
}
