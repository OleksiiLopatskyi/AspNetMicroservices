using AutoMapper;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.BAL
{
    public class BaseService<TDocument> : IBaseService<TDocument> where TDocument : IDocument
    {
        protected readonly IRepository<TDocument> Repository;
        protected readonly IMapper Mapper;

        public BaseService(IRepository<TDocument> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task CreateAsync<TRequest>(TRequest request)
        {
            var document = Mapper.Map<TDocument>(request);
            await Repository.InsertOneAsync(document);
        }

        public virtual async Task DeleteAsync(string id)
        {
            var document = await FindByIdAsync<TDocument>(id);
            await Repository.DeleteByIdAsync(document.Id);
        }

        public virtual async Task<IEnumerable<TResult>> FindAllAsync<TResult, TFilterParams>(TFilterParams filterParams)
        {
            var documents = await Repository.FindAllAsync(x => true);
            return Mapper.Map<IEnumerable<TResult>>(documents);
        }

        public virtual async Task<TResult> FindByIdAsync<TResult>(string id)
        {
            var document = await Repository.FindByIdAsync(id);
            if (document == null)
            {
                throw new KeyNotFoundException($"Document with id:{id} not found");
            }
            return Mapper.Map<TResult>(document);
        }

        public virtual async Task UpdateAsync<TRequest>(string id, TRequest request)
        {
            var document = await FindByIdAsync<TDocument>(id);
            Mapper.Map(request, document);
            await Repository.ReplaceOneAsync(document);
        }
    }
}