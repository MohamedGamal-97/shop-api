﻿//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using YourStoreApi.Context;

//namespace YourStoreApi.Extensions
//{
    
//    public static class IdentityServiceExtensions
//    {
//        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
//            IConfiguration config, IdentityBuilder builder)
//        {
            

//            //builder = new IdentityBuilder(builder.UserType, builder.Services);
//            //builder.AddEntityFrameworkStores<AppIdentityContext>();
//            //builder.AddSignInManager<SignInManager<AppUser>>();

//            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//            //    .AddJwtBearer(options =>
//            //    {
//            //        options.TokenValidationParameters = new TokenValidationParameters
//            //        {
//            //            ValidateIssuerSigningKey = true,
//            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
//            //            ValidIssuer = config["Token:Issuer"],
//            //            ValidateIssuer = true,
//            //            ValidateAudience = false
//            //        };
//            //    });

//            return services;
//        }
//    }
//}

