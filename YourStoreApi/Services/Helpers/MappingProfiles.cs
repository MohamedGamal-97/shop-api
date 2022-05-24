using AutoMapper;
using YourStoreApi.Models;
using YourStoreApi.Models.Dto;
using YourStoreApi.Models.OderAggregate;

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

            CreateMap<YourStoreApi.Address, AddressDto>().ReverseMap();

            CreateMap<AddressDto, YourStoreApi.Models.OderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());

           



            

        }
    }
}
