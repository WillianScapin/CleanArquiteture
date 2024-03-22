using CleanArchiteture.Application.UseCases.GetAllUser;
using CleanArchiteture.Application.UseCases.GetUser;
using CleanArchiteture.Domain.Entities;
using CleanArchiteture.Domain.Interfaces;
using MediatR;
using Moq;

namespace CleanArquiteture.UnitTest
{
    public class UserTest
    {
        //private readonly IMediator _mediator;
        Mock<IBaseRepository<User>> _userRepositoryMock;
        IUserRepository _userRepository;


        public UserTest()
        {
            //_mediator = mediator;
            _userRepositoryMock = new Mock<IBaseRepository<User>>();
            
        }

        [Fact]
        public void GetUser()
        {

        }
    }
}
