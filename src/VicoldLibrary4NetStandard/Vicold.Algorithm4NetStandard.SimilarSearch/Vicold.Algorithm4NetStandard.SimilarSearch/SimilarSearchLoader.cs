using System;
using System.Collections.Generic;
using System.Text;

namespace Vicold.Algorithm4NetStandard.SimilarSearch
{
    public static class SimilarSearchLoader
    {
        public static ISimilarSearcher Creator()
        {
            return new SimilarSearcher();
        }
    }
}
