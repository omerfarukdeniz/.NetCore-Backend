using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Authentication
{
    public class AuthenticationCoordinator : IAuthenticationCoordinator
    {
        private readonly IServiceProvider _serviceProvider;
        public AuthenticationCoordinator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IAuthenticationProvider SelectProvider(AuthenticationProviderType type)
        {
            return type switch
            {
                AuthenticationProviderType.Person => (IAuthenticationProvider)_serviceProvider.GetService(typeof(PersonAuthenticationProvider)),
                AuthenticationProviderType.Agent => (IAuthenticationProvider)_serviceProvider.GetService(typeof(AgentAuthenticationProvider)),
                _ => throw new ApplicationException($"Authentication provider not found : {type}")
            };
        }
    }
}
