using Common.Contracts.Exceptions;
using Common.Contracts.Exceptions.Types;
using Common.Contracts.v1.RepairLabourTime;
using Common.Core.CQRS.Commands.RepairLabourTime;
using Common.Core.CQRS.Queries.RepairLabourTime;
using Common.Core.Models;
using Common.Core.Models.RepairLabourTimeModels;
using Common.Core.Services.Validation.RepairLabourTime;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Core.Services.RepairLabourTimeService
{
    public class FailureComponentService : IFailureComponentService
    {
        private readonly IMediator _mediator;

        public FailureComponentService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<FailureComponentModel> CreateFailureComponent(CreateFailureComponent failureComponent, Guid modifiedBy)
        {
            CreateFailureComponentValidator validator = new CreateFailureComponentValidator();

            ValidationResult results = validator.Validate(failureComponent);

            if (!results.IsValid)
            {
                var ex = new BusinessLogicException("Failure component validation error", "Failure component validation error");
                var validationErrors = new List<ValidationError>();
                foreach (var failure in results.Errors)
                {
                    validationErrors.Add(new ValidationError(failure.PropertyName, failure.ErrorMessage));
                }
                ex.ValidationErrors = validationErrors;
                throw ex;
            }

            var createdFailureComponentId = await _mediator.Send(new CreateFailureComponentCommand
            {
                Title = failureComponent.Title,
                AssemblyOrSystemName = failureComponent.AssemblyOrSystemName,
                CreatedAt = DateTime.Now,
                ModifiedBy = modifiedBy
            });

            if(createdFailureComponentId > 0) return await GetFailureComponent(createdFailureComponentId);
            throw new BusinessLogicException("Could not create failure component, please retry", "Could not create failure component, please retry");
        }

        public async Task DeleteFailureComponent(int id)
        {
            await GetFailureComponent(id);
            bool isDeleted = await _mediator.Send(new DeleteFailureComponentCommand { Id = id });
            if (isDeleted == false) throw new BusinessLogicException("Could not delete failure component, please try again later", "Could not delete failure component, please try again later");
        }

        public async Task<FailureComponentModel> GetFailureComponent(int id)
        {
            return await _mediator.Send(new GetFailureComponentQuery { Id = id});
        }

        public async Task<PagedListResult<FailureComponentModel>> GetFailureComponents(PaginateFailureComponent paginate)
        {
            return await _mediator.Send(new GetFailureComponentsQuery
            {
                PageNumber = paginate.PageNumber,
                PageSize = paginate.PageSize
            });
        }

        public async Task<FailureComponentModel> UpdateFailureComponent(UpdateFailureComponent failureComponent, Guid modifiedBy)
        {
            UpdateFailureComponentValidator validator = new UpdateFailureComponentValidator();

            ValidationResult results = validator.Validate(failureComponent);

            if (!results.IsValid)
            {
                var ex = new BusinessLogicException("Failure component validation error", "Failure component validation error");
                var validationErrors = new List<ValidationError>();
                foreach (var failure in results.Errors)
                {
                    validationErrors.Add(new ValidationError(failure.PropertyName, failure.ErrorMessage));
                }
                ex.ValidationErrors = validationErrors;
                throw ex;
            }

            var updateDbResponse = await _mediator.Send(new UpdateFailureComponentCommand
            {
                Id = failureComponent.Id,
                Title = failureComponent.Title,
                AssemblyOrSystemName = failureComponent.AssemblyOrSystemName,
                ModifiedBy = modifiedBy
            });

            if (updateDbResponse > 0)
            {
                return await GetFailureComponent(failureComponent.Id);
            }
            throw new BusinessLogicException("Could not update failure component, please retry", "Could not update failure component, please retry");
        }
    }
}
