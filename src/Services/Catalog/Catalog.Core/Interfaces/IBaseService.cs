using Catalog.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Core.Interfaces
{
    public interface IBaseService<TDocument> where TDocument : IDocument
    {
        Task<IEnumerable<TResult>> FindAllAsync<TResult>();
        Task<TResult> FindByIdAsync<TResult>(string id);
        Task DeleteAsync(string id);
        Task UpdateAsync<TRequest>(string id, TRequest request);
        Task CreateAsync<TRequest>(TRequest request);
    }
}