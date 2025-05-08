using Domain.Core.Base;
using Domain.Core.Entity;
using Domain.Core.Exceptions;
using Domain.Core.Interfaces.Domain;
using Domain.Core.Models;
using Domain.Core.Models.Response;
using Domain.UseCases.Accounts.GetBalance;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Domain.UseCases.Accounts.GetStatement
{
    public class UseCaseGetStatement : BaseUseCaseHandler<TransactionGetStatement, BaseReturn<StatementResponse>, StatementResponse>
    {
        private readonly IAccountService _accountService;

        public UseCaseGetStatement(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _accountService = serviceProvider.GetRequiredService<IAccountService>();
        }

        protected override async Task ValidateTransaction(TransactionGetStatement transaction, CancellationToken cancellationToken)
        {
            if (transaction.AgencyNumber >= 0)
                _validateException.AddDetails(new ErrorDetails("Agencia deve ser um numero maior que 0", "AgencyNumber"));

            if (string.IsNullOrEmpty(transaction.AccountNumber))
                _validateException.AddDetails(new ErrorDetails("Conta deve vir preenchida", "AccountNumber"));

            if (!Regex.IsMatch(transaction.AccountNumber, @"^\d+$"))
                _validateException.AddDetails(new ErrorDetails("Conta deve vir preenchida somente com numeros", "AccountNumber"));




        }


        protected override async Task<StatementResponse> ExecuteTransactionProcessing(TransactionGetStatement transaction, CancellationToken cancellationToken)
        {
            try
            {

                var _user = await _accountService.GetUser(transaction.AgencyNumber, transaction.AccountNumber);

                var _account = await _accountService.GetAccountAsync(transaction.AgencyNumber, transaction.AccountNumber);

                var _transactionList = await _accountService.GetTransactionsAsync(_user.Cpf);

                var _response = new StatementResponse
                {
                    AgencyNumber = transaction.AgencyNumber,
                    AccountNumber = transaction.AccountNumber,
                    Cpf = _user.Cpf,
                    Name = _user.Name,
                    CorrelationId = transaction.CorrelationId,
                    CurrentBalance = _account?.Balance ?? 0,
                    Transactions = _transactionList.Select(t => new TransactionItem
                    {
                        Date = t.Date,
                        Description = t.Description,
                        Amount = t.Amount,
                        Type = t.Type.ToString()
                    }).ToList()

                };

                return _response;

            }
            catch (Exception dbEx)
            {
                _loggingAdapter.LogError("Database error", dbEx);
                throw;
            }
        }


        protected override BaseReturn<StatementResponse> ReturnSuccessResponse(StatementResponse result, string message, string correlationId)
        {
            return BaseReturn<StatementResponse>.FromSuccess(
                result,
                message,
                correlationId
            );
        }


        protected override BaseReturn<StatementResponse> ReturnErrorResponse(Exception exception, string correlationId)
        {
            return new BaseReturn<StatementResponse>(exception, false, correlationId);
        }
    }
}
