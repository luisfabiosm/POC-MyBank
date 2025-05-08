namespace Domain.Core.Models.Request
{
    public record AccountRequest
    {
        public int BankNumber { get; set; }
        public int AgencyNumber { get; set; }
        public string AccountNumber { get; set; } = string.Empty;


        public AccountRequest()
        {
            
        }
        public AccountRequest(int bank, int agency, string accountNumber)
        {
            BankNumber = bank;
            AgencyNumber = agency;
            AccountNumber = accountNumber;
        }
    }
}
