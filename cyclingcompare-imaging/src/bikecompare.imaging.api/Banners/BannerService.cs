using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.api.Banners
{
    public abstract partial class BannerService
    {
        [Sql(GetBannersByDimensionsStatement)]
        public abstract Task<List<Banner>> GetBannersByDimensions(int width, int height);
    }
}
