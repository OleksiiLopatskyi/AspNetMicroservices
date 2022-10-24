using Catalog.API.Core.Entities;

namespace Catalog.Core.Settings
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
    public class DatabaseSettings : IDatabaseSettings
    {
        public const string SectionName = "DatabaseSettings";
        public const string ProductsCollectionName = "Products";

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}