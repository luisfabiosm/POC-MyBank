using Domain.Core.Base;
using Domain.Core.Enums;
using Domain.Core.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Transactions;

namespace Domain.Core.Entity
{
    public record ExternalAccount
    {
        public int BankNumber { get; set; }
        public int AgencyNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Cpf { get; set; }
        public List<PixKey> PixKeys { get; set; } = new List<PixKey>();
    
        public ExternalAccount()
        {
            
        }

        public ExternalAccount(int bank, int agency, string account, string cpf, string key, PixKeyType type)
        {
            this.BankNumber = bank;
            this.AgencyNumber = agency;
            this.AccountNumber = account;
            this.Cpf = cpf;
            this.PixKeys.Add(new PixKey(key, type));
        }

        public ExternalAccount(int bank, int agency, string account, string cpf)
        {
            this.BankNumber = bank;
            this.AgencyNumber = agency;
            this.AccountNumber = account;
            this.Cpf = cpf;
    
        }
    }
}
