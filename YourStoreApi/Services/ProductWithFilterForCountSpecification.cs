using YourStoreApi.Models;

namespace YourStoreApi.Services
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams productParams)
            :base(p =>
            (string.IsNullOrEmpty(productParams.Search)
             || p.Name.ToLower().Contains(productParams.Search)
             ||p.SubCategory.Name.ToLower().Contains(productParams.Search)
             ||p.SubCategory.Category.Name.ToLower().Contains(productParams.Search)
             ||p.ProductBrand.Name.ToLower().Contains(productParams.Search)
             ) &&
            (string.IsNullOrEmpty(productParams.Brand) || productParams.Brand.Contains(p.ProductBrand.Name)) &&
            (!productParams.Sale.HasValue || p.Sale >= productParams.Sale) &&
            (!productParams.Star.HasValue || p.AverageRating >= productParams.Star) &&
            (productParams.MinPrice <= (p.Price * (Convert.ToDecimal(100 - p.Sale) / 100)) && productParams.MaxPrice >= (p.Price * (Convert.ToDecimal(100 - p.Sale) / 100)))
            &&
             (string.IsNullOrEmpty(productParams.Color) || productParams.Color.Contains(p.Color.Name)) &&
            (string.IsNullOrEmpty(productParams.Size) || productParams.Size.Contains(p.Size.Name)) &&
            (!productParams.CategoryId.HasValue || p.SubCategory.Category_Id == productParams.CategoryId) &&
           (productParams.SubCategory == null || p.SubCategory.Name == productParams.SubCategory)
            )
        {

        }

    }
}
