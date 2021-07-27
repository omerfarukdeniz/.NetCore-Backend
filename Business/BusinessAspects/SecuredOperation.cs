using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;
using Core.Extensions;
using System.Security;
using Business.Constants;

namespace Business.BusinessAspects
{
    public class SecuredOperation:MethodInterception
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            var operationName = invocation.TargetType.ReflectedType.Name;
            if (roleClaims.Contains(operationName))
                return;

            throw new SecurityException(Messages.AuthorizationsDenied);
        }
    }
}
