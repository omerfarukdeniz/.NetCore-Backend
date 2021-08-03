using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserClaims.Commands
{
    public class CreateUserClaimsInternalCommand: IRequest<IResult>
    {
        public int UserId { get; set; }
        public IEnumerable<OperationClaim> OperationClaims { get; set; }

        public class CreateUserClaimsInternalCommandHandler : IRequestHandler<CreateUserClaimsInternalCommand, IResult>
        {
            private readonly IUserClaimRepository _userClaimRepository;
            public CreateUserClaimsInternalCommandHandler(IUserClaimRepository userClaimRepository)
            {
                _userClaimRepository = userClaimRepository;
            }
            public async Task<IResult> Handle(CreateUserClaimsInternalCommand request, CancellationToken cancellationToken)
            {
                foreach (var claim in request.OperationClaims)
                {
                    if (await DoesClaimExistsForUser(new UserClaim { ClaimId = claim.Id, UserId = request.UserId }))
                        continue;

                    _userClaimRepository.Add(new UserClaim
                    {
                        ClaimId = claim.Id,
                        UserId = request.UserId
                    });
                }
                await _userClaimRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }

            private async Task<bool> DoesClaimExistsForUser(UserClaim userClaim)
            {
                return (await _userClaimRepository.GetAsync(x => x.UserId == userClaim.UserId && x.ClaimId == userClaim.ClaimId)) is null ? false : true;
            }
        }
    }
}
