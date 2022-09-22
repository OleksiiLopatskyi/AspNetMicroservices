using AutoMapper;
using Catalog.API.Core.Entities;
using Catalog.API.DTO.Request;

namespace Catalog.API.MapperProfiles.Converters
{
    public class UpdateProductRequestConverter : ITypeConverter<UpdateProductRequest, Product>
    {
        public Product Convert(UpdateProductRequest source, Product destination, ResolutionContext context)
        {
            if (source.Name != null)
            {
                destination.Name = source.Name;
            }

            if (source.Description != null)
            {
                destination.Description = source.Description;
            }

            if (source.Summary != null)
            {
                destination.Summary = source.Summary;
            }

            if (source.ImageFile != null)
            {
                destination.ImageFile = source.ImageFile;
            }

            if (source.Price != destination.Price && source.Price != null)
            {
                destination.Price = (decimal)source.Price;
            }

            return destination;
        }
    }
}