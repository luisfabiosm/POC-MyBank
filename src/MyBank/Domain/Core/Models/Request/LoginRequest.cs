namespace Domain.Core.Models.Request
{
    public record LoginRequest
    {
        public string Cpf { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
