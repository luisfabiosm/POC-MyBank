using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.UseCases.Accounts.GetBalance;
using Domain.UseCases.Accounts.GetStatement;

namespace Domain.Core.Models.Response
{
    public record StatementResponse : BaseTransactionResponse
    {
        public int AgencyNumber { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public decimal CurrentBalance { get; set; }
        public List<TransactionItem> Transactions { get; set; } = new List<TransactionItem>();


        public StatementResponse()
        {
            
        }

        //public StatementResponse(User user, Account account,  TransactionGetStatement transaction)
        //{
        //    this.AgencyNumber = transaction.AgencyNumber;
        //    this.AccountNumber = transaction.AccountNumber;
        //    this.Cpf = user.Cpf;
        //    this.Name = user.Name;
        //    this.Balance = account.Balance;
        //    this.Transactions = account.Transactions;
        //    this.CorrelationId = transaction.CorrelationId;
        //}
    }
}
