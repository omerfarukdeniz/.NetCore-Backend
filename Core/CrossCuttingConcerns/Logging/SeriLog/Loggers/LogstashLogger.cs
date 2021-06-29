using Core.CrossCuttingConcerns.Logging.SeriLog.ConfigurationModels;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Http.BatchFormatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.SeriLog.Loggers
{
    public class LogstashLogger:LoggerServiceBase
    {
        public LogstashLogger()
        {
            IConfiguration configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfigurations:LogstashConfiguration").Get<LogstashConfiguration>()
                ?? throw new Exception(SeriLogMessages.NullOptionsMessage);

            var seriLogConfig = new LoggerConfiguration().WriteTo
                .DurableHttpUsingFileSizeRolledBuffers(
                requestUri: logConfig.URL,
                batchFormatter: new ArrayBatchFormatter(),
                textFormatter: new ElasticsearchJsonFormatter()).CreateLogger();
            _logger = seriLogConfig;
        }
    }
}
