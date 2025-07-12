using AutoMapper;
using CleanArchiteture.Application.UseCases.CreateUser;
using CleanArchiteture.Domain.Entities;
using CleanArchiteture.Domain.Interfaces;
using CleanArquiteture.UnitTest.Fixtures;
using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArquiteture.UnitTest.Application.Services.UserTests
{
    public class CreateUserTests : IClassFixture<AutoMapperFixture>
    {
        private readonly IMapper _mapper;

        public CreateUserTests(AutoMapperFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ValidUser_CreateIsCalled_ReturnValidCreateUserResponse()
        {
            // Arrange
            var request = new CreateUserRequest("scapinwill@gmail.com", "Willian Scapin");
            var user = new User("scapinwill@gmail.com", "Willian Scapin");
            
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            
            var createUserHandler = new CreateUserHandler(unitOfWorkMock.Object, userRepositoryMock.Object, _mapper);
            
            // Act
            var response = await createUserHandler.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.Name, response.Name);
            Assert.Equal(request.Email, response.Email);
            
            userRepositoryMock.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
