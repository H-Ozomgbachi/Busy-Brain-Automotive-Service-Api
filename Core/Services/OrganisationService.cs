using Common.Contracts.Exceptions;
using Common.Contracts.Exceptions.Types;
using Common.Contracts.v1.Organisation;
using Common.Core.CQRS.Commands.Organisation;
using Common.Core.CQRS.Queries;
using Common.Core.Models;
using Common.Core.Services.Validation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Core.Services
{
    public class OrganisationService : IOrganisationService
    {
        private readonly IMediator _mediator;
        public OrganisationService(IMediator mediator)
        {
            _mediator = mediator;           
        }
        public async Task<OrganisationViewModel> Create(OrganisationPayload org, Guid currentUser)
        {
            OrganisationPayloadValidator validator = new OrganisationPayloadValidator();

            ValidationResult results = validator.Validate(org);

            if (!results.IsValid)
            {
                var ex = new BusinessLogicException("Organisation validation error", "Organisation validation error");
                var validationErrors = new List<ValidationError>();
                foreach (var failure in results.Errors)
                {
                    validationErrors.Add(new ValidationError(failure.PropertyName, failure.ErrorMessage));
                }
                ex.ValidationErrors = validationErrors;
                throw ex;
            }

            var newUserId = await _mediator.Send(new CreateOrganisationCommand()
            {
                UniqueID = Guid.NewGuid(),
                Name = org.Name,
                Phone = org.Phone,
                ContactEmail = org.ContactEmail,
                ReportEmail = org.ReportEmail,
                CreatedBy = currentUser,
                AccountType = org.AccountType
                
            });

            if (newUserId > 0)
            {
                return await GetById(newUserId);
            }
            else
            {
                throw new BusinessLogicException($"An organisation already exists with email: {org.ContactEmail}");
            }
        }

        public async Task<OrganisationViewModel> GetById(int id)
        {
            return await _mediator.Send(new GetOrganisationQuery() { Id = id });
        }
    }
}
