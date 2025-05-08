using Domain.Core.Enums;

namespace Domain.Core.Entity
{
    public record PixKey
    {
        public string Key { get; set; } = string.Empty;
        public PixKeyType Type { get; set; }

        public PixKey()
        {
            
        }
        public PixKey(string key, PixKeyType type)
        {
            this.Key = key;
            this.Type = type;
        }
    }
}
