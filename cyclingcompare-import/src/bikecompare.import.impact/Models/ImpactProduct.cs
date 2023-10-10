using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace bikecompare.import.impact.Models.Impact
{
    [XmlRoot(ElementName = "Products", IsNullable = true)]
    public class ImpactProducts
    {
        [XmlElement(ElementName = "Product")]
        
        public ImpactProduct[] Products { get; set; }
    }

    public class ImpactProduct
    {
        public string Unique_Merchant_SKU { get; set; }
        public string Product_Name { get; set; }
        public string Product_URL { get; set; }
        public string Image_URL { get; set; }
        public string Alternative_Image_URL_1 { get; set; }
        public decimal Current_Price { get; set; }
        public decimal Original_Price { get; set; }
        public string Stock_Availability { get; set; }
        public string Condition { get; set; }
        public string EAN { get; set; }
        public string MPN { get; set; }
        public string UPC { get; set; }
        public decimal Shipping_Rate { get; set; }
        public string Manufacturer { get; set; }
        public string Product_Description { get; set; }
        public string Product_Type { get; set; }
        public string Category { get; set; }
        public string Parent_SKU { get; set; }
        public string Gender { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Age_Range { get; set; }
        public float Product_Weight { get; set; }
        public float Shipping_Weight { get; set; }
        public string Weight_Unit_of_Measure { get; set; }
        public string Bundle { get; set; }
        public string Currency { get; set; }
        public string Shipping_Region { get; set; }

    }
}
