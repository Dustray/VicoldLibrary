using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Vicold.Algorithm4NetStandard.SimilarSearch.Utilities;

namespace Vicold.Algorithm4NetStandard.SimilarSearch
{
    internal class Pooling
    {

        private float _valueSum = 0;
        private int _width;
        private int _height;

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
            var result = new float[_width / multiple, _height / multiple];
            for (int x = 0, cx = 0; x < _width; x += multiple, cx++)
            {
                for (int y = 0, cy = 0; y < _height; y += multiple, cy++)
                {
                    if (poolingType == PoolingType.Mean)
                    {
                        result[cx, cy] = PoolMean(data, x, multiple, y, multiple);
                    }
                    else
                    {
                        result[cx, cy] = PoolMax(data, x, multiple, y, multiple);
                    }
                }
            }

            return result;
        }

        internal float[,] Execute(Bitmap bitmap, int compressionWidth, PoolingType poolingType)
        {
            _width = bitmap.Width;
            _height = bitmap.Height;

            if (_width < compressionWidth)
            {
                return null;
            }

            // 倍数
            var multiple = _width / compressionWidth;
            var result = new float[(int)Math.Ceiling(_width / (float)multiple), (int)Math.Ceiling(_height / (float)multiple)];

            var lockbmp = new LockBitmap4Pointer(bitmap);
            lockbmp.LockBits();
            for (int x = 0, cx = 0; x < _width; x += multiple, cx++)
            {
                for (int y = 0, cy = 0; y < _height; y += multiple, cy++)
                {
                    if (poolingType == PoolingType.Mean)
                    {
                        result[cx, cy] = PoolMean(lockbmp, x, multiple, y, multiple);
                    }
                    else
                    {
                        result[cx, cy] = PoolMax(lockbmp, x, multiple, y, multiple);
                    }
                }
            }

            //从内存解锁Bitmap
            lockbmp.UnlockBits();

            return result;
        }

        internal float GetMedianValue()
        {
            return _valueSum / (_width * _height);
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
                    result += bitmap.GetPixel(x, y).GetBrightness();
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
    }
}
