using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.admin.api.Product
{
    public abstract partial class ProductService
    {        

        [Sql(@"select ps.ProductId, pc.CategoryId, ps.Size, ps.Colour, ps.Gender, ps.PreviewImageUrl
	            from ProductSummary ps
	            join ProductCategory pc on ps.ProductId = pc.ProductId
	            join Category c on pc.CategoryId = c.CategoryId
	            where (c.CategoryId = @categoryId or c.ParentId = @categoryId);")]
        public abstract Task<List<Product>> GetProductsByCategory(string categoryId);
    }
}

