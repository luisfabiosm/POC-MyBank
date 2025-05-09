using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces.Domain;
using Domain.Core.Models.Response;
using Domain.Services;
using Domain.UseCases.Security.AuthTransaction;

namespace Domain.UseCases.Security.LoginAccount
{
    public class UseCaseLoginAccount : BaseUseCaseHandler<TransactionLoginAccount, BaseReturn<LoginResponse>, LoginResponse>
    {
        private readonly IAuthService _authService;
        public UseCaseLoginAccount(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _authService = serviceProvider.GetRequiredService<IAuthService>();
        }


        protected override async Task ValidateTransaction(TransactionLoginAccount transaction, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(transaction.Cpf))
                _validateException.AddDetails(new ErrorDetails("Cpf deve vir preenchido", "Cpf"));

            if (new Account().IsValidCPF(transaction.Cpf))
                _validateException.AddDetails(new ErrorDetails("CPF inválido", "Cpf"));

            if (string.IsNullOrEmpty(transaction.Password))
                _validateException.AddDetails(new ErrorDetails("Password deve vir preenchido", "Password"));

            if (transaction.Password.Length != 8)
                _validateException.AddDetails(new ErrorDetails("Password deve ter 8 digitos", "Password"));

        }

        protected override async Task<LoginResponse> ExecuteTransactionProcessing(TransactionLoginAccount transaction, CancellationToken cancellationToken)
        {
            try
            {

                var _result = await _authService.AuthenticateAsync(transaction.Cpf, transaction.Password);

                if (_result is null)
                    throw new BusinessException("Falha no Login");


                return _result;
            }
            catch (Exception dbEx)
            {
                _loggingAdapter.LogError("Database error", dbEx);
                throw;
            }
        }

        protected override BaseReturn<LoginResponse> ReturnErrorResponse(Exception exception, string correlationId)
        {
            return new BaseReturn<LoginResponse>(exception, false, correlationId);
        }

        protected override BaseReturn<LoginResponse> ReturnSuccessResponse(LoginResponse result, string message, string correlationId)
        {
            return BaseReturn<LoginResponse>.FromSuccess(
            result,
             message,
             correlationId
            );
        }
    }

}
