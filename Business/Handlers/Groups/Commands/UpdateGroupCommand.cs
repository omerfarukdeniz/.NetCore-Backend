using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Groups.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Groups.Commands
{
    [SecuredOperation]
    public class UpdateGroupCommand:IRequest<IResult>
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, IResult>
        {
            private readonly IGroupRepository _groupRepository;
            public UpdateGroupCommandHandler(IGroupRepository groupRepository)
            {
                _groupRepository = groupRepository;
            }

            [ValidationAspect(typeof(UpdateGroupValidator),Priority =1)]
            public async Task<IResult> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
            {
                var groupToUpdate = new Group
                {
                    Id = request.Id,
                    GroupName = request.GroupName
                };

                _groupRepository.Update(groupToUpdate);
                await _groupRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}
