using System.Diagnostics;

namespace Domain.Core.Interfaces.Outbound
{
    public interface IOperationContext : IDisposable
    {
        Activity Activity { get; }
        void SetTag(string key, string value);
        void SetStatus(string status);
        IOperationContext StartOperation(
            string operationName,
            string correlationId,
            ActivityContext parentContext = default,
            ActivityKind kind = ActivityKind.Internal
        );
    }
}
