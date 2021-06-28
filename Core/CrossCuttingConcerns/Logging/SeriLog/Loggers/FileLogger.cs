using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.IoC;
using Core.CrossCuttingConcerns.Logging.SeriLog.ConfigurationModels;
using Core.Utilities.Messages;

namespace Core.CrossCuttingConcerns.Logging.SeriLog.Loggers
{
    public class FileLogger:LoggerServiceBase
    {
        public FileLogger()
        {
            IConfiguration configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration").Get<FileLogConfiguration>() ?? throw new Exception(SeriLogMessages.NullOptionsMessage);

            string logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + logConfig.FolderPath, ".txt");

            _logger = new LoggerConfiguration().WriteTo.File(
                logFilePath, rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: null,
                fileSizeLimitBytes: 5000000,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}").CreateLogger();
        }
    }
}
