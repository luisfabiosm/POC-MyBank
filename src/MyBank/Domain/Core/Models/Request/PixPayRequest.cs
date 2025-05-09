using Domain.Core.Entity;

namespace Domain.Core.Models.Request
{
    public record PixPayRequest
    {

        public int SourceBankNumber { get; set; }
        public int SourceAgencyNumber { get; set; }
        public string SourceAccountNumber { get; set; } = string.Empty;
        public ExternalAccount Account2 { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }


        public PixPayRequest()
        {
            
        }

    }
}
