using Insight.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace product_search.api.Categories
{
    public abstract class CategoryService
    {
        [Sql("select CategoryId, Title, ParentId, Position, Description, IsEnabled, UrlSlug, CategoryBannerImage from Category where IsEnabled = 1")]
        public abstract Task<IList<Category>> GetAllCategoriesAsync();

        [Sql("update Category set Title = @title, ParentId = @parentId, Position = @position, Description = @description, IsEnabled = @isEnabled, UrlSlug = @urlSlug where CategoryId = @categoryId")]
        public abstract Task UpdateCategory(string categoryId, string title, string parentId, int position, string description, bool isEnabled, string urlSlug);

        [Sql("delete from Category where CategoryId = @categoryId")]
        public abstract Task DeleteCategory(string categoryId);

        [Sql("delete from ProductCategory where CategoryId = @categoryId")]
        public abstract Task DeleteProductCategoryMapping(string categoryId);


        [Sql("insert into Category (CategoryId, Title, ParentId, Position, Description, IsEnabled, UrlSlug, CategoryBannerImage) values (@categoryId, @title, @parentId, @position, @description, @isEnabled, @urlSlug, @categoryBannerImage)")]
        public abstract Task InsertCategory(string categoryId, string title, string parentId, int position, string description, bool isEnabled, string urlSlug, string categoryBannerImage);
    }
}
