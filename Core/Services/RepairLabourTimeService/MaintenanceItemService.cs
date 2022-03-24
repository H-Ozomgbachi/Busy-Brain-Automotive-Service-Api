using Common.Contracts.Exceptions;
using Common.Contracts.Exceptions.Types;
using Common.Contracts.v1.RepairLabourTime;
using Common.Core.CQRS.Commands.RepairLabourTime;
using Common.Core.CQRS.Queries.RepairLabourTime;
using Common.Core.Models.RepairLabourTimeModels;
using Common.Core.Services.Validation.RepairLabourTime;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Core.Services.RepairLabourTimeService
{
    public class MaintenanceItemService : IMaintenanceItemService
    {
        private readonly IMediator _mediator;
        private readonly IFailureComponentService _failureComponentService;

        public MaintenanceItemService(IMediator mediator, IFailureComponentService failureComponentService)
        {
            _mediator = mediator;
            _failureComponentService = failureComponentService;
        }

        public async Task<MaintenanceItemModel> CreateMaintenanceItem(CreateMaintenanceItem maintenanceItem, Guid modifiedBy)
        {
            CreateMaintenanceItemValidator validator = new CreateMaintenanceItemValidator();

            ValidationResult results = validator.Validate(maintenanceItem);

            if (!results.IsValid)
            {
                var ex = new BusinessLogicException("maintenance item validation error", "maintenance item validation error");
                var validationErrors = new List<ValidationError>();
                foreach (var failure in results.Errors)
                {
                    validationErrors.Add(new ValidationError(failure.PropertyName, failure.ErrorMessage));
                }
                ex.ValidationErrors = validationErrors;
                throw ex;
            }

            var failureComponent = await _failureComponentService.GetFailureComponent(maintenanceItem.FailureComponentId);

            if(failureComponent == null)
            {
                throw new BusinessLogicException($"Failure component with id: {maintenanceItem.FailureComponentId} doesn't exist", $"Failure component with id: {maintenanceItem.FailureComponentId} doesn't exist");
            }

            var createdMaintenanceItemId = await _mediator.Send(new CreateMaintenanceItemCommand
            {
                Title = maintenanceItem.Title,
                Code = maintenanceItem.Code,
                LabourTimeHours = maintenanceItem.LabourTimeHours,
                TruckModel = maintenanceItem.TruckModel,
                CostPerHour = maintenanceItem.CostPerHour,
                FailureComponentId = maintenanceItem.FailureComponentId,
                CreatedAt = DateTime.Now,
                ModifiedBy = modifiedBy
            });

            if (createdMaintenanceItemId > 0) return await GetMaintenanceItem(maintenanceItem.FailureComponentId, createdMaintenanceItemId);
            throw new BusinessLogicException("Could not create maintenance item, please retry", "Could not create maintenance item, please retry");
        }

        public async Task DeleteMaintenanceItem(int failureComponentId, int id)
        {
            await GetMaintenanceItem(failureComponentId, id);
            bool isDeleted = await _mediator.Send(new DeleteMaintenanceItemCommand { Id = id, FailureComponentId = failureComponentId });
            if (isDeleted == false) throw new BusinessLogicException("Could not delete maintenance item, please try again later", "Could not delete maintenance item, please try again later");
        }

        public async Task<MaintenanceItemModel> GetMaintenanceItem(int failureComponentId, int id)
        {
            return await _mediator.Send(new GetMaintenanceItemQuery { Id = id, FailureComponentId = failureComponentId });
        }

        public async Task<MaintenanceItemModel> GetMaintenanceItemByCode(string code)
        {
            return await _mediator.Send(new GetMaintenanceItemByCodeQuery { Code = code});
        }

        public async Task<IEnumerable<MaintenanceItemModel>> GetMaintenanceItems(int? failureComponentId, List<int> failureComponentIds = null)
        {
            return await _mediator.Send(new GetMaintenanceItemsQuery { FailureComponentId = failureComponentId, FailureComponentIds = failureComponentIds });
        }

        public async Task<MaintenanceItemModel> UpdateMaintenanceItem(UpdateMaintenanceItem maintenanceItem, Guid modifiedBy)
        {
            UpdateMaintenanceItemValidator validator = new UpdateMaintenanceItemValidator();

            ValidationResult results = validator.Validate(maintenanceItem);

            if (!results.IsValid)
            {
                var ex = new BusinessLogicException("maintenance item validation error", "maintenance item validation error");
                var validationErrors = new List<ValidationError>();
                foreach (var failure in results.Errors)
                {
                    validationErrors.Add(new ValidationError(failure.PropertyName, failure.ErrorMessage));
                }
                ex.ValidationErrors = validationErrors;
                throw ex;
            }

            var updateDbResponse = await _mediator.Send(new UpdateMaintenanceItemCommand
            {
                Id = maintenanceItem.Id,
                Title = maintenanceItem.Title,
                Code = maintenanceItem.Code,
                LabourTimeHours = maintenanceItem.LabourTimeHours,
                TruckModel = maintenanceItem.TruckModel,
                CostPerHour = maintenanceItem.CostPerHour,
                FailureComponentId = maintenanceItem.FailureComponentId,
                ModifiedAt = DateTime.Now,
                ModifiedBy = modifiedBy
            });

            if (updateDbResponse > 0)
            {
                return await GetMaintenanceItem(maintenanceItem.FailureComponentId, maintenanceItem.Id);
            }
            throw new BusinessLogicException("Could not update maintenance item, please retry", "Could not update maintenance item, please retry");
        }
    }
}
