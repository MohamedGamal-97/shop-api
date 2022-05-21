using YourStoreApi.Models;

namespace YourStoreApi.Services
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {

        public ProductsWithTypesAndBrandsSpecification(int id) : base(p => id == p.Id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.SubCategory);
            // AddInclude(p => p.Category);
            AddInclude(p => p.ProductImages);
            AddInclude(p => p.ProductFeatures);
            AddInclude(p => p.ProductReviews);
            AddInclude(p => p.Color);
            AddInclude(p => p.Size);
        }



        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
        : base(p =>
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
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.SubCategory);
            // AddInclude(p => p.Category);
            AddInclude(p => p.ProductImages);
            AddInclude(p => p.Color);
            AddInclude(p => p.Size);

            AddOrderBy(p => p.Name);

            ApplyPagintion(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);
            // ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    case "date":
                        AddOrderByDescending(p => p.Date);
                        break;
                    case "star":
                        AddOrderByDescending(p => p.AverageRating);
                        break;
                    case "arrival":
                        AddOrderByDescending(p => p.Sale);
                        break;
                    case "selling":
                        AddOrderByDescending(p => p.CustomerBuy);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }

        }
    }
}
