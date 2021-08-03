﻿using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Constants;

namespace Business.Handlers.UserClaims.Commands
{
    [SecuredOperation]
    public class UpdateUserClaimCommand:IRequest<IResult>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int[] ClaimId { get; set; }

        public class UpdateUserClaimCommandHandler : IRequestHandler<UpdateUserClaimCommand, IResult>
        {
            private readonly IUserClaimRepository _userClaimRepository;
            public UpdateUserClaimCommandHandler(IUserClaimRepository userClaimRepository)
            {
                _userClaimRepository = userClaimRepository;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateUserClaimCommand request, CancellationToken cancellationToken)
            {
                var userList = request.ClaimId.Select(x => new UserClaim() { ClaimId = x, UserId = request.UserId });

                await _userClaimRepository.BulkInsert(request.UserId, userList);
                await _userClaimRepository.SaveChangesAsync();

                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
