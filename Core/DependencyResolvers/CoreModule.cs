using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Core.Utilities.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IMailService, MailManager>();
            services.AddSingleton<IEmailConfiguration, EmailConfiguration>();
            services.AddSingleton<IHttpContextAccessor, IHttpContextAccessor>();
            services.AddSingleton<Stopwatch>();
            //services.AddMediatR(Assembly.GetExecutingAssembly());

            
        }
    }
}
