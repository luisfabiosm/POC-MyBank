using System.Diagnostics;

namespace Domain.Core.Interfaces.Outbound
{
    public interface ILoggingAdapter
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(string message, Exception ex = null, params object[] args);
        void LogDebug(string message, params object[] args);
        void AddProperty(string key, string value);
        IOperationContext StartOperation(
           string operationName,
           string correlationId,
           ActivityContext parentContext = default,
           ActivityKind kind = ActivityKind.Internal
       );
    }
}
