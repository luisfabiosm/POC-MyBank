using Domain.Core.Base;
using Domain.Core.Models.Response;

namespace Domain.UseCases.Security.GetToken
{
    public record TransactionGetToken : BaseTransaction<BaseReturn<GetTokenResponse>>
    {
        public string Cpf { get; set; }
        public string Password { get; set; }


        public TransactionGetToken()
        {
            
        }
        public TransactionGetToken(string cpf, string password)
        {
            Cpf = cpf;
            Password = password;
        }

    }
}
