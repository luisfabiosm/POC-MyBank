using Adapters.Outbound.Database.InMemory;
using Domain.Core.Entity;
using Domain.Core.Interfaces.Domain;
using Microsoft.IdentityModel.Tokens;
using System.Transactions;

namespace Domain.Services
{
    public class AccountService : IAccountService
    {

        private readonly InMemoryDatabase _database;

        public AccountService(InMemoryDatabase database)
        {
            _database = database;
        }

        public async Task<Account?> GetAccountByCpfAsync(string cpf)
        {
            return await _database.GetAccountByCpfAsync(cpf);
        }

        public async Task<Account> GetAccountAsync(int agency, string accountNumber, int bank=1)
        {
            return await _database.GetAccountAsync(agency, accountNumber, bank);
        }


        public async Task<decimal> GetBalanceAsync(string cpf)
        {
            var _account = await _database.GetAccountByCpfAsync(cpf);
            return _account?.Balance ?? 0;
        }


        public async Task<List<BankTransaction>> GetTransactionsAsync(string cpf)
        {
            var _account = await _database.GetAccountByCpfAsync(cpf);
            return _account?.Transactions ?? new List<BankTransaction>();
        }


        public async Task AddTransactionAsync(string cpf, BankTransaction transaction)
        {
            await _database.AddTransactionAsync(cpf, transaction);
        }


        public async Task<decimal> GetBalanceAsync(int agency, string accountNumber, int bank =1)
        {
            var _account = await _database.GetAccountAsync(agency, accountNumber, bank);
            return _account?.Balance ?? 0;
        }


        public Task<User> GetUser(int agency, string accountNumber)
        {
            throw new NotImplementedException();
        }
    }
}
