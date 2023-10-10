using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.api.Banners
{
    public partial class BannerService
    {
        public const string GetBannersByDimensionsStatement = @"SELECT BannerId,
                                                                        Height,
                                                                        Width,
                                                                        ImageUrl,
                                                                        Name,
                                                                        MerchantId,
                                                                        MerchantName,
                                                                        TargetUrl,
                                                                        TrackingCode,
                                                                        TrackingUrl
                                                                    FROM Banner
                                                                    WHERE (Width > (@width * 0.9) and Width < (@width * 1.1)) 
	                                                                    and (Height > (@height * 0.9) and Height < (@height * 1.1))";
    }
}
