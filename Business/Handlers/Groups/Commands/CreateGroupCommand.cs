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
    public class CreateGroupCommand:IRequest<IResult>
    {
        public string GroupName { get; set; }
        public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, IResult>
        {
            private readonly IGroupRepository _groupRepository;
            public CreateGroupCommandHandler(IGroupRepository groupRepository)
            {
                _groupRepository = groupRepository;
            }

            [ValidationAspect(typeof(CreateGroupValidator), Priority = 1)]
            public async Task<IResult> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var group = new Group
                    {
                        GroupName = request.GroupName
                    };
                    _groupRepository.Add(group);
                    await _groupRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Added);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
