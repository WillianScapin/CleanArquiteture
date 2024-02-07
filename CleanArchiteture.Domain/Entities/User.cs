using CleanArchiteture.Domain.Exceptions;
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
        public string? Email { get; set; }
        public string? Name { get; set; }


        bool Validate()
        {
            //Validação de nome
            if (this.Name.Length < 3)
                throw new InvalidNameException("O nome não pode ter nemos do que 3 caracteres");

            if (this.Name.Length > 50)
                throw new InvalidNameException("O nome não pode ter mais do que 50 caracteres");


            //Validação de Email
            if(!ValidateEmail(this.Email))
                throw new InvalidEmailException("Email inválido");

            if(this.Email.Length > 50)
                throw new InvalidEmailException("Email não pode ter mais do que 50 caracteres");

            return true;
        }


        bool ValidateEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Verifica se o email corresponde ao padrão da expressão regular
            return Regex.IsMatch(email, pattern);
        }

    }
} 
