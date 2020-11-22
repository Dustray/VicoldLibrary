using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vicold.FileConfusion.Utilities
{
    internal static class ArrayUtility
    {
        public static byte[] Invert(byte[] source)
        {
            var mid = source.Length / 2;
            var max = source.Length - 1;

            // 同步速度快
            for (var i = 0; i < mid; i++)
            // Parallel.For(0, mid, (i) =>
            {
                var tmp = source[i];
                source[i] = source[max - i];
                source[max - i] = tmp;
            }// );
            return source;
        }
    }
}
