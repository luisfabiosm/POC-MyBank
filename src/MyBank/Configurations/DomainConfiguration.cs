

using Domain.Core.Base;
using Domain.Core.Interfaces.Domain;
using Domain.Core.Mediator;
using Domain.Core.Models.Response;
using Domain.Services;
using Domain.UseCases.Accounts.GetBalance;
using Domain.UseCases.Accounts.GetStatement;
using Domain.UseCases.PIX.GetPixKey;
using Domain.UseCases.PIX.InitiatePixPayment;
using Domain.UseCases.Security.AuthTransaction;
using Domain.UseCases.Security.GetToken;
using Domain.UseCases.Security.LoginAccount;


namespace Configurations 
{
    public static class DomainConfiguration
    {

        public static IServiceCollection ConfigureDomainAdapters(this IServiceCollection services, IConfiguration configuration)
        {


            //#region Domain MediatoR

            services.AddTransient<BSMediator>();
            services.AddTransient<IBSRequestHandler<TransactionLoginAccount, BaseReturn<LoginResponse>>, UseCaseLoginAccount>(); //PARA CADA USECASE HANDLER
            services.AddTransient<IBSRequestHandler<TransactionAuthTransaction, BaseReturn<AuthTransactionResponse>>, UseCaseAuthTransaction>(); //PARA CADA USECASE HA                                                                                                                                                 
            services.AddTransient<IBSRequestHandler<TransactionGetPixKey, BaseReturn<PixKeyResponse>>, UseCaseGetPixKey>(); //PARA CADA USECASE HANDLER
            services.AddTransient<IBSRequestHandler<TransactionInitiatePixPayment, BaseReturn<PixPayResponse>>, UseCaseInitiatePixPayment>(); //PARA CADA USECASE HANDLER
            services.AddTransient<IBSRequestHandler<TransactionGetBalance, BaseReturn<BalanceResponse>>, UseCaseGetBalance>(); //PARA CADA USECASE HANDLER
            services.AddTransient<IBSRequestHandler<TransactionGetStatement, BaseReturn<StatementResponse>>, UseCaseGetStatement>(); //PARA CADA USECASE HANDLER
            services.AddTransient<IBSRequestHandler<TransactionGetToken, BaseReturn<GetTokenResponse>>, UseCaseGetToken>(); //PARA CADA USECASE HANDLER

            //#endregion


            #region Domain Services

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPixService, PixService>();

            #endregion



            return services;
        }
    }
}
