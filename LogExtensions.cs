using BepInEx.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Haruka.Logging.BepInEx {
    public static class LogExtensions {
        /// <summary>
        /// Adds a logger to send log messages to the given BepInEx <see cref="ManualLogSource"/>.
        /// </summary>
        public static ILoggingBuilder AddBepInEx(this ILoggingBuilder builder, ManualLogSource log) {
            builder.Services.Add(ServiceDescriptor.Singleton<ILoggerProvider, BepInExLoggerProvider>(provider => new BepInExLoggerProvider(provider, log)));
            return builder;
        }
    }
}