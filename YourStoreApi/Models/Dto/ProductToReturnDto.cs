
namespace YourStoreApi.Models.Dto
{

    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> PictureUrl { get; set; }
        public string ProductBrand { get; set; }
        public int CustomerBuy { get; set; }=0;
        public DateTime Date { get; set; }
        public int AverageRating { get; set; } = 0;
        public int RateCount { get; set; } = 0;
        public int Quantity { get; set; } = 1;
        public string Color { get; set; }
        //  public string ProductType { get; set; }
        public string Size { get; set; }
        public int Sale { get; set; } = 0;
        public string SubCategory { get; set; }
        public string Category { get; set; }

        public List<string> ProductFeatures { get; set; } 
        public List<Review> ProductReviews { get; set; } 
    }
}


