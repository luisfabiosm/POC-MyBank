using Domain.Core.Exceptions;
using Domain.Core.Interfaces.Outbound;
using System;

namespace Domain.Core.Base
{
    public class BaseService
    {

        protected readonly ILoggingAdapter _loggingAdapter;

        public BaseService(IServiceProvider serviceProvider)
        {
            _loggingAdapter = serviceProvider.GetRequiredService<ILoggingAdapter>();
        }


        protected async Task<Exception> handleErrorAsync(Exception exception, string methodOnError)
        {
            _loggingAdapter.LogError($"Erro em: {methodOnError} - {exception.Message}", exception);

            Exception resultException = exception switch
            {
                BusinessException _ => exception,
                InternalException _ => exception,
                ValidateException _ => exception,
                _ => new InternalException("Erro interno não esperado", 500, exception)
            };


            return resultException;
        }

    }
}
