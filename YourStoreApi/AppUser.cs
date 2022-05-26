using Microsoft.AspNetCore.Identity;

namespace YourStoreApi
{
    public class AppUser : IdentityUser
    {
        //override
       //public string DisplayName { get; set; }
       override
       public string UserName { get; set; }
        public Address Address { get; set; }
        public string? Role { get; set; }
    }

    
}
