using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces.Domain;
using Domain.Core.Models.Request;
using Domain.Core.Models.Response;
using Domain.UseCases.Accounts.GetBalance;
using System.Text.RegularExpressions;

namespace Domain.UseCases.PIX.InitiatePixPayment
{
    public class UseCaseInitiatePixPayment : BaseUseCaseHandler<TransactionInitiatePixPayment, BaseReturn<PixPayResponse>, PixPayResponse>
    {

        private readonly IAccountService _accountService;
        private readonly IPixService _pixService;

        public UseCaseInitiatePixPayment(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _accountService = serviceProvider.GetRequiredService<IAccountService>();
            _pixService = serviceProvider.GetRequiredService<IPixService>();
        }

        protected override async Task ValidateTransaction(TransactionInitiatePixPayment transaction, CancellationToken cancellationToken)
        {

            if (transaction.AgencyNumber >= 0)
                _validateException.AddDetails(new ErrorDetails("Agencia origem deve ser um numero maior que 0", "Account1.AgencyNumber"));

            if (string.IsNullOrEmpty(transaction.AccountNumber))
                _validateException.AddDetails(new ErrorDetails("Conta origem deve vir preenchida", "Account1.AccountNumber"));

            if (!Regex.IsMatch(transaction.AccountNumber, @"^\d+$"))
                _validateException.AddDetails(new ErrorDetails("Conta origem deve vir preenchida somente com numeros", "Account1.AccountNumber"));


            if (transaction.RemoteAccount.AgencyNumber >= 0)
                _validateException.AddDetails(new ErrorDetails("Agencia destino deve ser um numero maior que 0", "Account2.AgencyNumber"));

            if (string.IsNullOrEmpty(transaction.RemoteAccount.AccountNumber))
                _validateException.AddDetails(new ErrorDetails("Conta destino deve vir preenchida", "Account2.AccountNumber"));

            if (!Regex.IsMatch(transaction.RemoteAccount.AccountNumber, @"^\d+$"))
                _validateException.AddDetails(new ErrorDetails("Conta destino deve vir preenchida somente com numeros", "Account2.AccountNumber"));

            if (transaction.Amount <=0)
                _validateException.AddDetails(new ErrorDetails("Valor deve ser maior que 0", "Amount"));

        }


        protected override async Task<PixPayResponse> ExecuteTransactionProcessing(TransactionInitiatePixPayment transaction, CancellationToken cancellationToken)
        {
            try
            {
                var _sourceAccount = await _accountService.GetAccountAsync(transaction.AgencyNumber, transaction.AccountNumber);

                var _result = await _pixService.InitiatePixTransferAsync(_sourceAccount, transaction);


                return _result;
            }
            catch (Exception dbEx)
            {
                _loggingAdapter.LogError("Database error", dbEx);
                throw;
            }
        }


        protected override BaseReturn<PixPayResponse> ReturnSuccessResponse(PixPayResponse result, string message, string correlationId)
        {
            return BaseReturn<PixPayResponse>.FromSuccess(
                result,
                message,
                correlationId
            );
        }


        protected override BaseReturn<PixPayResponse> ReturnErrorResponse(Exception exception, string correlationId)
        {
            return new BaseReturn<PixPayResponse>(exception, false, correlationId);
        }
    }

}

