using AutoMapper;
using Catalog.API.Core.Entities;
using Catalog.API.DTO.Request;
using Catalog.API.DTO.Response;
using Catalog.Core.Models;

namespace Catalog.API.MapperProfiles
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<Product, MinimalProductResponse>();

            CreateMap<ProductFilterParameters, ProductFilterParametersModel>();
        }
    }
}
