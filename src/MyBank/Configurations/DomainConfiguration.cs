

using Domain.Core.Base;
using Domain.Core.Interfaces.Domain;
using Domain.Core.Mediator;
using Domain.Core.Models.Response;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Configurations 
{
    public static class DomainConfiguration
    {

        public static IServiceCollection ConfigureDomainAdapters(this IServiceCollection services, IConfiguration configuration)
        {


            //#region Domain MediatoR

            //services.AddTransient<BSMediator>();
            //services.AddTransient<IBSRequestHandler<TransactionGetSampleTask, BaseReturn<ResponseGetSampleTask>>, UseCaseGetSampleTask>(); //PARA CADA USECASE HANDLER
            //services.AddTransient<IBSRequestHandler<TransactionAddSampleTask, BaseReturn<ResponseNewSampleTask>>, UseCaseAddSampleTask>(); //PARA CADA USECASE HANDLER
            //services.AddTransient<IBSRequestHandler<TransactionUpdateSampleTaskTimer, BaseReturn<bool>>, UseCaseUpdateSampleTaskTimer>(); //PARA CADA USECASE HANDLER
            //services.AddTransient<IBSRequestHandler<TransactionListSampleTask, BaseReturn<ResponseListSampleTask>>, UseCaseListSampleTask>(); //PARA CADA USECASE HANDLER

            //#endregion


            //#region Domain Services

            //services.AddScoped<ISampleService, SampleService>();

            //#endregion



            return services;
        }
    }
}
