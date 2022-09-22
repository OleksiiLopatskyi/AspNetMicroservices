using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Core.Interfaces
{
    public interface ICatalogContext<TDocument> where TDocument : IDocument
    {
        IMongoDatabase Database { get; }
    }
}