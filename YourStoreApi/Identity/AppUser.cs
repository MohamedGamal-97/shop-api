using Microsoft.AspNetCore.Identity;
using YourStoreApi;
using YourStoreApi.Models.OderAggregate;

namespace YourStoreApi
{
    public class AppUser : IdentityUser
    {
        //override
       public string DisplayName { get; set; }
        public Address Address { get; set; }
    }

    
}
