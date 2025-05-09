using Adapters.Outbound.Database.InMemory;
using Domain.Core.Entity;
using Domain.Core.Enums;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces.Domain;
using Domain.Core.Models.Request;
using Domain.Core.Models.Response;
using Domain.UseCases.PIX.InitiatePixPayment;
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

        public async Task<PixPayResponse> InitiatePixTransferAsync(Account sourceAccount, TransactionInitiatePixPayment transaction)
        {
            

            if (sourceAccount == null)
            {
                throw new BusinessException("Conta origem não encontrada.");
            }

            if (sourceAccount.Balance < transaction.Amount)
            {
                throw new BusinessException("Saldo insuficiente.");

            }

            // Criar transação pendente
            var bankTransaction = new BankTransaction
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Amount = transaction.Amount,
                Description = transaction.Description,
                Type = TransactionType.PixSent,
                DestinationAccount = "Destinatário PIX"
            };

            // Simular necessidade de autenticação para transações acima de R$ 1.000
            bool requiresAuth = transaction.Amount > 1000;

            if (!requiresAuth)
            {
                await _database.ExecuteTransactionAsync(sourceAccount.Cpf, transaction);
                return new PixPayResponse
                {
                    TransactionId = transaction.Id,
                    RequiresAuthentication = false,
                    Message = "Transferência PIX realizada com sucesso"
                };
            }
            else
            {
                await _database.AddPendingTransactionAsync(sourceAccount.Cpf, transaction);
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
