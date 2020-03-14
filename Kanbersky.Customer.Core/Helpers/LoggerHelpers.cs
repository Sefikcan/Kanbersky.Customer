using Kanbersky.Customer.Core.Settings;
using Serilog;
using Serilog.Events;
using System;
using Serilog.Sinks.Elasticsearch;
using Serilog.Formatting.Elasticsearch;

namespace Kanbersky.Customer.Core.Helpers
{
    public class LoggerHelpers
    {
        private readonly ElasticsearchSettings _elasticsearchSettings;

        public LoggerHelpers(ElasticsearchSettings elasticsearchSettings)
        {
            _elasticsearchSettings = elasticsearchSettings;
        }

        public ILogger Register(string applicationName,LogEventLevel logEventLevel)
        {
            var logConf = new LoggerConfiguration();
            logConf.MinimumLevel.Override("Microsoft", logEventLevel);
            logConf.MinimumLevel.Override("System", logEventLevel);
            logConf.MinimumLevel.Verbose();
            logConf.Enrich.FromLogContext();
            logConf.Enrich.WithProperty("Application", applicationName);
            logConf.WriteTo.Console();

            if (_elasticsearchSettings != null && !string.IsNullOrEmpty(_elasticsearchSettings.ServerUrl))
            {
                logConf.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(_elasticsearchSettings.ServerUrl)));
            }

            return logConf.CreateLogger();
        }
    }
}
