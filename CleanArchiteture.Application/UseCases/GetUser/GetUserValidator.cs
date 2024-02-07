using CleanArchiteture.Application.UseCases.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Application.UseCases.GetUser
{
    public sealed class GetUserValidator : AbstractValidator<CreateUserRequest>
    {
        public GetUserValidator()
        {
            RuleFor(w => w.Email).NotEmpty().MaximumLength(50).EmailAddress();
            RuleFor(w => w.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
        }
    }
}
