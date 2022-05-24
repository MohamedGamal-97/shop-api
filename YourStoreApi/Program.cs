using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourStoreApi;
using StackExchange.Redis;
using YourStoreApi.Context;
using YourStoreApi.Errors;
using YourStoreApi.Middleware;
using YourStoreApi.Models;
using YourStoreApi.Services;
using YourStoreApi.Services.Helpers;
using System.Net;  
using System.Net.Mail;
using System.Net.Mime;
using StackExchange.Redis;
using ServiceStack.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("con")));
// builder.Services.AddSingleton<IConnectionMultiplexer>(c=>{
//     var configration=ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"),true);
//     return ConnectionMultiplexer.Connect(configration);
// });
// builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
builder.Services.AddScoped<IFavouriteRepository, FavouriteRepository>();
builder.Services.AddDbContext<AppIdentityContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Identitycon")));
// builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling =
Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage).ToArray();

        var errorResponse = new ApiValidationErrorResponse
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});


builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
    });
});

builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), false);
    return ConnectionMultiplexer.Connect(configuration);
});



var build=builder.Services.AddIdentityCore<AppUser>();
build=new IdentityBuilder(build.UserType,build.Services);
build.AddEntityFrameworkStores<AppIdentityContext>();
build.AddSignInManager<SignInManager<AppUser>>(); 

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });
var app = builder.Build();
IConfiguration configuration = app.Configuration;


// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//MailAddress to = new MailAddress("bido777adel@gmail.com");  
//MailAddress from = new MailAddress("bido777adel@gmail.com");  

//MailMessage message = new MailMessage(from, to);  
//message.Subject = "Good morning, Elizabeth";  
//message.Body = "Elizabeth, Long time no talk. Would you be up for lunch in Soho on Monday? I'm paying.;";  

//SmtpClient client = new SmtpClient("smtp.server.address", 2525)  
//{  
//    Credentials = new NetworkCredential("smtp_username", "smtp_password"),  
//    EnableSsl = true  
//};  
//// code in brackets above needed if authentication required   

//try  
//{    
//  client.Send(message);  
//}  
//catch (SmtpException ex)  
//{  
//  Console.WriteLine(ex.ToString());  
//}  
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

//app.Run();
using var scope = app.Services.CreateScope();

    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(context, loggerFactory);
    

    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var IdentityContext = services.GetRequiredService<AppIdentityContext>();
    await IdentityContext.Database.MigrateAsync();
    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
}
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occured during migration");
    }

await app.RunAsync();
