using System.Collections.Generic;

namespace product_search.api.Categories
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            Children = new List<CategoryModel>();
        }
        public string CategoryId { get; set; }
        public string Title { get; set; }
        public string ParentId { get; set; }
        public int Position { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public string UrlSlug { get; set; }
        public string CategoryBannerImage { get; set; }
        public IList<CategoryModel> Children { get; set; }

        public bool TryAddChild(CategoryModel model)
        {
            if (model.ParentId == this.CategoryId)
            {
                Children.Add(model);
                return true;
            }
            return false;
        }
    }
}
