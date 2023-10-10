using Insight.Database;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries.Filters
{
    public class PriceFilterProvider : ProductFilterProviderBase
    {
        private readonly IFilterService _filterService;
        public PriceFilterProvider(IFilterService filterService, IMemoryCache cache, ProductSummaryService productSummaryService) : base(productSummaryService, cache)
        {
            _filterService = filterService;
        }
        //public async Task<IList<ProductSummary>> GetFilteredProducts(Filter filter, string categoryId)
        //{
        //    var minValue = filter.MinValue.HasValue ? filter.MinValue.Value : decimal.Zero;
        //    var maxValue = filter.MaxValue.HasValue ? filter.MaxValue.Value : decimal.MaxValue;
        //    var products = await GetProductsInPriceRange(categoryId, minValue, maxValue);
        //    return products;
        //}

        public override async Task<IList<ProductSummary>> GetFilteredProducts(string categoryId, FilterViewModel filter)
        {
            var productsToFilter = await base.GetProductSummariesByCategory(categoryId);
            var minValue = filter.MinValue.HasValue ? filter.MinValue.Value : decimal.Zero;
            var maxValue = filter.MaxValue.HasValue ? filter.MaxValue.Value : decimal.MaxValue;
            return productsToFilter.Where(x => x.MinPrice >= minValue && x.MinPrice <= maxValue).ToList();
        }

        public override Task<Dictionary<string, bool>> GetFilterOptions(string categoryId, int categoryFilterGroupId)
        {
            return Task.FromResult<Dictionary<string,bool>>(null);
        }

        //[Sql(@"SELECT ps.ProductId, ps.ProductName, ps.Brand, ps.UrlSlug, ps.Description, ps.Rating, ps.MinPrice, ps.PreviewImageUrl, ps.IsOnSale
        //        FROM ProductSummary ps
        //        join ProductCategory pc on ps.ProductId = pc.ProductId
        //        join Category c on pc.CategoryId = c.CategoryId
        //        where(@categoryId is null or c.CategoryId = @categoryId or c.ParentId = @categoryId) and ps.MinPrice > @minValue and ps.MinPrice < @maxValue")]
        //public abstract Task<List<ProductSummary>> GetProductsInPriceRange(string categoryId, decimal minValue, decimal maxValue);
    }
}
