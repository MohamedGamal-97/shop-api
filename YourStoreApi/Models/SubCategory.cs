using System.ComponentModel.DataAnnotations.Schema;
using YourStoreApi.Models;

namespace YourStoreApi.Models
{
    public class SubCategory:BaseEntity
    {
        
       public string Name { get; set; }
       [ForeignKey("Category")]
       public int Category_Id { get; set; }
       
        public Category Category { get; set; }
    }
}