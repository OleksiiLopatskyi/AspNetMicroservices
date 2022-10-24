using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Core.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.DAL
{
    public class Repository<TDocument> : IRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> Collection;

        public Repository(ICatalogContext<TDocument> catalogContext, IDatabaseSettings databaseSettings) 
        {
            Collection = catalogContext.Database.GetCollection<TDocument>($"{typeof(TDocument).Name}s");
        }

        public IQueryable<TDocument> AsQueryable() => Collection.AsQueryable();

        public async Task DeleteByIdAsync(string id)
        {
            await Collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            await Collection.DeleteManyAsync(filterExpression);
        }

        public async Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            await Collection.DeleteOneAsync(filterExpression);
        }

        public IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Collection.Find(filterExpression).ToEnumerable();
        }

        public IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return Collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public async Task<IEnumerable<TDocument>> FindAllAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return await Collection.Find(filterExpression).ToListAsync();
        }

        public async Task<TDocument> FindByIdAsync(string id)
        {
            return await Collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return await Collection.Find(filterExpression).FirstOrDefaultAsync();
        }

        public async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await Collection.InsertManyAsync(documents);
        }

        public async Task InsertOneAsync(TDocument document)
        {
            await Collection.InsertOneAsync(document);
        }

        public async Task ReplaceOneAsync(TDocument document)
        {
            await Collection.ReplaceOneAsync(x => x.Id == document.Id, document);
        }
    }
}