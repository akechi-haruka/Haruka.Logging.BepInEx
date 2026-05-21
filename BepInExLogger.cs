using System;
using BepInEx.Logging;
using Microsoft.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Haruka.Logging.BepInEx {
    public class BepInExLogger : ILogger {
        private readonly string categoryName;
        private readonly IServiceProvider provider;
        private readonly ManualLogSource log;

        public BepInExLogger(string categoryName, IServiceProvider provider, ManualLogSource log) {
            this.categoryName = categoryName;
            this.provider = provider;
            this.log = log;
        }

        public IDisposable BeginScope<TState>(TState state) {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel) {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter) {
            if (!IsEnabled(logLevel)) {
                return;
            }

            if (formatter == null) {
                throw new ArgumentNullException(nameof(formatter));
            }

            string message = formatter(state, exception);

            log.Log(MicrosoftLogLevelToBepInEx(logLevel), message);
        }

        private global::BepInEx.Logging.LogLevel MicrosoftLogLevelToBepInEx(LogLevel logLevel) {
            switch (logLevel) {
                case LogLevel.Critical: return global::BepInEx.Logging.LogLevel.Fatal;
                case LogLevel.Error: return global::BepInEx.Logging.LogLevel.Error;
                case LogLevel.Warning: return global::BepInEx.Logging.LogLevel.Warning;
                case LogLevel.Information: return global::BepInEx.Logging.LogLevel.Info;
                case LogLevel.Debug:
                case LogLevel.Trace: return global::BepInEx.Logging.LogLevel.Debug;

                default: return global::BepInEx.Logging.LogLevel.None;
            }
        }
    }
}