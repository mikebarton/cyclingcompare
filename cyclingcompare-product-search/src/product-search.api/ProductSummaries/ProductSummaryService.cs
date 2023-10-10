using Insight.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries
{
    public abstract class ProductSummaryService
    {
        public IDbConnection Connection { get; set; }

        public async Task<IList<ProductSummary>> GetPagedByCategory(int pageSize, int pageNum, api.Constants.SortOrder sortOrder, string categoryId)
        {

            var offset = pageSize * pageNum;
            var orderBy = string.Empty;

            switch (sortOrder)
            {
                case api.Constants.SortOrder.NameAsc:
                    orderBy = " ORDER BY ps.ProductName, ps.Rating desc";
                    break;
                case api.Constants.SortOrder.NameDesc:
                    orderBy = " ORDER BY ps.ProductName desc, ps.Rating desc";
                    break;
                case api.Constants.SortOrder.PriceAsc:
                    orderBy = " ORDER BY ps.MinPrice, ps.Rating desc";
                    break;
                case api.Constants.SortOrder.PriceDesc:
                    orderBy = " ORDER BY ps.MinPrice desc, ps.Rating desc";
                    break;
                default:
                    orderBy = " ORDER BY ps.Rating desc";
                    break;
            }

            var query = string.Format(RetrievePagedProductsByCategoryStatement, orderBy);
            var results = await Connection.QuerySqlAsync<ProductSummary>(query, new { categoryId, offset = Math.Max(0, offset-1), pageSize });
            return results;
        }

        [Sql(@"SELECT count(*) 
                FROM ProductSummary ps
                JOIN ProductCategory pc on pc.ProductId = ps.ProductId
                JOIN Category c on pc.CategoryId = c.CategoryId
                WHERE c.CategoryId = @categoryId or  c.ParentId = @categoryId")]
        public abstract Task<long> GetProductCountByCategory(string categoryId);

        public const string RetrievePagedProductsByCategoryStatement = @"SELECT ps.ProductId, ps.ProductName, ps.Brand, ps.UrlSlug, ps.Description, ps.Rating, mps.Price as MinPrice, mps.PriceRrp as MinPriceRrp, ps.PreviewImageUrl, ps.IsOnSale, ms.MerchantName, ps.Size, ps.Gender, ps.Colour, ps.CurrencyCode, mps.HasStock
                FROM ProductSummary ps 
                JOIN ProductCategory pc on pc.ProductId = ps.ProductId                  
                JOIN Category c on pc.CategoryId = c.CategoryId
                JOIN MerchantSummary ms on ms.MerchantId = (select MerchantId from MerchantProductSummary where ProductId = ps.ProductId limit 1)
                join MerchantProductSummary mps on mps.VariationId = (select VariationId from MerchantProductSummary mps2 where mps2.ProductId = ps.ProductId order by Price limit 1)
                WHERE c.CategoryId = @categoryId or  c.ParentId = @categoryId
                {0} LIMIT @offset, @pageSize";
        

        [Sql(@"select ps.ProductId, ps.ProductName, ps.Brand, ps.UrlSlug, ps.Description, ps.Rating, mps.Price as MinPrice, mps.PriceRrp as MinPriceRrp, ps.PreviewImageUrl, ps.IsOnSale, ps.Size, ps.Gender, ps.Colour, ps.CurrencyCode, mps.HasStock
                from ProductSummary ps
                join 
	                (select distinct ProductId from 
		                (select PriceRrp - Price as Discount, mps.ProductId
		                from MerchantProductSummary mps
		                join ProductCategory pc on mps.ProductId = pc.ProductId
		                join Category c on c.CategoryId = pc.CategoryId
		                WHERE c.CategoryId = @categoryId or c.ParentId = @categoryId
		                order by (PriceRrp - Price) desc
		                limit 2000) t) t2 on t2.ProductId = ps.ProductId
				join MerchantProductSummary mps on mps.VariationId = (select VariationId from MerchantProductSummary mps2 where mps2.ProductId = ps.ProductId order by Price limit 1)
                order by (MinPriceRrp - MinPrice) desc, IsOnSale desc, Rating desc
                limit 20;")]
        public abstract Task<List<ProductSummary>> GetTopDealsByCategoryId(int categoryId);

        [Sql(@"select ps.ProductId, ps.ProductName, ps.Brand, ps.UrlSlug, ps.Description, ps.Rating, mps.Price as MinPrice, mps.PriceRrp as MinPriceRrp, ps.PreviewImageUrl, ps.IsOnSale, ms.MerchantName, mps.Size, mps.Gender, mps.Colour, ps.CurrencyCode
                from ProductSummary ps
                join MerchantProductSummary mps on ps.ProductId = mps.ProductId
                JOIN ProductCategory pc on pc.ProductId = ps.ProductId                  
                JOIN Category c on pc.CategoryId = c.CategoryId
                JOIN MerchantSummary ms on ms.MerchantId = (select MerchantId from MerchantProductSummary where ProductId = ps.ProductId limit 1)                
                WHERE c.CategoryId = @categoryId or  c.ParentId = @categoryId
                Order by Rating desc, MinPrice desc;")]
        public abstract Task<List<ProductSummary>> GetByCategoryId(string categoryId);

        [Sql(@"SELECT ps.ProductId, ps.ProductName, ps.Brand, ps.UrlSlug, ps.Description, ps.Rating, ps.MinPrice, ps.PreviewImageUrl, ps.IsOnSale, ps.Size, ps.Gender, ps.Colour, ps.CurrencyCode
                FROM ProductSummary ps
                WHERE ps.ProductName like @keywords")]
        public abstract Task<List<ProductSummary>> GetByKeywords(string keywords);

    }
}
