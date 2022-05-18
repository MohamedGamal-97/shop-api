namespace YourStoreApi.Models
{
    public class FavouriteItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public int Sale { get; set; } = 0;
public string SubCategory { get; set; }
    }
}