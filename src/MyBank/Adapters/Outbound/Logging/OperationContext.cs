using Domain.Core.Interfaces.Outbound;
using System.Diagnostics;

namespace Adapters.Outbound.Logging
{
    public class OperationContext: IOperationContext
    {
        public Activity Activity { get; }

        public OperationContext(Activity activity)
        {
            Activity = activity;
        }

        public void Dispose()
        {
            Activity?.Dispose();
        }
        public void SetTag(string key, string value)
        {
            Activity?.SetTag(key, value);
        }

        public void SetStatus(string status)
        {
            Activity?.SetStatus(status == "OK"
                ? ActivityStatusCode.Ok
                : ActivityStatusCode.Error);
        }

        public IOperationContext StartOperation(
            string operationName,
            string correlationId,
            ActivityContext parentContext = default,
            ActivityKind kind = ActivityKind.Internal)
        {
            var activity = Activity.Source.StartActivity(
                operationName,
                kind,
                parentContext,
                tags: new[] {
                    new KeyValuePair<string, object>("correlation_id", correlationId)
                }
            );

            return activity != null
                ? new OperationContext(activity)
                : new NoOpOperationContext(Activity.Current);
        }
    }
}
