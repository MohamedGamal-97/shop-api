
namespace YourStoreApi.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public int SubCategory_Id { get; set; }
        public ICollection<SubCategory> SubCategory { get; set; }=new List<SubCategory>();
    }
}