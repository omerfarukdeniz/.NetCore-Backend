using Business.BusinessAspects;
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
    
    public class GetUsersInGroupLookupByGroupIdQuery:IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public int GroupId { get; set; }
        public class GetUsersInGroupLookupByGroupIdQueryHandler : IRequestHandler<GetUsersInGroupLookupByGroupIdQuery, IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IUserGroupRepository _userGroupRepository;
            public GetUsersInGroupLookupByGroupIdQueryHandler(IUserGroupRepository userGroupRepository)
            {
                _userGroupRepository = userGroupRepository;
            }
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetUsersInGroupLookupByGroupIdQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SelectionItem>>(await _userGroupRepository.GetUsersInGroupSelectedListByGroupId(request.GroupId));
            }
        }
    }
}
