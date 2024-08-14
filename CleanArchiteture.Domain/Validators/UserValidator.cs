using CleanArchiteture.Domain.Entities;
using FluentValidation;

namespace CleanArchiteture.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            //Username
            RuleFor(custommer => custommer.Name)
            .NotNull().WithMessage("O nome não pode ser nulo")
            .NotEmpty().WithMessage("O nome não pode ser vazio")
            .MinimumLength(3).WithMessage("O nome deve ter mais do que 3 caracteres")
            .MaximumLength(50).WithMessage("O nome deve ter menos do que 50 caracteres");

            //Email
            RuleFor(custommer => custommer.Email)
            .NotNull().WithMessage("O e-mail não pode ser nulo")
            .NotEmpty().WithMessage("O e-mail não pode ser vazio")
            .EmailAddress().WithMessage("O e-mail deve ser um e-mail válido");
        }
    }
}
