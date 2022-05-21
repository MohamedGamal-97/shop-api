using System.ComponentModel.DataAnnotations.Schema;

namespace YourStoreApi.Models
{
    public class Review:BaseEntity
    {
        public int Customer_Id { get; set; }
        // [foriegnKey('Customer_Id')]
        [ForeignKey("Customer_Id")]
        public Customer Customer { get; set; }
        
        public int Product_Id { get; set; }
        [ForeignKey("Product_Id")]
        public Product Product { get; set; }
        public int star { get; set; }
        public string review { get; set; }
        public string head { get; set; }
        public string name { get; set; }
        public DateTime Date { get; set; }
    }

  
}