using System.ComponentModel.DataAnnotations.Schema;

namespace YourStoreApi.Models
{
    public class ProductFeature:BaseEntity
    {
        public string feature { get; set; }
           public int ProductId { get; set; }
            [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}