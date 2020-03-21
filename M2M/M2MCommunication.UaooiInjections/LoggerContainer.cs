using CommonServiceLocator;
using ReactiveHMI.M2MCommunication.Core.Interfaces;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using UAOOI.Common.Infrastructure.Diagnostic;
using UAOOI.Networking.Core;

namespace ReactiveHMI.M2MCommunication.UaooiInjections
{
    [Export(typeof(ILoggerContainer))]
    [Export(typeof(ITraceSource))]
    public class LoggerContainer : ILoggerContainer, ITraceSource, IDisposable
    {
        private readonly ILogger _loggerSink;
        private readonly IEnumerable<INetworkingEventSourceProvider> _eventSourceProviders;

        private IDisposable subscription;

        public LoggerContainer()
        {
            _loggerSink = ServiceLocator.Current.GetInstance<ILogger>();
            _eventSourceProviders = ServiceLocator.Current.GetAllInstances<INetworkingEventSourceProvider>();
        }

        #region ILoggerContainer

        public ILoggerContainer EnableLoggers()
        {
            subscription = _eventSourceProviders
                ?.Select(p =>
                {
                    ObservableEventListener eventListener = new ObservableEventListener();
                    eventListener.EnableEvents(p.GetPartEventSource(), EventLevel.LogAlways, Keywords.All);
                    return eventListener as IObservable<EventEntry>;
                })
                ?.Merge()
                ?.ObserveOn(Scheduler.Default)
                ?.Subscribe(TraceExternalLogs);
            return this;
        }

        #endregion

        #region ITraceSource

        public void TraceData(TraceEventType eventType, int id, object data)
        {
            switch (eventType)
            {
                case TraceEventType.Critical:
                case TraceEventType.Error:
                    _loggerSink?.LogError($"{id}: {data.ToString()}");
                    break;
                case TraceEventType.Warning:
                    _loggerSink?.LogWarning($"{id}: {data.ToString()}");
                    break;
                default:
                    _loggerSink?.LogInfo($"{id}: {data.ToString()}");
                    break;
            }
        }

        #endregion

        #region Private

        private void TraceExternalLogs(EventEntry entry)
        {
            string log = $"{entry.EventId}: {entry.FormattedMessage} - {string.Join("; ", entry.Payload.Select(p => p.ToString()))}";
            switch (entry.Schema.Level)
            {
                case EventLevel.Critical:
                case EventLevel.Error:
                    _loggerSink?.LogError(log);
                    break;
                case EventLevel.Warning:
                    _loggerSink?.LogWarning(log);
                    break;
                default:
                    _loggerSink?.LogInfo(log);
                    break;
            }
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    subscription?.Dispose();
                }
                subscription = null;

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
