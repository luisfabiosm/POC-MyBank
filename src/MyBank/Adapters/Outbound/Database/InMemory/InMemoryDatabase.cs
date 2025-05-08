using Domain.Core.Entity;
using Domain.Core.Enums;
using Google.Protobuf;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.Transactions;

namespace Adapters.Outbound.Database.InMemory
{
    public class InMemoryDatabase
    {
        private readonly List<User> _users = new();
        private readonly List<Account> _accounts = new();
        private readonly List<ExternalAccount> _externalAccounts = new();

        private readonly Dictionary<string, List<BankTransaction>> _pendingTransactions = new();

        public void Initialize()
        {
            // Dados mockados conforme requisitos
            var user = new User
            {
                Cpf = "97786149031", // Sem formatação para simplificar a busca
                Name = "LUIZ NONO SILVA",
                AccessPassword = "07122526",
                CardPassword = "0321",
                PhoneNumber = "91985758797"
            };


            var account = new Account
            {
                BankNumber = 1,
                AgencyNumber = 1,
                AccountNumber = "25202",
                Cpf = "97786149031",
                Balance = 10000.53m,
                PixKeys = new List<PixKey>
                {
                    new PixKey { Key = "97786149031", Type = PixKeyType.Cpf },
                    new PixKey { Key = "91985758797", Type = PixKeyType.Phone }
                },
                Transactions = new List<BankTransaction>
                {
                    new BankTransaction
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now.AddDays(-5),
                        Amount = 2500.00m,
                        Description = "Salário",
                        Type = TransactionType.Credit
                    },
                    new BankTransaction
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now.AddDays(-3),
                        Amount = 150.75m,
                        Description = "Supermercado",
                        Type = TransactionType.Debit
                    },
                    new BankTransaction
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now.AddDays(-1),
                        Amount = 350.00m,
                        Description = "PIX recebido de José Silva",
                        Type = TransactionType.PixReceived
                    }
                }
            };


            var externalaccount = new ExternalAccount
            {
                BankNumber = 2,
                AgencyNumber = 21,
                AccountNumber = "2547719",
                Cpf = "62566610282",
      
                PixKeys = new List<PixKey>
                {
                    new PixKey { Key = "62566610282", Type = PixKeyType.Cpf },
                    new PixKey { Key = "91981155731", Type = PixKeyType.Phone }
                },
               
            };


            _users.Add(user);
            _accounts.Add(account);
            _externalAccounts.Add(externalaccount);
            _pendingTransactions[user.Cpf] = new List<BankTransaction>();
        }

        public Task<User?> GetUserByCpfAsync(string cpf)
        {
            var user = _users.FirstOrDefault(u => u.Cpf == cpf);
            return Task.FromResult(user);
        }


        public Task<ExternalAccount?> GetExternalAccountByCpfAsync(string myKey)
        {
            var account = _externalAccounts.FirstOrDefault(a => a.PixKeys.Any(p => p.Key == myKey));

            return Task.FromResult(account);
        }

        public Task<Account?> GetAccountByCpfAsync(string cpf)
        {
            var _account = _accounts.FirstOrDefault(a => a.Cpf == cpf);
            return Task.FromResult(_account);
        }


        public Task<Account?> GetAccountAsync(int agency, string accountNumber, int bank = 1)
        {
            var account = _accounts.FirstOrDefault(a => a.BankNumber == bank && a.AgencyNumber == agency && a.AccountNumber == accountNumber);
            return Task.FromResult(account);
        }


        public Task AddTransactionAsync(string cpf, BankTransaction transaction)
        {
            var account = _accounts.FirstOrDefault(a => a.Cpf == cpf);
            if (account != null)
            {
                account.Transactions.Add(transaction);

                // Atualizar saldo com base no tipo de transação
                if (transaction.Type == TransactionType.Credit || transaction.Type == TransactionType.PixReceived)
                {
                    account.Balance += transaction.Amount;
                }
                else
                {
                    account.Balance -= transaction.Amount;
                }
            }

            return Task.CompletedTask;
        }


        public Task AddPendingTransactionAsync(string cpf, BankTransaction transaction)
        {
            if (!_pendingTransactions.ContainsKey(cpf))
            {
                _pendingTransactions[cpf] = new List<BankTransaction>();
            }

            _pendingTransactions[cpf].Add(transaction);
            return Task.CompletedTask;
        }


        public Task<BankTransaction?> GetPendingTransactionAsync(string cpf, Guid transactionId)
        {
            if (!_pendingTransactions.ContainsKey(cpf))
            {
                return Task.FromResult<BankTransaction?>(null);
            }

            var transaction = _pendingTransactions[cpf].FirstOrDefault(t => t.Id == transactionId);
            return Task.FromResult(transaction);
        }


        public Task<bool> ExecuteTransactionAsync(string cpf, BankTransaction transaction)
        {
            var account = _accounts.FirstOrDefault(a => a.Cpf == cpf);
            if (account == null || account.Balance < transaction.Amount)
            {
                return Task.FromResult(false);
            }

            // Adicionar a transação e atualizar o saldo
            account.Transactions.Add(transaction);

            if (transaction.Type == TransactionType.Debit || transaction.Type == TransactionType.PixSent)
            {
                account.Balance -= transaction.Amount;
            }
            else
            {
                account.Balance += transaction.Amount;
            }

            // Remover das transações pendentes, se existir
            if (_pendingTransactions.ContainsKey(cpf))
            {
                var pendingTransaction = _pendingTransactions[cpf].FirstOrDefault(t => t.Id == transaction.Id);
                if (pendingTransaction != null)
                {
                    _pendingTransactions[cpf].Remove(pendingTransaction);
                }
            }

            return Task.FromResult(true);
        }
    }
}
