using Domain.Core.Base;
using Domain.Core.Enums;
using Domain.Core.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Transactions;

namespace Domain.Core.Entity
{
    public record Account
    {
        public int BankNumber { get; set; }
        public int AgencyNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Cpf { get; set; }
        public decimal Balance { get; set; }
        public List<PixKey> PixKeys { get; set; } = new List<PixKey>();
        public List<BankTransaction> Transactions { get; set; } = new List<BankTransaction>();

        public Account()
        {

        }

        public Account(int bank, int agency, string account, string cpf, string key, PixKeyType type)
        {
            this.BankNumber = bank;
            this.AgencyNumber = agency;
            this.AccountNumber = account;
            this.Cpf = cpf;
            this.PixKeys.Add(new PixKey(key, type));
        }


        public bool IsValidCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            // Invalid known CPFs
            var invalidCpfs = new[]
            {
            "00000000000", "11111111111", "22222222222",
            "33333333333", "44444444444", "55555555555",
            "66666666666", "77777777777", "88888888888",
            "99999999999"  };

            if (invalidCpfs.Contains(cpf))
                return false;

            // Validate first check digit
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (cpf[i] - '0') * (10 - i);

            int remainder = sum % 11;
            int firstCheckDigit = (remainder < 2) ? 0 : 11 - remainder;

            if (cpf[9] - '0' != firstCheckDigit)
                return false;

            // Validate second check digit
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (cpf[i] - '0') * (11 - i);

            remainder = sum % 11;
            int secondCheckDigit = (remainder < 2) ? 0 : 11 - remainder;

            return cpf[10] - '0' == secondCheckDigit;
        }

    }
}
