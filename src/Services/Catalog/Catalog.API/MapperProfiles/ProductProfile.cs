using AutoMapper;
using Catalog.API.DTO.Request;
using Catalog.API.DTO.Response;
using Catalog.API.Core.Entities;
using Catalog.API.MapperProfiles.Converters;

namespace Catalog.API.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddProductRequest, Product>(MemberList.None);

            CreateMap<UpdateProductRequest, Product>(MemberList.None)
                .ConvertUsing<UpdateProductRequestConverter>();

            CreateMap<Product, MinimalProductResponse>(MemberList.None);
        }
    }
}