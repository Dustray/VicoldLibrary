using System;
using System.Collections.Generic;
using System.Text;

namespace Vicold.Algorithm4NetStandard.SimilarSearch
{
    internal class Binarization
    {
        private byte[] _similarArray;
        internal byte[] Execute(float[,] data, float threshold)
        {
            var width = data.GetLength(0);
            var height = data.GetLength(1);

            // 倍数
            var sourceBytes = new byte[width * height];
            var index = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (data[x, y] <= threshold)
                    {
                        sourceBytes[index] = 0;
                    }
                    else
                    {
                        sourceBytes[index] = 1;
                    }
                    index++;
                }
            }

            _similarArray = sourceBytes;
            var byteLength = (int)Math.Ceiling(sourceBytes.Length / 8f);
            var bytes = new byte[byteLength];
            for (var i = 0; i < byteLength; i++)
            {
                var spans = sourceBytes.AsSpan(i * 8, byteLength - i >= 8 ? 8 : byteLength - i);
                var strb = new StringBuilder();
                foreach (var span in spans)
                {
                    strb.Append(span);
                }
                bytes[i] = Convert.ToByte(strb.ToString(), 2);
            }
            return bytes;
        }

        internal byte[] GetSimilarArray()
        {
            return _similarArray;
        }
    }
}
