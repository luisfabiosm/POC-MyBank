using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces.Domain;
using Domain.Core.Models.Response;
using Domain.UseCases.Security.AuthTransaction;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Domain.UseCases.Security.GetToken
{
    public class UseCaseGetTokenTransaction : BaseUseCaseHandler<TransactionGetToken, BaseReturn<GetTokenResponse>, GetTokenResponse>
    {

        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;

        public UseCaseGetTokenTransaction(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _accountService = serviceProvider.GetRequiredService<IAccountService>();
            _authService = serviceProvider.GetRequiredService<IAuthService>();
        }


        protected override async Task ValidateTransaction(TransactionGetToken transaction, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(transaction.Cpf))
                _validateException.AddDetails(new ErrorDetails("Cpf deve vir preenchido", "Cpf"));

            if (new Account().IsValidCPF(transaction.Cpf))
                _validateException.AddDetails(new ErrorDetails("CPF inválido", "Cpf"));

            if (!Regex.IsMatch(transaction.Cpf, @"^\d+$"))
                _validateException.AddDetails(new ErrorDetails("CPF deve vir preenchida somente com numeros", "Cpf"));


        }


        protected override async Task<GetTokenResponse> ExecuteTransactionProcessing(TransactionGetToken transaction, CancellationToken cancellationToken)
        {
            try
            {
                var _user = await _accountService.GetUserbyCpfAsync(transaction.Cpf);

                if (_user is null)
                    throw new BusinessException("Falha na recuperação do Token.");

                var _result = await _authService.GenerateJwtTokenAsync(_user.Cpf, transaction.Password, _user.Name);

                return new GetTokenResponse(_result.BearerToken, _result.Expiration);
            }
            catch (Exception dbEx)
            {
                _loggingAdapter.LogError("Database error", dbEx);
                throw;
            }
        }


        protected override BaseReturn<GetTokenResponse> ReturnSuccessResponse(GetTokenResponse result, string message, string correlationId)
        {
            return BaseReturn<GetTokenResponse>.FromSuccess(
               result,
                message,
                correlationId
            );
        }


        protected override BaseReturn<GetTokenResponse> ReturnErrorResponse(Exception exception, string correlationId)
        {
            return new BaseReturn<GetTokenResponse>(exception, false, correlationId);
        }

    }
}
