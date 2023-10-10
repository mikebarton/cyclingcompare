namespace product_search.api.Categories
{
    public class Category
    {
        public string CategoryId { get; set; }
        public string Title { get; set; }
        public string ParentId { get; set; }
        public int Position { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public string UrlSlug { get; set; }
        public string CategoryBannerImage { get; set; }

    }
}
