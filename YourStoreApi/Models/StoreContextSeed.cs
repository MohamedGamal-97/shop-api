using System.Text.Json;
using YourStoreApi.Context;

namespace YourStoreApi.Models
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("./Models/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
    //    if (!context.ProductTypes.Any())
    //             {
    //                 var typesData = File.ReadAllText("./Models/SeedData/types.json");
    //                 var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
    //                 foreach (var item in types)
    //                 {
    //                     context.ProductTypes.Add(item);
    //                 }
    //                 await context.SaveChangesAsync();
    //             }
                if (!context.Categories.Any())
                {
                    var CategoriesData = File.ReadAllText("./Models/SeedData/category.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(CategoriesData);

                    foreach (var item in categories)
                    {
                        context.Categories.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.SubCategories.Any())
                {
                    var subCategoriessData = File.ReadAllText("./Models/SeedData/subCategory.json");
                    var subCategories = JsonSerializer.Deserialize<List<SubCategory>>(subCategoriessData);

                    foreach (var item in subCategories)
                    {
                        context.SubCategories.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.Colors.Any())
                {
                    var ColorsData = File.ReadAllText("./Models/SeedData/Color.json");
                    var Colors = JsonSerializer.Deserialize<List<Colour>>(ColorsData);

                    foreach (var item in Colors)
                    {
                        context.Colors.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.Sizes.Any())
                {
                    var SizesData = File.ReadAllText("./Models/SeedData/Size.json");
                    var Sizes = JsonSerializer.Deserialize<List<Size>>(SizesData);

                    foreach (var item in Sizes)
                    {
                        context.Sizes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
               
           
                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("./Models/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
               
                 if (!context.ProductImages.Any())
                {
                    var productImagesData = File.ReadAllText("./Models/SeedData/productImage.json");
                    var ProductImages = JsonSerializer.Deserialize<List<ProductImages>>(productImagesData);

                    foreach (var item in ProductImages)
                    {
                        context.ProductImages.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                      if (!context.ProductFeatures.Any())
                {
                    var ProductFeaturesData = File.ReadAllText("./Models/SeedData/prodeuctFeature.json");
                    var ProductFeatures = JsonSerializer.Deserialize<List<ProductFeature>>(ProductFeaturesData);

                    foreach (var item in ProductFeatures)
                    {
                        context.ProductFeatures.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }

    }
}
