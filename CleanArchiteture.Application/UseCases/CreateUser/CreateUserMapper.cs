using AutoMapper;
using CleanArchiteture.Domain.Entities;


namespace CleanArchiteture.Application.UseCases.CreateUser
{
    public sealed class CreateUserMapper : Profile
    {
        public CreateUserMapper() 
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, CreateUserResponse>();
        }
    }
}
