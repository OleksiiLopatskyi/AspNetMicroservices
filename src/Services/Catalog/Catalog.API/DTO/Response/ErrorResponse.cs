using System.Text.Json.Serialization;

namespace Catalog.API.DTO.Response
{
    public record ErrorResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; init; }

        public ErrorResponse()
        {
        }

        public ErrorResponse(string message)
        {
            Message = message;
        }
    }
}