using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.UseCases.PIX.GetPixKey;
using Domain.UseCases.Security.AuthTransaction;

namespace Domain.Core.Models.Response
{
    public record PixKeyResponse: BaseTransactionResponse
    {

       
        public ExternalAccount Account2;
        public string Key { get; set; } = string.Empty;


        public PixKeyResponse(ExternalAccount account, TransactionGetPixKey transaction)
        {
            this.Account2 = new ExternalAccount(account.BankNumber, account.AgencyNumber, account.AccountNumber, account.Cpf, transaction.Key, transaction.Type);
            this.Key = transaction.Key;
            this.CorrelationId = transaction.CorrelationId;
        }
    }
}
