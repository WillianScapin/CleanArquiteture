using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Application.UseCases.CreateUser
{
    public sealed class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator() 
        {
            RuleFor(w => w.Email).NotEmpty().MaximumLength(50).EmailAddress();
            RuleFor(w => w.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
        }
    }
}
