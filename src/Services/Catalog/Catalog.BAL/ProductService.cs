using AutoMapper;
using Catalog.API.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.BAL
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IRepository<Product> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<IEnumerable<TResult>> FindAllAsync<TResult, TFilterParams>(TFilterParams filterParams)
        {
            var productFilterParams = Mapper.Map<ProductFilterParametersModel>(filterParams);
            Expression<Func<Product, bool>> expression = x => true;

            if (productFilterParams.Category != null)
            {
                expression = x => x.Category == productFilterParams.Category;
            }

            return Mapper.Map<IEnumerable<TResult>>(await Repository.FindAllAsync(expression));
        }

        public async Task<IEnumerable<TResult>> GetProductByCategory<TResult>(string category)
        {
            var document = await Repository.FindOneAsync(x => x.Category == category);
            return Mapper.Map<IEnumerable<TResult>>(document);
        }
    }
}
