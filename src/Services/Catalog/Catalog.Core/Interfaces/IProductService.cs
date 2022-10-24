using Catalog.API.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Core.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        Task<IEnumerable<TResult>> GetProductByCategory<TResult>(string category);
    }
}
