namespace Domain.Core.Exceptions
{
    public record ErrorDetails
    {
        public string Message { get; set; }
        public string PropertyName { get; set; }

        public ErrorDetails(string message, string propertyName = null)
        {
            Message = message;
            PropertyName = propertyName;
        }

    }
}
