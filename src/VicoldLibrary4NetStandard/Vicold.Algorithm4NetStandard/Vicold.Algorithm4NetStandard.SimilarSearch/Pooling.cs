using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Vicold.Algorithm4NetStandard.SimilarSearch.Utilities;

namespace Vicold.Algorithm4NetStandard.SimilarSearch
{
    internal class Pooling
    {

        private float _valueSum = 0;
        private int[] _valueSumOfColor = new int[4];
        private int _width;
        private int _height;
        private DataType _dataType;

        public Pooling(DataType dataType)
        {
            _dataType = dataType;
        }
        internal float[,] Execute(float[,] data, int compressionWidth, PoolingType poolingType)
        {
            _width = data.GetLength(0);
            _height = data.GetLength(1);

            if (_width < compressionWidth)
            {
                return null;
            }

            // 倍数
            var multiple = _width / compressionWidth;
            var cWidth = _width / multiple;
            var cHeight = _height / multiple;
            var result = new float[cWidth, cHeight];
            for (int cx = 0; cx < cWidth; cx++)
            {
                for (int cy = 0; cy < cHeight; cy++)
                {
                    if (poolingType == PoolingType.Mean)
                    {
                        result[cx, cy] = PoolMean(data, cx * multiple, multiple, cy * multiple, multiple);
                    }
                    else
                    {
                        result[cx, cy] = PoolMax(data, cx * multiple, multiple, cy * multiple, multiple);
                    }
                }
            }

            return result;
        }

        internal byte[,,] Execute(Bitmap bitmap, int compressionWidth, PoolingType poolingType)
        {
            _width = bitmap.Width;
            _height = bitmap.Height;

            if (_width < compressionWidth)
            {
                return null;
            }

            // 倍数
            var multiple = _width / compressionWidth;
            var cWidth = (int)Math.Ceiling(_width / (float)multiple);
            var cHeight = (int)Math.Ceiling(_height / (float)multiple);
            var result = new byte[cWidth, cHeight, 4];

            var lockbmp = new LockBitmap4Pointer(bitmap);
            lockbmp.LockBits();

            Parallel.For(0, cWidth, (cx) =>
            {

                //for (int cx = 0; cx < cWidth; cx++)
                //{
                Parallel.For(0, cHeight, (cy) =>
                {
                    //for (int cy = 0; cy < cHeight; cy++)
                    //{
                    (byte a, byte r, byte g, byte b) data;
                    if (poolingType == PoolingType.Mean)
                    {
                        data = PoolMean3(lockbmp, cx * multiple, multiple, cy * multiple, multiple);
                    }
                    else
                    {
                        data = PoolMax3(lockbmp, cx * multiple, multiple, cy * multiple, multiple);
                    }
                    result[cx, cy, 0] = data.a;
                    result[cx, cy, 1] = data.r;
                    result[cx, cy, 2] = data.g;
                    result[cx, cy, 3] = data.b;
                });
            });

            //从内存解锁Bitmap
            lockbmp.UnlockBits();

            return result;
        }

        /// <summary>
        /// 获取中值.
        /// </summary>
        /// <returns></returns>
        internal float GetMedianValue()
        {
            return _valueSum / (_width * _height);
        }

        /// <summary>
        /// 获取中值色.
        /// </summary>
        /// <returns></returns>
        internal Color GetMedianColor()
        {
            var colorCount = _width * _height;
            byte a = (byte)(_valueSumOfColor[0] / colorCount);
            byte r = (byte)(_valueSumOfColor[1] / colorCount);
            byte g = (byte)(_valueSumOfColor[2] / colorCount);
            byte b = (byte)(_valueSumOfColor[3] / colorCount);
            return Color.FromArgb(a, r, g, b);
        }

        private float PoolMean(float[,] data, int startX, int xLength, int startY, int yLength)
        {
            var result = 0f;
            var index = 0;
            for (var x = startX; x < startX + xLength && x < _width; x++)
            {
                for (var y = startY; y < startY + yLength && y < _height; y++)
                {
                    result += data[x, y];
                    index++;
                }
            }
            _valueSum += result;
            return result / index;
        }

        private float PoolMax(float[,] data, int startX, int xLength, int startY, int yLength)
        {
            var result = 0f;
            for (var x = startX; x < startX + xLength && x < _width; x++)
            {
                for (var y = startY; y < startY + yLength && y < _height; y++)
                {
                    if (result < data[x, y])
                    {
                        result = data[x, y];
                    }
                    _valueSum += data[x, y];
                }
            }

            return result;
        }
        private float PoolMean(LockBitmap4Pointer bitmap, int startX, int xLength, int startY, int yLength)
        {
            var result = 0f;
            var index = 0;
            for (var x = startX; x < startX + xLength && x < _width; x++)
            {
                for (var y = startY; y < startY + yLength && y < _height; y++)
                {
                    result += bitmap.GetPixel(x, y).ToArgb();//.GetBrightness();
                    index++;
                }
            }

            _valueSum += result;
            return result / index;
        }

        private float PoolMax(LockBitmap4Pointer bitmap, int startX, int xLength, int startY, int yLength)
        {
            var result = 0f;
            for (var x = startX; x < startX + xLength && x < _width; x++)
            {
                for (var y = startY; y < startY + yLength && y < _height; y++)
                {
                    var light = Math.Abs(bitmap.GetPixel(x, y).ToArgb());//.GetBrightness();
                    if (result < light)
                    {
                        result = light;
                    }
                    _valueSum += light;
                }
            }

            return result;
        }

        private (byte a, byte r, byte g, byte b) PoolMean3(LockBitmap4Pointer bitmap, int startX, int xLength, int startY, int yLength)
        {
            var a = 0;
            var r = 0;
            var g = 0;
            var b = 0;
            var index = 0;
            for (var x = startX; x < startX + xLength && x < _width; x++)
            {
                for (var y = startY; y < startY + yLength && y < _height; y++)
                {
                    var color = bitmap.GetPixel(x, y);
                    a += color.A;
                    r += color.R;
                    g += color.G;
                    b += color.B;
                    index++;
                }
            }

            _valueSumOfColor[0] += a;
            _valueSumOfColor[1] += r;
            _valueSumOfColor[2] += g;
            _valueSumOfColor[3] += b;
            return ((byte)(a / index), (byte)(r / index), (byte)(g / index), (byte)(b / index));
        }

        private (byte a, byte r, byte g, byte b) PoolMax3(LockBitmap4Pointer bitmap, int startX, int xLength, int startY, int yLength)
        {
            var a = 0;
            var r = 0;
            var g = 0;
            var b = 0;
            for (var x = startX; x < startX + xLength && x < _width; x++)
            {
                for (var y = startY; y < startY + yLength && y < _height; y++)
                {
                    var color = bitmap.GetPixel(x, y);
                    if (a < color.A)
                    {
                        a = color.A;
                    }
                    if (r < color.R)
                    {
                        r = color.R;
                    }
                    if (g < color.G)
                    {
                        g = color.G;
                    }
                    if (b < color.B)
                    {
                        b = color.B;
                    }
                    _valueSumOfColor[0] += color.A;
                    _valueSumOfColor[1] += color.R;
                    _valueSumOfColor[2] += color.G;
                    _valueSumOfColor[3] += color.B;
                }
            }

            return ((byte)a, (byte)r, (byte)g, (byte)b);
        }
    }
}
