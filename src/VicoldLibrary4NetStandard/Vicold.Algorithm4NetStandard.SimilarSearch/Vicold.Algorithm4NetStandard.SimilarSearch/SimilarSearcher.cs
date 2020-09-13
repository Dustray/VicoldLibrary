﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Vicold.Algorithm4NetStandard.SimilarSearch
{
    internal class SimilarSearcher : ISimilarSearcher
    {
        private float[,] _poolData;
        internal byte[] SimilarArray;

        private Point _compressionPoint;

        public byte[] GetEigenvalue(float[,] data, PoolingType poolingType, int precision = 16, float threshold = 9999)
        {
            var pool = new Pooling();
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

        public byte[] GetEigenvalue(Bitmap bitmap, PoolingType poolingType, int precision = 16, float threshold = 9999)
        {
            var pool = new Pooling();
            var poolData = pool.Execute(bitmap, precision, poolingType);
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

        public float GetSimilarity(ISimilarSearcher similarSearcher)
        {
            var si = similarSearcher as SimilarSearcher;
            var len = 0;
            var minLength = Math.Min(SimilarArray.Length, si.SimilarArray.Length);
            for (var i = 0; i < minLength; i++)
            {
                if (Math.Abs(SimilarArray[i] - si.SimilarArray[i]) < 30)
                {
                    len++;
                }
            }
            return len / (float)SimilarArray.Length;
        }

        public float[,] GetPoolData()
        {
            return _poolData;
        }
        public Point GetCompressionPoint()
        {
            return _compressionPoint;
        }
    }
}
