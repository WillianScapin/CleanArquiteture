using CleanArchiteture.Domain.Exceptions;
using CleanArchiteture.Domain.Validators;
using System.Text.RegularExpressions;

namespace CleanArchiteture.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        public User(string email, string name)
        {
            Email = email;
            Name = name;

            this.Validate();
        }

        //Normalmente se cria o provate set e a validação é feita no Domain (Core)
        public string Email { get; set; }
        public string Name { get; set; }


        bool Validate()
        {
            var validator = new UserValidator();
            var result = validator.Validate(this);

            if (result.IsValid)
                return true;
            else
            {
                var errors = string.Join(Environment.NewLine, result.Errors);
                throw new EntityException(errors);
            }
        }

    }
} 
