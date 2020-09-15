using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Vicold.Algorithm4NetStandard.SimilarSearch
{
    public interface ISimilarSearcher
    {
        byte[] GetEigenvalue(float[,] data, PoolingType poolingType, int precisionX = 16, int precisionY = 16, float threshold = 9999);
        byte[] GetEigenvalue(Bitmap bitmap, PoolingType poolingType, Color threshold, int precisionX = 16, int precisionY = 16);
        Point GetCompressionPoint();
        float[,] GetPoolData();
        byte[,,] GetPoolColor();
        float GetSimilarity(ISimilarSearcher similarSearcher);
    }
}
