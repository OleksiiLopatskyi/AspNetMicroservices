using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Core.Settings;
using MongoDB.Driver;

namespace Catalog.DAL
{
    public class CatalogContext<TDocument> : ICatalogContext<TDocument> where TDocument : IDocument
    {
        public CatalogContext(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            Database = client.GetDatabase(databaseSettings.DatabaseName);
        }

        public IMongoDatabase Database { get; }
    }
}
