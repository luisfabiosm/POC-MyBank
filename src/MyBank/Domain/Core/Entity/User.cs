namespace Domain.Core.Entity
{
    public record User
    {
        public string Cpf { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string AccessPassword { get; set; } = string.Empty;
        public string CardPassword { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
