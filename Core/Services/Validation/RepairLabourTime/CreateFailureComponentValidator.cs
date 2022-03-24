using Common.Contracts.v1.RepairLabourTime;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core.Services.Validation.RepairLabourTime
{
    public class CreateFailureComponentValidator : AbstractValidator<CreateFailureComponent>
    {
        public CreateFailureComponentValidator()
        {
            RuleFor(fc => fc.Title).NotEmpty().WithMessage("Failure component must have a title");
            RuleFor(fc => fc.AssemblyOrSystemName).NotEmpty().WithMessage("Failure component must have an assembly name");
        }
    }
}
