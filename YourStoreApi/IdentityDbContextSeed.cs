﻿using Microsoft.AspNetCore.Identity;

namespace YourStoreApi
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Bobbity",
                        street = "10 The street",
                        City = "New York",
                        State = "NY",
                        ZipCode = "90210"
                    }
                    
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
