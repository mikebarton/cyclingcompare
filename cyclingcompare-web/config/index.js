const config = {
    GoogleAnalyticsId:process.env.NEXT_PUBLIC_GOOGLE_ANALYTICS_ID,
    ListingService: {
        Hostname: process.env.NEXT_PUBLIC_LISTINGS_API_HOST,
        SubmitConversionPath: '/Listing/SubmitConversion'
    },
    ApiGateway:{
        HostName: process.env.NEXT_PUBLIC_API_GATEWAY_HOST,
        SubmitConversionPath: '/Listing/SubmitConversion',
        SearchByKeywordPath: '/ProductSummary/Search/',
        GetAllCategoriesPath: '/Category/GetAll',
        GetProductFilters: '/ProductSummary/GetFilters',
        GetPagedProductsByCategoryId: '/ProductSummary/GetPagedByCategoryId/ps/{0}/pn/{1}/so/{2}/cid/{3}',
        GetFilteredProducts: '/ProductSummary/FilterProducts/ps/{0}/pn/{1}/so/{2}/cid/{3}',
        GetTopBikes: '/ProductSummary/GetTopBikes',
        GetOtherDeals: '/ProductSummary/GetOtherDeals',
        GetTopDealsByCategory: '/ProductSummary/GetTopDealsByCategory/',
        GetListingsById: '/Listing/GetListing/',
        GetRandomBanner: '/Banner/GetRandomBanner',
    }
}

export default config;