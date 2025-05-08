using Domain.Core.Entity;

namespace Domain.Core.Models.Request
{
    public record PixPayRequest
    {
        public int Bank { get; set; }
        public int AgencyNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Cpf { get; set; } 
        public decimal Amount { get; set; }
        public string Description { get; set; }


        public PixPayRequest(int bank, int agency, string account, string cpf, decimal amount, string description = "")
        {
            this.Bank = bank;
            this.AgencyNumber = agency;
            this.AccountNumber = account;
            this.Cpf = cpf;
            this.Amount = amount;
            this.Description = description;
        } 
    }
}
