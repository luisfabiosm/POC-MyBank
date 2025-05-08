

using Domain.Core.Entity;
using Domain.Core.Enums;
using Domain.Core.Mediator;

namespace Domain.Core.Base
{
    public abstract record BaseTransaction<TResponse> : BankTransaction,  IBSRequest<TResponse>
    {
        public int Code { get; init; }

        public string CorrelationId { get; set; } = Guid.NewGuid().ToString();

       
        public BaseTransaction()
        {
            
        }

    }
}
