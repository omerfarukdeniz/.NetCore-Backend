using Business.Handlers.Translates.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Handlers.Translates.ValidationRules
{
    public class CreateTranslateValidator:AbstractValidator<CreateTranslateCommand>
    {
        public CreateTranslateValidator()
        {
            RuleFor(x => x.LangId).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
        }
    }

    public class UpdateTranslateValidator : AbstractValidator<UpdateTranslateCommand>
    {
        public UpdateTranslateValidator()
        {
            RuleFor(x => x.LangId).NotEmpty();
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
