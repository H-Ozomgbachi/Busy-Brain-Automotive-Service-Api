using Common.Contracts.v1.RepairLabourTime;
using FluentValidation;

namespace Common.Core.Services.Validation.RepairLabourTime
{
    public class CreateMaintenanceItemValidator : AbstractValidator<CreateMaintenanceItem>
    {
        public CreateMaintenanceItemValidator()
        {
            RuleFor(mi => mi.Title).NotEmpty().WithMessage("Maintenance item must have a title");
            RuleFor(mi => mi.Code).NotEmpty().WithMessage("Maintenance item must have a code");
            RuleFor(mi => mi.LabourTimeHours).NotEqual(0).WithMessage("Maintenance item must have a labour time hours");
            RuleFor(mi => mi.TruckModel).NotEmpty().WithMessage("Maintenance item must have a truck model");
            RuleFor(mi => mi.CostPerHour).NotNull().WithMessage("Maintenance item must have a cost per hour");
        }
    }
}
