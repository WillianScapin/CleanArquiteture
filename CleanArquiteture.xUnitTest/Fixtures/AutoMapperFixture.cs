using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchiteture.Application.UseCases.CreateUser;
using CleanArchiteture.Application.UseCases.DeleteUser;
using CleanArchiteture.Application.UseCases.GetAllUser;
using CleanArchiteture.Application.UseCases.GetUser;
using CleanArchiteture.Application.UseCases.UpdateUser;
using CleanArchiteture.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CleanArquiteture.UnitTest.Fixtures
{
    public class AutoMapperFixture : IDisposable
    {
        public IMapper Mapper { get; private set; }

        public AutoMapperFixture()
        {
            var config = new MapperConfiguration(cfg =>
            {
                #region User

                cfg.CreateMap<CreateUserRequest, User>();
                cfg.CreateMap<User, CreateUserResponse>();

                cfg.CreateMap<DeleteUserRequest, User>();
                cfg.CreateMap<User, DeleteUserResponse>();

                cfg.CreateMap<User, GetAllUserResponse>();

                cfg.CreateMap<GetUserRequest, User>();
                cfg.CreateMap<User, GetUserResponse>();

                cfg.CreateMap<UpdateUserRequest, User>();
                cfg.CreateMap<User, UpdateUserResponse>();

                #endregion
            });

            Mapper = config.CreateMapper();
        }

        public void Dispose()
        {
            // Limpeza se necessário
        }
    }
}
