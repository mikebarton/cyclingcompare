namespace product_search.api.ProductSummaries
{
    public class ProductSummaryModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string UrlSlug { get; set; }
        public short Rating { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MinPriceRrp { get; set; }
        public string PreviewImageUrl { get; set; }
        public bool IsOnSale { get; set; }
        public string Size { get; set; }
        public string Gender { get; set; }
        public string Colour { get; set; }
        public string CurrencyCode { get; set; }
        public bool? HasStock { get; set; }
    }
}
