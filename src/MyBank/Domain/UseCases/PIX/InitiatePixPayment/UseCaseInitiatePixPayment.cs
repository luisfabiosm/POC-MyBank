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

            if (transaction.Account1.AgencyNumber >= 0)
                _validateException.AddDetails(new ErrorDetails("Agencia origem deve ser um numero maior que 0", "Account1.AgencyNumber"));

            if (string.IsNullOrEmpty(transaction.Account1.AccountNumber))
                _validateException.AddDetails(new ErrorDetails("Conta origem deve vir preenchida", "Account1.AccountNumber"));

            if (!Regex.IsMatch(transaction.Account1.AccountNumber, @"^\d+$"))
                _validateException.AddDetails(new ErrorDetails("Conta origem deve vir preenchida somente com numeros", "Account1.AccountNumber"));


            if (transaction.Account2.AgencyNumber >= 0)
                _validateException.AddDetails(new ErrorDetails("Agencia destino deve ser um numero maior que 0", "Account2.AgencyNumber"));

            if (string.IsNullOrEmpty(transaction.Account2.AccountNumber))
                _validateException.AddDetails(new ErrorDetails("Conta destino deve vir preenchida", "Account2.AccountNumber"));

            if (!Regex.IsMatch(transaction.Account2.AccountNumber, @"^\d+$"))
                _validateException.AddDetails(new ErrorDetails("Conta destino deve vir preenchida somente com numeros", "Account2.AccountNumber"));

            if (transaction.Amount <=0)
                _validateException.AddDetails(new ErrorDetails("Valor deve ser maior que 0", "Amount"));

        }


        protected override async Task<PixPayResponse> ExecuteTransactionProcessing(TransactionInitiatePixPayment transaction, CancellationToken cancellationToken)
        {
            try
            {

                var _user = await _accountService.GetUser(transaction.Account1.AgencyNumber, transaction.Account1.AccountNumber);

                var _pixRequest = new PixPayRequest(transaction.Account2.BankNumber,  transaction.Account2.AgencyNumber, transaction.Account2.AccountNumber, transaction.Account2.Cpf, transaction.Amount, "");

                var _result = await _pixService.InitiatePixTransferAsync(transaction.Account1, _pixRequest);


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

