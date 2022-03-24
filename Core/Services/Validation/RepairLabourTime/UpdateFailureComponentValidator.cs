using Common.Contracts.v1.RepairLabourTime;
using FluentValidation;

namespace Common.Core.Services.Validation.RepairLabourTime
{
    public class UpdateFailureComponentValidator : AbstractValidator<UpdateFailureComponent>
    {
        public UpdateFailureComponentValidator()
        {
            RuleFor(fc => fc.Title).NotEmpty().WithMessage("Failure component must have a title");
            RuleFor(fc => fc.AssemblyOrSystemName).NotEmpty().WithMessage("Failure component must have an assembly name");
        }
    }
}
