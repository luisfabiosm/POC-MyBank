

namespace Domain.Core.Mediator
{
    public class BSMediator
    {

        private readonly IServiceProvider _serviceProvider;

        public BSMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IBSRequest<TResponse>
        {
            var handlerType = typeof(IBSRequestHandler<TRequest, TResponse>);
            var handler = (IBSRequestHandler<TRequest, TResponse>)_serviceProvider.GetService(handlerType);

            if (handler == null)
                throw new InvalidOperationException($"Nenhum handler encontrado para {typeof(TRequest).Name}");

            return await handler.Handle(request, cancellationToken);
        }
    }
}
