using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Users.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Utilities.Results;
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
    public class UpdateUserCommand:IRequest<IResult>
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobilePhones { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }



        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IResult>
        {
            private readonly IUserRepository _userRepository;
            public UpdateUserCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            [ValidationAspect(typeof(UpdateUserValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            public async Task<IResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var isUserExits = await _userRepository.GetAsync(u => u.UserId == request.UserId);

                isUserExits.FirstName = request.FirstName;
                isUserExits.LastName = request.LastName;
                isUserExits.Email = request.Email;
                isUserExits.MobilePhones = request.MobilePhones;
                isUserExits.Address = request.Address;
                isUserExits.Notes = request.Notes;

                isUserExits.UpdateContactDate = DateTime.Now;

                _userRepository.Update(isUserExits);
                await _userRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
