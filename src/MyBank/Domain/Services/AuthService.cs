using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Adapters.Outbound.Database.InMemory;
using Domain.Core.Interfaces.Domain;
using Domain.Core.Models.Response;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Services
{
    public class AuthService: IAuthService
    {
        private readonly InMemoryDatabase _database;
        private readonly IConfiguration _configuration;

        public AuthService(InMemoryDatabase database, IConfiguration configuration)
        {
            _database = database;
            _configuration = configuration;
        }

        public async Task<LoginResponse?> AuthenticateAsync(string cpf, string password)
        {
            var user = await _database.GetUserByCpfAsync(cpf);
            if (user == null || user.AccessPassword != password)
            {
                return null;
            }

            var account = await _database.GetAccountByCpfAsync(cpf);
            if (account == null)
            {
                return null;
            }

            var token = await GenerateJwtToken(user.Cpf, password,  user.Name);

            return new LoginResponse
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1),
                Name = user.Name,
                Cpf = user.Cpf,
                AgencyNumber = account.AgencyNumber,
                AccountNumber = account.AccountNumber
            };
        }


        public async Task<GetTokenResponse> GenerateJwtTokenAsync(string cpf, string password, string name)
        {

            var _jwtToken = await GenerateJwtToken(cpf, password, name);

            return new GetTokenResponse(_jwtToken, DateTime.UtcNow.AddHours(1));
        }


        private async Task<string> GenerateJwtToken(string cpf, string password, string name)
        {

            var user = await _database.GetUserByCpfAsync(cpf);
            if (user == null || user.AccessPassword != password)
            {
                return null;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "3C8p@N1J8t$R#V7Y$Z2qsT7UxW1ac0cD"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, cpf),
                new Claim(JwtRegisteredClaimNames.Name, name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "MyBank",
                audience: _configuration["Jwt:Audience"] ?? "MyBankClient",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ValidateCardPasswordAsync(string cpf, string cardPassword)
        {
            var user = _database.GetUserByCpfAsync(cpf).Result;
            return user != null && user.CardPassword == cardPassword;
        }
    }
}

