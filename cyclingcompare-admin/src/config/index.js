const config = {
    IdentityPlatform: {
        ApiKey: process.env.REACT_APP_IDENTITY_API_KEY,
        AuthDomain: process.env.REACT_APP_IDENTITY_AUTHDOMAIN
    },
    ProductSearchApi : {
        host: process.env.REACT_APP_PRODUCT_SEARCH_API_HOST,
        getAllCategorys: '/AdminCategory/GetFlatList',
        getCategoryHierarchy: '/AdminCategory/GetHierarchy',
        updateCategory: '/AdminCategory/UpdateCategory',
        deleteCategory: '/AdminCategory/DeleteCategory/',
        createCategory: '/AdminCategory/CreateCategory',
        getFiltersByCategory: '/AdminFilter/GetFilters/',
        getTranslations: '/AdminFilter/GetTranslations/',
        addOrUpdateFilter: '/AdminFilter/AddOrUpdateFilter',
        deleteFilter: '/AdminFilter/DeleteFilter',
        getTranslationsByFilterId: '/AdminFilter/GetTranslationsByFilterId/',
        addOrUpdateTranslation: '/AdminFilter/AddOrUpdateTranslation',
        deleteTranslationById: '/AdminFilter/DeleteTranslation/',
        getFilterGroups: '/AdminFilter/GetFilterGroups/'     ,
        addOrUpdateFilterGroup: '/AdminFilter/AddOrUpdateFilterGroup'   ,
        deleteFilterGroup: '/AdminFilter/DeleteFilterGroup/',
        relocateFilter: '/AdminFilter/RelocateFilter/'
    },
    ImportApi: {
        host: process.env.REACT_APP_IMPORT_API_HOST,
        getExternalCategories: '/Category/GetExternalCategories',
        getCategoryMappings: '/Category/GetMappings',
        UpdateCategoryMappings: '/Category/UpdateMapping',
        getProductSummaries: '/GlobalProduct/GetAllProductSummaries',
        getGlobalProduct: '/GlobalProduct/GetProductDetails/',
        updateGlobalProduct: '/GlobalProduct/UpdateGlobalProduct',
        publishGlobalProducts: '/GlobalProduct/PublishGlobalProducts',
        unpublishGlobalProducts: '/GlobalProduct/UnpublishGlobalProducts',
        setAsReviewed: '/GlobalProduct/SetReviewed/',
        setAsNotReviewed: '/GlobalProduct/SetNotReviewed/',
        GetMerchants: '/Merchants/GetAllMerchants',
        UnPublishProductsByMerchant: '/GlobalProduct/UnpublishGlobalProductByMerchantId/',
        PublishProductsByMerchant: '/GlobalProduct/PublishGlobalProductByMerchantId/'
    }
}

export default config;