using Adapters.Inbound.WebApi.Bank.Mapping;
using Domain.Core.Base;
using Domain.Core.Mediator;
using Domain.Core.Models.Request;
using Domain.Core.Models.Response;
using Domain.UseCases.Accounts.GetBalance;
using Domain.UseCases.PIX.GetPixKey;
using Domain.UseCases.PIX.InitiatePixPayment;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Adapters.Inbound.WebApi.Bank.Endpoints
{
    public static partial class PixEndpoints
    {

        public static void AddPixEndpoints(this WebApplication app)
        {


            var group = app.MapGroup("/api/pix")
              .WithTags("PIX")
              .RequireAuthorization();

            // Endpoint 5: Consulta Chave PIX
            group.MapGet("/keys/{PixKey}/{KeyType}", async (
                string PixKey, int KeyType,
                [FromServices] BSMediator bSMediator,
                [FromServices] MappingHttpRequestToTransaction mapping
                ) =>
            {
                string correlationId = Guid.NewGuid().ToString();

                var _request = new PixGetKeyRequest(PixKey, KeyType);

                var transaction = mapping.ToTransactionGetPixKey(_request);
                var _result = await bSMediator.Send<TransactionGetPixKey, BaseReturn<PixKeyResponse>>(transaction);

                if (!_result.Success)
                    _result.ThrowIfError();

                return Results.Ok(_result.Data);
            })
            .WithName("ConsultarChavesPix")
            .WithDescription("Retorna a conta da chave PIX")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

         
            group.MapPost("/pay", async (
                PixPayRequest request,
                [FromServices] BSMediator bSMediator,
                [FromServices] MappingHttpRequestToTransaction mapping
                ) =>
            {
                string correlationId = Guid.NewGuid().ToString();
                var transaction = mapping.ToTransactionInitiatePixPayment(request);
                var _result = await bSMediator.Send<TransactionInitiatePixPayment, BaseReturn<PixPayResponse>>(transaction);

                if (!_result.Success)
                    _result.ThrowIfError();

                return Results.Ok(_result.Data);
            })
            .WithName("IniciarPagamentoPix")
            .WithDescription("Inicia uma transferência PIX")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status400BadRequest);



           
        }


    }
}

