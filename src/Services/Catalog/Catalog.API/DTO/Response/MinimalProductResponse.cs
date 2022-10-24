namespace Catalog.API.DTO.Response
{
    public class MinimalProductResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}