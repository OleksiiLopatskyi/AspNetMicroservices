using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities
{
    public abstract class BaseDocument : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public interface IDocument
    {
        string Id { get; set; }
        DateTime CreatedAt { get; set; }
    }
}