using Domain.Core.Base;
using Domain.Core.Models.Response;

namespace Domain.UseCases.Security.AuthTransaction
{
    public record TransactionAuthTransaction : BaseTransaction<BaseReturn<AuthTransactionResponse>>
    {
        public int AgencyNumber { get; set; } 
        public string AccountNumber { get; set; } = string.Empty;
        public string CardPassword { get; set; } = string.Empty;
        public string OriginaLTransactionId { get; set; } = string.Empty;

        public TransactionAuthTransaction(int agency, string accountNumber, string cardpassword, string originalId)
        {
            AgencyNumber = agency;
            AccountNumber = accountNumber;
            CardPassword = cardpassword;
            OriginaLTransactionId = originalId;
        }



    }
}
