using Insight.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace product_search.api.Categories
{
    public abstract class CategoryService
    {
        [Sql("select CategoryId, Title, ParentId, Position, Description, IsEnabled, UrlSlug, CategoryBannerImage from Category where IsEnabled = 1")]
        public abstract Task<IList<Category>> GetAllCategoriesAsync();
        
    }
}
