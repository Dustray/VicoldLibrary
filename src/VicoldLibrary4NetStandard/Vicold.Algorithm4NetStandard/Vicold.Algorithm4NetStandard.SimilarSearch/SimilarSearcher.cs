using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Vicold.Algorithm4NetStandard.SimilarSearch
{
    internal class SimilarSearcher : ISimilarSearcher
    {
        private float[,] _poolData;
        private byte[,,] _poolColor;
        internal byte[] SimilarArray;

        private Point _compressionPoint;
        internal DataType InputDataType;

        public byte[] GetEigenvalue(float[,] data, PoolingType poolingType, int precision = 16, float threshold = 9999)
        {
            InputDataType = DataType.Data;
            var pool = new Pooling(InputDataType);
            var poolData = pool.Execute(data, precision, poolingType);
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

        public byte[] GetEigenvalue(Bitmap bitmap, PoolingType poolingType, Color threshold, int precision = 16)
        {
            InputDataType = DataType.Image;
            var pool = new Pooling(InputDataType);
            var poolColor = pool.Execute(bitmap, precision, poolingType);
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
                    if (Math.Abs(SimilarArray[i] - si.SimilarArray[i]) < 20)
                    {
                        len++;
                    }
                }
            }
            else
            {

            }
            return len / (float)SimilarArray.Length;
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
    }
}
