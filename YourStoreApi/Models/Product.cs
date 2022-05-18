using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourStoreApi.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductImages> ProductImages { get; set; } = new List<ProductImages>();
        public int Quantity { get; set; } = 1;

        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
        public DateTime Date { get; set; }
        public int AverageRating { get; set; } = 0;
        public int RateCount { get; set; } = 0;
        public int CustomerBuy { get; set; } = 0;
        public int Sale { get; set; } = 0;


        public int SubCategory_Id { get; set; }
        public SubCategory SubCategory { get; set; }
        public int? Color_Id { get; set; }
        public Colour Color { get; set; }
        public int? Size_Id { get; set; }
        public Size Size { get; set; }

        public ICollection<ProductFeature> ProductFeatures { get; set; } = new List<ProductFeature>();
        public ICollection<Review> ProductReviews { get; set; }=new List<Review>();


    }
}
