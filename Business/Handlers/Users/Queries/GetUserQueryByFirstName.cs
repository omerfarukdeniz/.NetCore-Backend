using AutoMapper;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Users.Queries
{
    [SecuredOperation]
    public class GetUserQueryByFirstName:IRequest<IDataResult<UserDto>>
    {
        public string FirstName { get; set; }
        public class GetUserQueryByFirstNameHandler : IRequestHandler<GetUserQueryByFirstName, IDataResult<UserDto>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            public GetUserQueryByFirstNameHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<UserDto>> Handle(GetUserQueryByFirstName request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(u => u.FirstName.Contains(request.FirstName));
                var userDto = _mapper.Map<UserDto>(user);
                return new SuccessDataResult<UserDto>(userDto);
            }
        }
    }
}
