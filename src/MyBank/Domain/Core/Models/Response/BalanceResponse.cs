using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.UseCases.Accounts.GetBalance;

namespace Domain.Core.Models.Response
{
    public record BalanceResponse : BaseTransactionResponse
    {
        public int AgencyNumber { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public decimal Balance { get; set; }
        public string FormattedBalance { get; set; } = string.Empty;
        public DateTime LastUpdate { get; set; }

        public BalanceResponse(User user,TransactionGetBalance transaction,  decimal balance )
        {
            this.AgencyNumber = transaction.AgencyNumber;
            this.AccountNumber = transaction.AccountNumber;
            this.Cpf = user.Cpf;
            this.Name = user.Name;
            this.Balance = balance;
            this.CorrelationId = transaction.CorrelationId;
            this.FormattedBalance = $"R$ {balance:N2}";
            this.LastUpdate = DateTime.Now;
        }
    }
}
