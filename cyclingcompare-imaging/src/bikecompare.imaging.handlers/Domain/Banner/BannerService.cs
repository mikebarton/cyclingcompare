using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.handlers.Domain.Banner
{
    public abstract partial class BannerService
    {
        [Sql(InsertBannerStatement)]
        public abstract Task InsertBanner(Banner banner);

        [Sql(UpdateBannerStatement)]
        public abstract Task UpdateBanner(Banner banner);

        [Sql(GetBannerByIdStatement)]
        public abstract Task<Banner> GetBannerById(string bannerId);

        [Sql(DeleteBannerByIdStatement)]
        public abstract Task DeleteBannerById(string bannerId);
    }
}
