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
    public class GetUserGroupLookupQuery:IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public int UserId { get; set; }

        public class GetUserGroupLookupQueryHandler : IRequestHandler<GetUserGroupLookupQuery, IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IUserGroupRepository _userGroupRepository;
            public GetUserGroupLookupQueryHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUserGroupLookupQuery request, CancellationToken cancellationToken)
            {
                var data = await _userGroupRepository.GetUserGroupSelectedList(request.UserId);

                return new SuccessDataResult<IEnumerable<SelectionItem>>(data);
            }
        }
    }
}
