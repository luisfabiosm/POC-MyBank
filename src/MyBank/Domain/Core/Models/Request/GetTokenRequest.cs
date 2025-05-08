namespace Domain.Core.Models.Request
{
    public record GetTokenRequest
    {
        public string Cpf { get; set; }
        public string Password { get; set; }

        public GetTokenRequest(string cpf, string password)
        {
            Cpf = cpf;
            Password = password;
        }

    }
}
