using System;
using System.Drawing;

namespace Vicold.Algorithm4NetStandard.SimilarSearch
{
    internal class SimilarSearcher : ISimilarSearcher
    {
        private float[,] _poolData;
        private byte[,,] _poolColor;
        internal byte[] SimilarArray;

        private Point _compressionPoint;
        internal DataType InputDataType;

        public byte[] GetEigenvalue(float[,] data, PoolingType poolingType, int precisionX = 16, int precisionY = 16, float threshold = 9999)
        {
            InputDataType = DataType.Data;
            var pool = new Pooling(InputDataType);
            var poolData = pool.Execute(data, precisionX, precisionY, poolingType);
            _compressionPoint = new Point(poolData.GetLength(0), poolData.GetLength(1));
            _poolData = poolData;

            if (threshold == 9999)
            {
                threshold = pool.GetMedianValue();
            }
            var binary = new Binarization();
            var eigenvalue = binary.Execute(poolData, threshold);
            SimilarArray = binary.GetSimilarArray();

            return eigenvalue;
        }

        public byte[] GetEigenvalue(Bitmap bitmap, PoolingType poolingType, Color threshold, int precisionX = 16, int precisionY = 16)
        {
            InputDataType = DataType.Image;
            var pool = new Pooling(InputDataType);
            var poolColor = pool.Execute(bitmap, precisionX, precisionY, poolingType);
            _compressionPoint = new Point(poolColor.GetLength(0), poolColor.GetLength(1));
            _poolColor = poolColor;

            if (threshold == Color.Black)
            {
                threshold = pool.GetMedianColor();
            }
            var binary = new Binarization();
            var eigenvalue = binary.Execute(poolColor, threshold);
            SimilarArray = binary.GetSimilarArray();

            return eigenvalue;
        }

        public float GetSimilarity(ISimilarSearcher similarSearcher)
        {
            var si = similarSearcher as SimilarSearcher;
            if (InputDataType != si.InputDataType)
            {
                throw new Exception("不是同一类型");
            }

            var len = 0;
            var minLength = Math.Min(SimilarArray.Length, si.SimilarArray.Length);
            if (InputDataType == DataType.Data)
            {
                for (var i = 0; i < minLength; i++)
                {
                    //if (Math.Abs(SimilarArray[i] - si.SimilarArray[i]) < 20)
                    //{
                    //    len++;
                    //}
                    var yihuo = SimilarArray[i] ^ si.SimilarArray[i];
                    len += CountOne(yihuo);
                }
                return len / (float)SimilarArray.Length;
            }
            else
            {
                var dataCount = minLength / 4;
                for (var i = 0; i < dataCount; i++)
                {
                    //var yihuo = SimilarArray[i] ^ si.SimilarArray[i];
                    //len += CountOne(yihuo);
                    if (Math.Abs(SimilarArray[4 * i] - si.SimilarArray[4 * i]) > 40
                        || Math.Abs(SimilarArray[4 * i + 1] - si.SimilarArray[4 * i + 1]) > 40
                        || Math.Abs(SimilarArray[4 * i + 2] - si.SimilarArray[4 * i + 2]) > 40
                        || Math.Abs(SimilarArray[4 * i + 3] - si.SimilarArray[4 * i + 3]) > 40)
                    {
                        continue;
                    }
                    len++;
                    //if (SimilarArray[i] == si.SimilarArray[i])
                    //{
                    //    len++;
                    //}
                }

                return len / (float)dataCount;
            }
        }

        public float[,] GetPoolData()
        {
            return _poolData;
        }
        public byte[,,] GetPoolColor()
        {
            return _poolColor;
        }
        public Point GetCompressionPoint()
        {
            return _compressionPoint;
        }
        public int CountOne(int n)
        {
            int count = 0;
            int flag = 1;
            int c = 0;
            while (flag != 0 && c < 8)
            {
                if ((n & flag) == 0)
                {
                    count++;
                }
                flag = flag << 1;
                c++;
            }
            return count;
        }
    }
}
