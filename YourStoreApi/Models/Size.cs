using System.ComponentModel.DataAnnotations.Schema;
using YourStoreApi.Models;

namespace YourStoreApi.Models
{
    public class Size:BaseEntity
    {
        public string Name { get; set; }
    }
}