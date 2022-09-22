using Catalog.API.Core.Entities;

namespace Catalog.Core.Settings
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ProductsCollectionName { get; set; }
    }
    public class DatabaseSettings : IDatabaseSettings
    {
        public const string SectionName = "DatabaseSettings";
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ProductsCollectionName { get; set; }
    }
}