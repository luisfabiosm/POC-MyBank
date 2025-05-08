using Adapters.Inbound.WebApi.Bank.Mapping;
using Domain.Core.Base;
using Domain.Core.Mediator;
using Domain.Core.Models.Request;
using Domain.Core.Models.Response;
using Domain.UseCases.Security.GetToken;
using Domain.UseCases.Security.LoginAccount;
using Microsoft.AspNetCore.Mvc;

namespace Adapters.Inbound.WebApi.Bank.Endpoints
{
    public static partial class SecurityEndpoints
    {

        public static void AddSecurityEndpoints(this WebApplication app)
        {

            //var group = app.MapGroup("/api/account")
            //           .WithTags("Conta")
            //           .RequireAuthorization();

            //group.MapGet("/balance", async (
            //    [FromBody] AccountRequest request,
            //    [FromServices] BSMediator bSMediator,
            //    [FromServices] MappingHttpRequestToTransaction mapping
            //    ) =>
            //{
            //    string correlationId = Guid.NewGuid().ToString();
            //    var transaction = mapping.ToTransactionGetBalance(request);
            //    var _result = await bSMediator.Send<TransactionGetBalance, BaseReturn<BalanceResponse>>(transaction);

            //    if (!_result.Success)
            //        _result.ThrowIfError();

            //    return Results.Ok(_result.Data);
            //})
            //.WithName("ConsultarSaldo")
            //.WithDescription("Retorna o saldo atual da conta")
            //.Produces(StatusCodes.Status200OK)
            //.Produces(StatusCodes.Status401Unauthorized);


            var group = app.MapGroup("/api/auth")
                           .WithTags("Autenticação");

            group.MapPost("/token", async (
                [FromBody] GetTokenRequest request,
                [FromServices] BSMediator bSMediator,
                [FromServices] MappingHttpRequestToTransaction mapping
                ) =>
            {
                string correlationId = Guid.NewGuid().ToString();
                var transaction = mapping.ToTransactionGetToken(request);

                var _result = await bSMediator.Send<TransactionGetToken, BaseReturn<GetTokenResponse>>(transaction);

                if (!_result.Success)
                    _result.ThrowIfError();

                return Results.Ok(_result.Data);
            })
           .WithName("GerarToken")
           .WithDescription("Gera um novo token JWT para acesso à API")
           .Produces(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status401Unauthorized);



            group.MapPost("/login", async (
                [FromBody] LoginRequest request,
                [FromServices] BSMediator bSMediator,
                [FromServices] MappingHttpRequestToTransaction mapping
                ) =>
            {
                string correlationId = Guid.NewGuid().ToString();
                var transaction = mapping.ToTransactionLoginAccount(request);
                var _result = await bSMediator.Send<TransactionLoginAccount, BaseReturn<LoginResponse>>(transaction);

                if (!_result.Success)
                    _result.ThrowIfError();

                return Results.Ok(_result.Data);
            })
            .WithName("Login")
            .WithDescription("Autentica um usuário e retorna um token JWT")
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);


        }
    }
}
