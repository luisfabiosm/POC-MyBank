using Domain.Core.Entity;
using Domain.Core.Enums;
using Google.Protobuf.WellKnownTypes;

namespace Domain.Core.Models.Request
{
    public record PixGetKeyRequest
    {
        public string Key { get; set; }
        public PixKeyType Type { get; set; }

        public PixGetKeyRequest(string key, int type)
        {
            Key = key;
            Type = (PixKeyType)type;
        }
    }
}
