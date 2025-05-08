using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces.Domain;
using Domain.Core.Models.Response;
using System.Text.RegularExpressions;

namespace Domain.UseCases.Accounts.GetBalance
{
    public class UseCaseGetBalance : BaseUseCaseHandler<TransactionGetBalance, BaseReturn<BalanceResponse>, BalanceResponse>
    {
        private readonly IAccountService _accountService;

        public UseCaseGetBalance(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _accountService = serviceProvider.GetRequiredService<IAccountService>();
        }

        protected override async Task ValidateTransaction(TransactionGetBalance transaction, CancellationToken cancellationToken)
        {

            if (transaction.AgencyNumber >= 0)
                _validateException.AddDetails(new ErrorDetails("Agencia deve ser um numero maior que 0", "AgencyNumber"));

            if (string.IsNullOrEmpty(transaction.AccountNumber))
                _validateException.AddDetails(new ErrorDetails("Conta deve vir preenchida", "AccountNumber"));

            if (!Regex.IsMatch(transaction.AccountNumber, @"^\d+$"))
                _validateException.AddDetails(new ErrorDetails("Conta deve vir preenchida somente com numeros", "AccountNumber"));

        }


        protected override async Task<BalanceResponse> ExecuteTransactionProcessing(TransactionGetBalance transaction, CancellationToken cancellationToken)
        {
            try
            {

                var _user = await _accountService.GetUser(transaction.AgencyNumber, transaction.AccountNumber);

                var _balance = await _accountService.GetBalanceAsync(transaction.AgencyNumber, transaction.AccountNumber);


                return new BalanceResponse(_user, transaction, _balance);
            }
            catch (Exception dbEx)
            {
                _loggingAdapter.LogError("Database error", dbEx);
                throw;
            }
        }


        protected override BaseReturn<BalanceResponse> ReturnSuccessResponse(BalanceResponse result, string message, string correlationId)
        {
            return BaseReturn<BalanceResponse>.FromSuccess(
                result,
                message,
                correlationId
            );
        }


        protected override BaseReturn<BalanceResponse> ReturnErrorResponse(Exception exception, string correlationId)
        {
            return new BaseReturn<BalanceResponse>(exception, false, correlationId);
        }
    }
}
