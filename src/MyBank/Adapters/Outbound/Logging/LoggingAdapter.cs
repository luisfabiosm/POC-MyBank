using Domain.Core.Interfaces.Outbound;
using Serilog;
using System.Diagnostics;

namespace Adapters.Outbound.Logging
{
    public class LoggingAdapter : ILoggingAdapter, IDisposable
    {
        private readonly ActivitySource _activitySource;
        private Activity _currentActivity;

        private bool _disposed;


    
        public LoggingAdapter(
             string sourceName)
        {
            _activitySource = new ActivitySource(sourceName);
        }

        public void AddProperty(string key, string value)
        {
            _currentActivity?.SetTag(key, value);
        }

        public void LogInformation(string message, params object[] args)
        {

            Log.Information(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {

            Log.Warning(message, args);
        }

        public void LogError(string message, Exception ex = null, params object[] args)
        {
            //_logger.LogError(ex, message, args);
            Log.Error(ex, message, args);

            // Se houver uma atividade atual, adicionar erro como tag
            var currentActivity = Activity.Current;
            currentActivity?.SetTag("error", true);
            currentActivity?.SetTag("error.message", message);
            if (ex != null)
            {
                currentActivity?.SetTag("error.stacktrace", ex.StackTrace);
            }
        }

        public void LogDebug(string message, params object[] args)
        {
            //_logger.LogDebug(message, args);
            Log.Debug(message, args);
        }

        public IOperationContext StartOperation(
          string operationName,
          string correlationId,
          ActivityContext parentContext = default,
          ActivityKind kind = ActivityKind.Internal)
        {
            var activity = _activitySource.StartActivity(
                operationName,
                kind,
                parentContext,
                tags: new[] {
                    new KeyValuePair<string, object>("correlation_id", correlationId)
                }
            );

            if (activity != null)
            {
                activity.SetTag("correlation_id", correlationId);
                _currentActivity = activity;
                return new OperationContext(activity);
            }
            else
            {
                // Return a no-op implementation instead of null
                return new NoOpOperationContext(_currentActivity);
            }

        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Liberar recursos gerenciados, se houver.
                // _activitySource não implementa IDisposable, então nada a fazer aqui.
            }

            _disposed = true;
        }
    }
}

