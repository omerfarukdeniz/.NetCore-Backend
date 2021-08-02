using Business.Handlers.Groups.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Handlers.Groups.ValidationRules
{
    public class CreateGroupValidator:AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupValidator()
        {
            RuleFor(x => x.GroupName).NotEmpty();
        }
    }
    public class UpdateGroupValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupValidator()
        {
            RuleFor(x => x.GroupName).NotEmpty();
        }
        
    }
}
