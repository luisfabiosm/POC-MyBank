using Domain.Core.Base;
using Domain.Core.Models.Response;

namespace Domain.UseCases.Accounts.GetBalance
{
    public record TransactionGetBalance : BaseTransaction<BaseReturn<BalanceResponse>>
    {
        public int BankNumber { get; set; }
        public int AgencyNumber { get; set; }
        public string AccountNumber { get; set; } = string.Empty;


        public TransactionGetBalance(int bank, int agency, string accountNumber)
        {
            BankNumber = bank;
            AgencyNumber = agency;
            AccountNumber = accountNumber;
        }

    }
}
