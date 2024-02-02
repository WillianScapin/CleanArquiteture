using AutoMapper;
using CleanArchiteture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Application.UseCases.GetAllUser
{
    internal class GetAllUserMapper : Profile
    {
        public GetAllUserMapper() 
        {
            CreateMap<User, GetAllUserResponse>();
        }
    }
}
