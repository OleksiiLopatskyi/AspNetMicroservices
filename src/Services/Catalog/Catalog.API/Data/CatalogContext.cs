using Catalog.API.Entities;
using Catalog.API.Settings;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            Products = database.GetCollection<Product>(databaseSettings.CollectionName);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
