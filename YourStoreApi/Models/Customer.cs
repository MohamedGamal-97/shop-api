namespace YourStoreApi.Models
{
    public class Customer:BaseEntity
    {
        public string name { get; set; }
        public ICollection<Review> Reviews { get; set; }=new List<Review>();
    }
}