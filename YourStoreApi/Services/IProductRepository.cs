using YourStoreApi.Models;

namespace YourStoreApi.Services
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<IReadOnlyList<ProductBrand>> GetProductBrands();
        Task<IReadOnlyList<ProductType>> GetProductTypes();

    }
}
