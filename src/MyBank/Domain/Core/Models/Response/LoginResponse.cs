using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.UseCases.Security.AuthTransaction;
using Domain.UseCases.Security.LoginAccount;

namespace Domain.Core.Models.Response
{

    public record LoginResponse : BaseTransactionResponse
    {
        public string Token { get; set; } 
        public DateTime Expiration { get; set; }
        public string Name { get; set; } 
        public string Cpf { get; set; } 

        public int BankNumber { get; set; }
        public int AgencyNumber { get; set; } 
        public string AccountNumber { get; set; }

        public LoginResponse()
        {
            
        }
        public LoginResponse(User user, Account account, TransactionLoginAccount transaction)
        {
            Token = "";
            Expiration = DateTime.Now;
            Name = user.Name;
            Cpf = transaction.Cpf;
            BankNumber = account.BankNumber;
            AgencyNumber = account.AgencyNumber;
            AccountNumber = account.AccountNumber;

        }

    }


}
