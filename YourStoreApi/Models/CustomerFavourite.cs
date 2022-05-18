namespace YourStoreApi.Models
{
    public class CustomerFavourite
    {
         public CustomerFavourite()
        {
        }

        public CustomerFavourite(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public List<FavouriteItem> Items { get; set; } = new List<FavouriteItem>();
    }
}