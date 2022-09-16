using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task Create(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task Delete(string id)
        {
            await _context.Products.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Category, category);
            return await _context.Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            return await _context.Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<bool> Update(Product product)
        {
            var updateResult = await _context.Products
                .ReplaceOneAsync(x => x.Id == product.Id, product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
