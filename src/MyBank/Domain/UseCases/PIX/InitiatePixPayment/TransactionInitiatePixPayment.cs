using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.Core.Models.Response;

namespace Domain.UseCases.PIX.InitiatePixPayment
{
    public record TransactionInitiatePixPayment : BaseTransaction<BaseReturn<PixPayResponse>>
    {
       public Account Account1 { get; set; }
        public ExternalAccount Account2 { get; set; }

        public decimal Amount { get; set; }

        public TransactionInitiatePixPayment(ExternalAccount account2, decimal amount )
        {
            this.Account2 = account2;
            this.Amount = amount;
        }

        public TransactionInitiatePixPayment(Guid id, Account account1, ExternalAccount account2, decimal amount)
        {
            this.Account1 = account1;
            this.Account2 = account2;
            this.Amount = amount;
        }
    }
}
