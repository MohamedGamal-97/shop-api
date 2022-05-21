using AutoMapper;
using YourStoreApi.Models;
using YourStoreApi.Models.Dto;
namespace YourStoreApi.Services.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
               .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                // .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.SubCategory.Category.Name))
                .ForMember(d => d.SubCategory, o => o.MapFrom(s => s.SubCategory.Name))
                .ForMember(d => d.Color, o => o.MapFrom(s => s.Color.Name))
                .ForMember(d => d.Size, o => o.MapFrom(s => s.Size.Name))
                .ForMember(d => d.ProductFeatures, o => o.MapFrom(s => s.ProductFeatures.Select(pf=>pf.feature)))
               .ForMember(d => d.PictureUrl, o => o.MapFrom(
                   s => s.ProductImages.Select(n=>"https://localhost:7260/" +n.PictureUrl)
               ));

            CreateMap<Category, CategoryToReturnDto>()
           .ForMember(d => d.SubCategory, o => o.MapFrom(s => s.SubCategory.Select(s => s.Name).ToList()));

            CreateMap<SubCategory, SubCategoryToReturnDto>()
            .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name));

        }
    }
}
