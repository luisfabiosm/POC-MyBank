using Domain.Core.Base;
using Domain.UseCases.Security.AuthTransaction;

namespace Domain.Core.Models.Response
{
    public record AuthTransactionResponse  : BaseTransactionResponse
    {
        public string OriginalCorrelationId { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public AuthTransactionResponse(bool success, TransactionAuthTransaction transaction)
        {
            Success = success;
            OriginalCorrelationId = transaction.OriginaLTransactionId;
            CorrelationId = transaction.CorrelationId;
            Message = !success ? "Transação não Confirmada" : "Transação Autenticada e Confirmada com sucesso.";

        }
    }
}
