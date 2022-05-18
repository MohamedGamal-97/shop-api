using System.ComponentModel.DataAnnotations.Schema;

namespace YourStoreApi.Models
{
    public class ProductImages:BaseEntity
    {
        public string PictureUrl { get; set; }
        public int Product_Id { get; set; }
        [ForeignKey("Product_Id")]

        public Product Product { get; set; }
    }
}