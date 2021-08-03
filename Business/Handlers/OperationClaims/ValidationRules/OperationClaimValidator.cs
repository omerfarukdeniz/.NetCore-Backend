using Business.Handlers.OperationClaims.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Handlers.OperationClaims.ValidationRules
{
    public class CreateOperationClaimValidator:AbstractValidator<CreateOperationClaimCommand>
    {
        public CreateOperationClaimValidator()
        {
            RuleFor(x => x.ClaimName).NotEmpty();
        }
    }
    public class UpdateOperationClaimValidator : AbstractValidator<UpdateOperationClaimCommand>
    {
        public UpdateOperationClaimValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
