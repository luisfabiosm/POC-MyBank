using Domain.Core.Entity;
using Domain.Core.Models.Response;
using System.Transactions;

namespace Domain.Core.Interfaces.Domain
{
    public interface IAuthService
    {
        Task<LoginResponse?> AuthenticateAsync(string cpf, string password);
        string GenerateJwtToken(string cpf, string name);
        Task<bool> ValidateCardPasswordAsync(string cpf, string cardPassword);

    }
}
