using Business.Services.Authentication;
using Business.Services.Authentication.Model;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Utilities.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Authorizations.Commands
{
    public class VerifyOTPHandler : IRequestHandler<VerifyOTPCommand, IDataResult<DArchToken>>
    {
        private readonly IAuthenticationCoordinator _coordinator;
        public VerifyOTPHandler(IAuthenticationCoordinator coordinator)
        {
            _coordinator = coordinator;
        }

        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<DArchToken>> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var provider = _coordinator.SelectProvider(request.Provider);
            return await provider.Verify(request);
        }
    }
}
