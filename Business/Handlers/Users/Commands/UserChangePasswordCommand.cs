using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Users.Commands
{
    [SecuredOperation]
    public class UserChangePasswordCommand:IRequest<IResult>
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public class UserChangePasswordCommandHandler : IRequestHandler<UserChangePasswordCommand, IResult>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;
            public UserChangePasswordCommandHandler(IUserRepository userRepository, IMediator mediator)
            {
                _userRepository = userRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken)
            {
                var userExits = await _userRepository.GetAsync(u => u.UserId == request.UserId);
                if (userExits == null)
                    return new ErrorResult(Messages.UserNotFound);

                HashingHelper.CreatePasswordHash(request.Password, out byte[] passowordHash, out byte[] passwordSalt);

                userExits.PasswordHash = passowordHash;
                userExits.PasswordSalt = passwordSalt;

                _userRepository.Update(userExits);
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
