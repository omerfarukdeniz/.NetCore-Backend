﻿using AutoMapper;
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
    public class GetUserQuery:IRequest<IDataResult<UserDto>>
    {
        public int UserId { get; set; }

        public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IDataResult<UserDto>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            [LogAspect(typeof(LogstashLogger))]
            public async Task<IDataResult<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(p => p.UserId == request.UserId);
                var userDto = _mapper.Map<UserDto>(user);

                return new SuccessDataResult<UserDto>(userDto);
            }
        }
    }
}
