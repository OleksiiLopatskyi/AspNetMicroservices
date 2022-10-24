namespace Catalog.Core.Models
{
    public class ErrorMessage
    {
        public string Message { get; set; }

        public string PropertyName { get; set; } = null;

        public ErrorMessage()
        {
        }

        public ErrorMessage(string message, string propertyName = null)
        {
            Message = message;
            PropertyName = propertyName;
        }
    }
}
