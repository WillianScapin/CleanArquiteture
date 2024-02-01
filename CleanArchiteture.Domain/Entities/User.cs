using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        //Normalmente se cria o provate set e a validação é feita no Domain (Core)
        public string? Email { get; set; }
        public string? Name { get; set; }
    }
} 
