using Domain.Core.Interfaces.Outbound;
using System.Diagnostics;

namespace Adapters.Outbound.Logging
{
    public class NoOpOperationContext: IOperationContext
    {
        public Activity Activity { get; }

        public NoOpOperationContext(Activity activity)
        {
            Activity = activity ?? Activity.Current;
        }

        public void Dispose()
        {
            Activity?.Dispose();
        }

        public void SetTag(string key, string value) { }

        public void SetStatus(string status) { }

        public IOperationContext StartOperation(
           string operationName,
           string correlationId,
           ActivityContext parentContext = default,
           ActivityKind kind = ActivityKind.Internal)
        {
            return this;
        }
    }
}
