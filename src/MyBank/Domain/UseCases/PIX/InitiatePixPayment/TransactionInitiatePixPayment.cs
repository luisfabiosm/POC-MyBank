using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.Core.Models.Response;

namespace Domain.UseCases.PIX.InitiatePixPayment
{

    public record TransactionInitiatePixPayment : BaseTransaction<BaseReturn<PixPayResponse>>
    {
        public int BankNumber { get; set; }
        public int AgencyNumber { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public ExternalAccount RemoteAccount { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public TransactionInitiatePixPayment(int sourceBank, int sourceAgency, string sourceAccount, ExternalAccount remoteAccount, decimal amount, string descripition )
        {
            this.BankNumber = sourceBank;
            this.AgencyNumber = sourceAgency;
            this.AccountNumber = sourceAccount;

            this.RemoteAccount = remoteAccount;
            this.Amount = amount;
            this.Description = descripition;
        }

        public TransactionInitiatePixPayment()
        {
            
        }


    }
}
