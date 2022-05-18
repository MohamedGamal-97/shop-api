// using Microsoft.EntityFrameworkCore;
// using YourStoreApi.Context;
// using YourStoreApi.Models;

// namespace YourStoreApi.Services
// {
//     public class ProductRepository : IProductRepository
//     {
//         private readonly StoreContext _context;

//         public ProductRepository(StoreContext context)
//         {
//             _context = context;
//         }
//         public async Task<IReadOnlyList<Product>> GetAllProducts()
//         {
//             return await _context.Products
//                 .Include(p=>p.ProductType)
//                 .Include(p=>p.ProductBrand)
//                 .ToListAsync();
//         }

//         public async Task<IReadOnlyList<ProductBrand>> GetProductBrands()
//         {
//             return await _context.ProductBrands.ToListAsync();
//         }

//         public async Task<Product> GetProductById(int id)
//         {
//             return await _context.Products
//                 .Include(p => p.ProductType)
//                 .Include(p => p.ProductBrand)
//                 .FirstOrDefaultAsync(p => p.Id == id);
//         }

//         // public async Task<IReadOnlyList<ProductType>> GetProductTypes()
//         // {
//         //     return await _context.ProductTypes.ToListAsync();
//         // }

//         public void AddProduct(Product product)
//         {
//             _context.Products.Add(product);
//             _context.SaveChanges();
//         }
//     }
// }
