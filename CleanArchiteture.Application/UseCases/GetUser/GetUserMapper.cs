using AutoMapper;
using CleanArchiteture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Application.UseCases.GetUser
{
    public sealed class GetUserMapper : Profile
    {
        public GetUserMapper() 
        {
            CreateMap<GetUserRequest, User>();
            CreateMap<User, GetUserResponse>();
        }
    }
}
