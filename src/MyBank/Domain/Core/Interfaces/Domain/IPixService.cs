using Domain.Core.Entity;
using Domain.Core.Enums;
using Domain.Core.Models.Request;
using Domain.Core.Models.Response;
using Domain.UseCases.PIX.InitiatePixPayment;

namespace Domain.Core.Interfaces.Domain
{
    public interface IPixService
    {
        Task<List<PixKey>> GetPixKeysAsync(string cpf);

        Task<ExternalAccount> EvaluatePixKeysAsync(string key, PixKeyType type);
        Task<PixPayResponse> InitiatePixTransferAsync(Account sourceAccout, TransactionInitiatePixPayment transaction);
        Task<bool> ConfirmPixTransferAsync(string cpf, Guid transactionId);
    }
}
