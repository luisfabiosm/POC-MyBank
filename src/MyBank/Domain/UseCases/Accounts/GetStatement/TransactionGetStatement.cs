using Domain.Core.Base;
using Domain.Core.Models.Response;

namespace Domain.UseCases.Accounts.GetStatement
{
    public record TransactionGetStatement : BaseTransaction<BaseReturn<StatementResponse>>
    {
        public int BankNumber { get; set; }
        public int AgencyNumber { get; set; }
        public string AccountNumber { get; set; } = string.Empty;

        public TransactionGetStatement(int bank, int agency, string accountNumber)
        {
            BankNumber = bank;
            AgencyNumber = agency;
            AccountNumber = accountNumber;
        }
    }
}
