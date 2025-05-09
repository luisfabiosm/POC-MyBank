using System.Security.Claims;
using Domain.Core.Base;
using System.Transactions;
using Domain.Core.Mediator;
using Microsoft.AspNetCore.Authorization;
using Domain.UseCases.Accounts.GetBalance;
using Domain.Core.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Adapters.Inbound.WebApi.Bank.Mapping;
using Domain.Core.Models.Request;
using Domain.UseCases.Accounts.GetStatement;
using Domain.UseCases.Security.AuthTransaction;


namespace Adapters.Inbound.WebApi.Bank.Endpoints
{
    public static partial class AccountEndpoints
    {

        public static void AddAccountEndpoints(this WebApplication app)
        {

            var group = app.MapGroup("/api/account/")
                         .WithTags("Conta")
                         .RequireAuthorization();

            group.MapGet("/balance/{BankNumber}/{AgencyNumber}/{AccountNumber}", async (
                //AccountRequest request,
                int BankNumber, int AgencyNumber, string AccountNumber,
                [FromServices] BSMediator bSMediator,
                [FromServices] MappingHttpRequestToTransaction mapping
                ) =>
            {
                var _request = new AccountRequest(BankNumber, AgencyNumber, AccountNumber);

                string correlationId = Guid.NewGuid().ToString();
                var transaction = mapping.ToTransactionGetBalance(_request);
                var _result = await bSMediator.Send<TransactionGetBalance, BaseReturn<BalanceResponse>>(transaction);

                if (!_result.Success)
                    _result.ThrowIfError();

                return Results.Ok(_result.Data);
            })
            .WithName("ConsultarSaldo")
            .WithDescription("Retorna o saldo atual da conta")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);


            group.MapGet("/statement/{BankNumber}/{AgencyNumber}/{AccountNumber}", async (
                int BankNumber, int AgencyNumber, string AccountNumber,
                [FromServices] BSMediator bSMediator,
                [FromServices] MappingHttpRequestToTransaction mapping
                ) =>
            {
                var _request = new AccountRequest(BankNumber,AgencyNumber, AccountNumber);
                string correlationId = Guid.NewGuid().ToString();
                var transaction = mapping.ToTransactionGetStatement(_request);
                var _result = await bSMediator.Send<TransactionGetStatement, BaseReturn<StatementResponse>>(transaction);

                if (!_result.Success)
                    _result.ThrowIfError();

                return Results.Ok(_result.Data);
            })
            .WithName("ConsultarExtrato")
            .WithDescription("Retorna o extrato da conta")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);


            group.MapPost("/authenticate-transaction", async (
                [FromBody] AuthTransactionRequest request,
                [FromServices] BSMediator bSMediator,
                [FromServices] MappingHttpRequestToTransaction mapping
                ) =>
            {
                string correlationId = Guid.NewGuid().ToString();
                var transaction = mapping.ToTransactionAuthTransaction(request);
                var _result = await bSMediator.Send<TransactionAuthTransaction, BaseReturn<AuthTransactionResponse>>(transaction);

                if (!_result.Success)
                    _result.ThrowIfError();

                return Results.Ok(_result.Data);
            })
            .WithName("AutenticarTransacao")
            .WithDescription("Autentica uma transação utilizando a senha do cartão")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}

