using System;
using BepInEx.Logging;
using Microsoft.Extensions.Logging;

namespace Haruka.Logging.BepInEx {
    [ProviderAlias("BepInEx")]
    public class BepInExLoggerProvider : ILoggerProvider {
        private readonly IServiceProvider provider;
        private readonly ManualLogSource log;

        public BepInExLoggerProvider(IServiceProvider provider, ManualLogSource log) {
            this.provider = provider;
            this.log = log;
        }

        public void Dispose() {
        }

        public ILogger CreateLogger(string categoryName) {
            return new BepInExLogger(categoryName, provider, log);
        }
    }
}