using Adapters.Outbound.Database.InMemory;
using Domain.Core.Entity;
using Domain.Core.Enums;
using Domain.Core.Interfaces.Domain;
using Domain.Core.Models.Request;
using Domain.Core.Models.Response;
using Microsoft.IdentityModel.Tokens;
using System.Transactions;

namespace Domain.Services
{
    public class PixService : IPixService
    {
        private readonly InMemoryDatabase _database;

        public PixService(InMemoryDatabase database)
        {
            _database = database;
        }

        public async Task<List<PixKey>> GetPixKeysAsync(string cpf)
        {
            var account = await _database.GetAccountByCpfAsync(cpf);
            return account?.PixKeys ?? new List<PixKey>();
        }

        public async Task<bool> ConfirmPixTransferAsync(string cpf, Guid transactionId)
        {
            var transaction = await _database.GetPendingTransactionAsync(cpf, transactionId);
            if (transaction == null)
            {
                return false;
            }

            return await _database.ExecuteTransactionAsync(cpf, transaction);
        }

        public async Task<ExternalAccount> EvaluatePixKeysAsync(string key, PixKeyType type)
        {
            var _account = await _database.GetExternalAccountByCpfAsync(key);

            return _account;
        }

        public async Task<PixPayResponse> InitiatePixTransferAsync(Account account1, PixPayRequest request)
        {
            

            if (account1 == null)
            {
                return new PixPayResponse
                {
                    Message = "Conta não encontrada",
                    RequiresAuthentication = false
                };
            }

            if (account1.Balance < request.Amount)
            {
                return new PixPayResponse
                {
                    Message = "Saldo insuficiente",
                    RequiresAuthentication = false
                };
            }

            // Criar transação pendente
            var transaction = new BankTransaction
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Amount = request.Amount,
                Description = request.Description,
                Type = TransactionType.PixSent,
                DestinationAccount = "Destinatário PIX"
            };

            // Simular necessidade de autenticação para transações acima de R$ 1.000
            bool requiresAuth = request.Amount > 1000;

            if (!requiresAuth)
            {
                await _database.ExecuteTransactionAsync(account1.Cpf, transaction);
                return new PixPayResponse
                {
                    TransactionId = transaction.Id,
                    RequiresAuthentication = false,
                    Message = "Transferência PIX realizada com sucesso"
                };
            }
            else
            {
                await _database.AddPendingTransactionAsync(account1.Cpf, transaction);
                return new PixPayResponse
                {
                    TransactionId = transaction.Id,
                    RequiresAuthentication = true,
                    Message = "Autenticação necessária para concluir a transferência"
                };
            }
        }
    }
}
