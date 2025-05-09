using Domain.Core.Entity;
using Domain.Core.Models.Response;
using System.Transactions;

namespace Domain.Core.Interfaces.Domain
{
    public interface IAuthService
    {
        Task<LoginResponse?> AuthenticateAsync(string cpf, string password);

        Task<GetTokenResponse> GenerateJwtTokenAsync(string cpf, string password, string name);

        Task<bool> ValidateCardPasswordAsync(string cpf, string cardPassword);

    }
}
