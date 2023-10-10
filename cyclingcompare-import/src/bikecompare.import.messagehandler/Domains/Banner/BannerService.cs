using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Domains.Banner
{
    public abstract partial class BannerService
    {
        [Sql(InsertBannerStatement)]
        public abstract Task InsertBanner(Banner banner);

        [Sql(GetBannerByExternalIdStatement)]
        public abstract Task<Banner> GetBannerByExternalId(string externalId, string merchantId, string apiManager);

        [Sql(UpdateBannerStatement)]
        public abstract Task UpdateBanner(Banner banner);

        [Sql("SELECT BannerId FROM Banner where IsDeleted = 0")]
        public abstract Task<List<string>> GetBannerIds();

        [Sql("Update Banner set IsDeleted = 1 where BannerId = @bannerId")]
        public abstract Task DeleteBanner(string bannerId);
    }
}
