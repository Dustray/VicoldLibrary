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
            var cWidth = (int)Math.Ceiling(_width / (float)multiple);
            var cHeight = (int)Math.Ceiling(_height / (float)multiple);
            var result = new float[cWidth, cHeight];

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
                    if (poolingType == PoolingType.Mean)
                    {
                        result[cx, cy] = PoolMean(lockbmp, cx * multiple, multiple, cy * multiple, multiple);
                    }
                    else
                    {
                        result[cx, cy] = PoolMax(lockbmp, cx * multiple, multiple, cy * multiple, multiple);
                    }
                });
            });

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
    }
}
