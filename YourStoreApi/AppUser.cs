using Microsoft.AspNetCore.Identity;

namespace YourStoreApi
{
    public class AppUser : IdentityUser
    {
        //override
       public string DisplayName { get; set; }
        public Address Address { get; set; }
    }

    
}
