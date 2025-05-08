using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.UseCases.PIX.GetPixKey;
using Domain.UseCases.PIX.InitiatePixPayment;

namespace Domain.Core.Models.Response
{
    public record PixPayResponse : BaseTransactionResponse
    {
        public Guid TransactionId { get; set; }
        public bool RequiresAuthentication { get; set; }
        public string Message { get; set; } = string.Empty;

        public PixPayResponse()
        {
                
        }

        public PixPayResponse(TransactionInitiatePixPayment  transaction)
        {
            this.RequiresAuthentication = true;
            this.CorrelationId = transaction.CorrelationId;
        }
    }
}
