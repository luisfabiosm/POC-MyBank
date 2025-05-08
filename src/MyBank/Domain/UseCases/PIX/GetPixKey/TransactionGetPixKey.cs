using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.Core.Enums;
using Domain.Core.Models.Response;

namespace Domain.UseCases.PIX.GetPixKey
{

    public record TransactionGetPixKey : BaseTransaction<BaseReturn<PixKeyResponse>>
    {
        public string Key { get; set; } 
        public PixKeyType Type { get; set; }

        public TransactionGetPixKey( string key, PixKeyType type)
        {
            this.Key = key;
            this.Type = type;
        }
    }
}
