using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces.Domain;
using Domain.Core.Models.Response;
using Domain.UseCases.Accounts.GetBalance;

namespace Domain.UseCases.PIX.GetPixKey
{
    public class UseCaseGetPixKey : BaseUseCaseHandler<TransactionGetPixKey, BaseReturn<PixKeyResponse>, PixKeyResponse>
    {

        private readonly IAccountService _accountService;
        private readonly IPixService _pixService;

        public UseCaseGetPixKey(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _accountService = serviceProvider.GetRequiredService<IAccountService>();
            _pixService = serviceProvider.GetRequiredService<IPixService>();
        }

        protected override async Task ValidateTransaction(TransactionGetPixKey transaction, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(transaction.Key))
                _validateException.AddDetails(new ErrorDetails("Chave deve vir preenchida", "Key"));


            if (string.IsNullOrEmpty(transaction.Key))
                _validateException.AddDetails(new ErrorDetails("Chave deve vir preenchida", "Key"));
        }


        protected override async Task<PixKeyResponse> ExecuteTransactionProcessing(TransactionGetPixKey transaction, CancellationToken cancellationToken)
        {
            try
            {

                var _account2 = await _pixService.EvaluatePixKeysAsync(transaction.Key, transaction.Type);

                return new PixKeyResponse(_account2, transaction);
            }
            catch (Exception dbEx)
            {
                _loggingAdapter.LogError("Database error", dbEx);
                throw;
            }
        }


        protected override BaseReturn<PixKeyResponse> ReturnSuccessResponse(PixKeyResponse result, string message, string correlationId)
        {
            return BaseReturn<PixKeyResponse>.FromSuccess(
                result,
                message,
                correlationId
            );
        }


        protected override BaseReturn<PixKeyResponse> ReturnErrorResponse(Exception exception, string correlationId)
        {
            return new BaseReturn<PixKeyResponse>(exception, false, correlationId);
        }
    }

}

