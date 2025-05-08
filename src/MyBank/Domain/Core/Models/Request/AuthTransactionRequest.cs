namespace Domain.Core.Models.Request
{
    public record AuthTransactionRequest
    {
        public string OriginaLTransactionId { get; set; } 
        public int AgencyNumber { get; set; }
        public string AccountNumber { get; set; } 
        public string CardPassword { get; set; } 
    }
}
