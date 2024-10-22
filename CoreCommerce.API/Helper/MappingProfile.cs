using AutoMapper;
using CoreCommerce.API.Dtos;
using CoreCommerce.Core.Entities;

namespace CoreCommerce.API.Helper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
               .ForMember(d => d.ProductCategory, o => o.MapFrom(s => s.ProductCategory.Name));
        CreateMap<ProductCreateDto, Product>();
    }

}
