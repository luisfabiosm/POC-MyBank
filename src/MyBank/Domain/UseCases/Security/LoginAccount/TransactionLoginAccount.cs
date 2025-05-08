using Domain.Core.Base;
using Domain.Core.Models.Response;

namespace Domain.UseCases.Security.LoginAccount
{
    public record TransactionLoginAccount : BaseTransaction<BaseReturn<LoginResponse>>
    {
        public string Cpf { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public TransactionLoginAccount(string cpf, string password)
        {
            Cpf = cpf;
            Password = password;
        }

    }

}
