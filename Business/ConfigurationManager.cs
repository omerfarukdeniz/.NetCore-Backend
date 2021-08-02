
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Business
{
    public class ConfigurationManager
    {
        public ApplicationMode Mode { get; private set; }

        public ConfigurationManager(IConfiguration configuration, IHostEnvironment environment)
        {
            Mode = (ApplicationMode)Enum.Parse(typeof(ApplicationMode), environment.EnvironmentName);
        }
    }


    public enum ApplicationMode
    {
        Development,
        Profiling,
        Staging,
        Production
    }
}
