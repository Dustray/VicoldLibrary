using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Vicold.Algorithm4NetStandard.SimilarSearch
{
    public interface ISimilarSearcher
    {
        byte[] GetEigenvalue(float[,] data, PoolingType poolingType, int precision = 32, float threshold = 9999);
        byte[] GetEigenvalue(Bitmap bitmap, PoolingType poolingType, int precision = 32, float threshold = 9999);
        Point GetCompressionPoint();
        float[,] GetPoolData();
        float GetSimilarity(ISimilarSearcher similarSearcher);
    }
}
