using Domain.Core.Entity;
using System.Transactions;

namespace Domain.Core.Interfaces.Domain
{
    public interface IAccountService
    {
        Task<Account?> GetAccountByCpfAsync(string cpf);
        Task<Account?> GetAccountAsync( int agency, string accountNumber, int bank=1);

        Task<decimal> GetBalanceAsync(int agency, string accountNumber, int bank =1);
        Task<List<BankTransaction>> GetTransactionsAsync(string cpf);
        Task AddTransactionAsync(string cpf, BankTransaction transaction);
        Task<User> GetUser(int agency, string accountNumber);

    }
}
