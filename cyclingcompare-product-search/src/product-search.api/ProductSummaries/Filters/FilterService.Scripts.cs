using product_search.api.ProductSummaries.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries.Filters
{
    public partial class FilterService 
    {
        public const string GetBrandsStatement = @"select distinct ps.Brand
                                                        from ProductSummary ps
                                                        join ProductCategory pc on ps.ProductId = pc.ProductId
                                                        join Category c on pc.CategoryId = c.CategoryId
                                                        where @categoryId is null or c.CategoryId = @categoryId or c.ParentId = @categoryId 
                                                        order by Brand";

        public const string GetMerchantsStatement = @"select distinct ms.MerchantName
                                                        from MerchantSummary ms
                                                        join MerchantProductSummary mps on ms.MerchantId = mps.MerchantId
                                                        join ProductSummary ps on mps.ProductId = ps.ProductId
                                                        join ProductCategory pc on ps.ProductId = pc.ProductId
                                                        join Category c on pc.CategoryId = c.CategoryId
                                                        where @categoryId is null or c.CategoryId = @categoryId or c.ParentId = @categoryId 
                                                        order by MerchantName";

        public const string GetSizesStatement = @"select cf.Name
                                                    from CategoryFilter cf
                                                    where CategoryId = @categoryId and FilterTypeCode = 'SIZ' and CategoryFilterGroupId = @categoryFilterGroupId order by `Order`";

        public const string GetGendersStatement = @"select cf.Name
                                                    from CategoryFilter cf
                                                    where CategoryId = @categoryId and FilterTypeCode = 'GEN' and CategoryFilterGroupId = @categoryFilterGroupId order by `Order`";

        public const string GetColoursStatement = @"select cf.Name
                                                    from CategoryFilter cf
                                                    where CategoryId = @categoryId and FilterTypeCode = 'COL' and CategoryFilterGroupId = @categoryFilterGroupId order by `Order`";

        //public const string GetCategoryFiltersStatement = @"select cf.CategoryFilterId, cf.Name, cf.FilterTypeCode, cf.Order, cf.CategoryId 
        //                                                    from CategoryFilter cf
        //                                                    where cf.CategoryId = @categoryId and FilterTypeCode = @filterTypeCode";

        
    }
}
