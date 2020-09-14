using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml.Schema;

namespace Vicold.Algorithm4NetStandard.SimilarSearch
{
    internal class Binarization
    {
        private byte[] _similarArray;
        internal byte[] Execute(float[,] data, float threshold)
        {
            var width = data.GetLength(0);
            var height = data.GetLength(1);
            var (max, min) = GetExtreme(data);

            // 倍数
            var sourceBytes = new byte[width * height];
            var index = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //if (data[x, y] <= threshold)
                    //{
                    //    sourceBytes[index] = 0;
                    //}
                    //else
                    //{
                    //    sourceBytes[index] = 1;
                    //}
                    var value = Normalize(data[x, y], max, min);
                    sourceBytes[index] = Convert.ToByte(value * 255);
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
                //bytes[i] = Convert.ToByte(strb.ToString(), 2);
            }
            return bytes;
        }

        internal byte[] Execute(byte[,,] data, Color threshold)
        {
            var width = data.GetLength(0);
            var height = data.GetLength(1);
            var (max, min) = GetExtreme(data);

            // 倍数
            var sourceBytes = new byte[width * height * 4];
            var index = 0;
            byte no = 0;
            byte yes = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    sourceBytes[index] = data[x, y, 0] <= threshold.A ? no: yes;
                    index++;
                    sourceBytes[index] = data[x, y, 1] <= threshold.R ? no: yes;
                    index++;
                    sourceBytes[index] = data[x, y, 2] <= threshold.G ? no: yes;
                    index++;
                    sourceBytes[index] = data[x, y, 3] <= threshold.B ? no: yes;

                    //var value = Normalize(data[x, y], max, min);
                    //sourceBytes[index] = Convert.ToByte(value * 255);
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
                //bytes[i] = Convert.ToByte(strb.ToString(), 2);
            }
            return bytes;
        }
        internal byte[] GetSimilarArray()
        {
            return _similarArray;
        }

        private (float max, float min) GetExtreme(float[,] data)
        {
            var max = data[0, 0];
            var min = data[0, 0];
            var width = data.GetLength(0);
            var height = data.GetLength(1);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var v = data[x, y];
                    if (v > max)
                    {
                        max = v;
                    }
                    else if (v < min)
                    {
                        min = v;
                    }
                }
            }
            return (max, min);
        }

        private (Color max, Color min) GetExtreme(byte[,,] rgb)
        {
            var maxA = rgb[0, 0, 0];
            var maxR = rgb[0, 0, 1];
            var maxG = rgb[0, 0, 2];
            var maxB = rgb[0, 0, 3];
            var minA = rgb[0, 0, 0];
            var minR = rgb[0, 0, 1];
            var minG = rgb[0, 0, 2];
            var minB = rgb[0, 0, 3];
            var width = rgb.GetLength(0);
            var height = rgb.GetLength(1);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    maxA = Math.Max(maxA, rgb[x, y, 0]);
                    maxR = Math.Max(maxR, rgb[x, y, 1]);
                    maxG = Math.Max(maxG, rgb[x, y, 2]);
                    maxB = Math.Max(maxB, rgb[x, y, 3]);
                    minA = Math.Max(minA, rgb[x, y, 0]);
                    minR = Math.Max(minR, rgb[x, y, 1]);
                    minG = Math.Max(minG, rgb[x, y, 2]);
                    minB = Math.Max(minB, rgb[x, y, 3]);
                }
            }
            var maxColor = Color.FromArgb(maxA, maxR, maxG, maxB);
            var minColor = Color.FromArgb(minA, minR, minG, minB);
            return (maxColor, minColor);
        }
        public float Normalize(float value, float max, float min)
        {
            return (value - min) / (max - min);
        }
    }
}
