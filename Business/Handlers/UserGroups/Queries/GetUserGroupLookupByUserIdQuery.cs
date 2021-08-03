﻿using Business.BusinessAspects;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.UserGroups.Queries
{
    [SecuredOperation]
    public class GetUserGroupLookupByUserIdQuery:IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public int UserId { get; set; }

        public class GetUserGroupLookupByUserIdQueryHandler : IRequestHandler<GetUserGroupLookupByUserIdQuery, IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IUserGroupRepository _userGroupRepository;
            private readonly IMediator _mediator;
            public GetUserGroupLookupByUserIdQueryHandler(IUserGroupRepository userGroupRepository, IMediator mediator)
            {
                _userGroupRepository = userGroupRepository;
                _mediator = mediator;
            }
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserGroupLookupByUserIdQuery request, CancellationToken cancellationToken)
            {
                var data = await _userGroupRepository.GetUserGroupSelectedList(request.UserId);
                return new SuccessDataResult<IEnumerable<SelectionItem>>(data);
            }
        }
    }
}
