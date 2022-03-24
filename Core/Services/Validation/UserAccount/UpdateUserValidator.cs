using Common.Contracts.v1.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core.Services.Validation
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserModel>    
    {
        public UpdateUserValidator()
        {
            When(x => !string.IsNullOrWhiteSpace(x.FirstName) && !string.IsNullOrWhiteSpace(x.LastName), () => {
                RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please specify a first name");
                RuleFor(x => x.FirstName).Length(1, 250).WithMessage("First name must be between 1 and 250 characters long");
                RuleFor(x => x.LastName).NotEmpty().WithMessage("Please specify a last name");
                RuleFor(x => x.LastName).Length(1, 250).WithMessage("Last name must be between 1 and 250 characters long");
                
            });

            When(x => !string.IsNullOrWhiteSpace(x.Phone) && !string.IsNullOrWhiteSpace(x.CountryCode), () => {
                RuleFor(x => x.Phone).NotEmpty().WithMessage("Please specify a Phone number");
                RuleFor(x => x.CountryCode).NotEmpty().WithMessage("Please specify a Phone Country Code");
                RuleFor(x => x.Phone).Length(5, 25).WithMessage("Phone numbermust be between 5 and 25 characters long");
                RuleFor(x => x.CountryCode).Length(2, 10).WithMessage("Last name must be between 2 and 10 characters long");
            });


            When(x => !string.IsNullOrWhiteSpace(x.Email), () => {
                RuleFor(x => x.Email).EmailAddress();
                RuleFor(x => x.Email).Length(1, 300).WithMessage("email must be between 1 and 300 characters long");
            });


            When(x => !string.IsNullOrWhiteSpace(x.Password), () => {
                RuleFor(x => x.Password).NotEmpty().WithMessage("Please specify a password");
                RuleFor(x => x.Password).Length(8, 128).WithMessage("Password must be between 8 and 128 characters long");
            });
            
        }
    }
}
