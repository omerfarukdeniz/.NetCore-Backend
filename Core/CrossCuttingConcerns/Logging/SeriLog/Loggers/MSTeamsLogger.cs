using Core.CrossCuttingConcerns.Logging.SeriLog.ConfigurationModels;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.SeriLog.Loggers
{
    public class MSTeamsLogger:LoggerServiceBase
    {
        public MSTeamsLogger()
        {
            IConfiguration configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfiguration:MSTeamsConfiguration").Get<MSTeamsConfiguration>()
                ?? throw new Exception(SeriLogMessages.NullOptionsMessage);

            _logger = new LoggerConfiguration().WriteTo.MicrosoftTeams(logConfig.ChannelHookAddress).CreateLogger();
        }
    }
}
