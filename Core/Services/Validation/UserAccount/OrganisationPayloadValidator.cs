using Common.Contracts.v1.Organisation;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Common.Core.Services.Validation
{
    public class OrganisationPayloadValidator : AbstractValidator<OrganisationPayload>
    {
        private readonly List<string> accountTypes = new List<string> { "owner" };
        public OrganisationPayloadValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please specify a name");
            RuleFor(x => x.ContactEmail).EmailAddress().WithMessage("Contact Email must be between 1 and 250 characters long");
            RuleFor(x => x.ReportEmail).EmailAddress().WithMessage("Please specify a Report Email");            
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Please specify a Phone number");           
            RuleFor(x => x.Phone).Length(7, 25).WithMessage("Phone number must be between 7 and 25 characters long");
            RuleFor(x => x.AccountType).NotEmpty().WithMessage("Please select account type").Must(p => accountTypes.Contains(p)).WithMessage($"Account type must be any of : " + String.Join(", ", accountTypes));
        }
    }
}
