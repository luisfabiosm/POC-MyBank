using Domain.Core.Entity;
using Domain.Core.Enums;

namespace Domain.Core.Models.Request
{
    public record PixGetKeyRequest
    {
        public string Key { get; set; }
        public PixKeyType Type { get; set; }
    }
}
