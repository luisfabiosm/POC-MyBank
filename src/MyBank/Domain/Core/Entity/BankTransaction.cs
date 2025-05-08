using Domain.Core.Enums;

namespace Domain.Core.Entity
{
    public record BankTransaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public TransactionType Type { get; set; }
        public string? DestinationAccount { get; set; }
        public string? DestinationAgency { get; set; }
        public string? DestinationBank { get; set; }
        public string? DestinationDocument { get; set; }

    }
}
