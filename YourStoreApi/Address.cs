using System.ComponentModel.DataAnnotations;

namespace YourStoreApi
{
    public class Address
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
       [Required]
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

    }
}
