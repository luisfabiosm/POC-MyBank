using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces.Domain;
using Domain.Core.Interfaces.Outbound;
using Domain.Core.Models.Response;
using System.Text.RegularExpressions;

namespace Domain.UseCases.Security.AuthTransaction
{
    public class UseCaseAuthTransaction : BaseUseCaseHandler<TransactionAuthTransaction, BaseReturn<AuthTransactionResponse>, AuthTransactionResponse>
    {
        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;

        public UseCaseAuthTransaction(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _accountService = serviceProvider.GetRequiredService<IAccountService>();
            _authService = serviceProvider.GetRequiredService<IAuthService>();
        }

        protected override async Task ValidateTransaction(TransactionAuthTransaction transaction, CancellationToken cancellationToken)
        {

            if (transaction.AgencyNumber >=0)
                _validateException.AddDetails(new ErrorDetails("Agencia deve ser um numero maior que 0", "AgencyNumber"));

            if (string.IsNullOrEmpty(transaction.AccountNumber))
                _validateException.AddDetails(new ErrorDetails("Conta deve vir preenchida", "AccountNumber"));

            if (!Regex.IsMatch(transaction.AccountNumber, @"^\d+$"))
                _validateException.AddDetails(new ErrorDetails("Conta deve vir preenchida somente com numeros", "AccountNumber"));


            if (string.IsNullOrEmpty(transaction.OriginaLTransactionId))
                _validateException.AddDetails(new ErrorDetails("OriginaLTransactionId deve vir preenchido", "OriginaLTransactionId"));

        }


        protected override async Task<AuthTransactionResponse> ExecuteTransactionProcessing(TransactionAuthTransaction transaction, CancellationToken cancellationToken)
        {
            try
            {
                var _user = await _accountService.GetUser(transaction.AgencyNumber, transaction.AccountNumber);

                var _result = await _authService.ValidateCardPasswordAsync(_user.Cpf, transaction.CardPassword);

                return new AuthTransactionResponse(_result, transaction);
            }
            catch (Exception dbEx)
            {
                _loggingAdapter.LogError("Database error", dbEx);
                throw;
            }
        }


        protected override BaseReturn<AuthTransactionResponse> ReturnSuccessResponse(AuthTransactionResponse result, string message, string correlationId)
        {
            return BaseReturn<AuthTransactionResponse>.FromSuccess(
               result,
                message,
                correlationId
            );
        }


        protected override BaseReturn<AuthTransactionResponse> ReturnErrorResponse(Exception exception, string correlationId)
        {
            return new BaseReturn<AuthTransactionResponse>(exception, false, correlationId);
        }

    }
}
