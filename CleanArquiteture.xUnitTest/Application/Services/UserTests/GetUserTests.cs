using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchiteture.Application.UseCases.GetUser;
using CleanArchiteture.Domain.Entities;
using CleanArchiteture.Domain.Interfaces;
using CleanArquiteture.UnitTest.Fixtures;
using Moq;
using Xunit;

namespace CleanArquiteture.UnitTest.Application.Services.UserTests
{
    public class GetUserTests : IClassFixture<AutoMapperFixture>
    {
        private readonly IMapper _mapper;

        public GetUserTests(AutoMapperFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ValidUser_GetIsCalled_ReturnValidGetUserResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new GetUserRequest(userId);
            var user = new User("test@test.com", "Test User") { Id = userId };
            
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Get(userId, It.IsAny<CancellationToken>()))
                             .ReturnsAsync(user);
            
            var getUserHandler = new GetUserHandler(userRepositoryMock.Object, _mapper);
            
            // Act
            var response = await getUserHandler.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.NotNull(response);
            Assert.Equal(userId, response.Id);
            Assert.Equal(user.Name, response.Name);
            Assert.Equal(user.Email, response.Email);
            
            userRepositoryMock.Verify(x => x.Get(userId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task InvalidUser_GetIsCalled_ReturnNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new GetUserRequest(userId);
            
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Get(userId, It.IsAny<CancellationToken>()))
                             .ReturnsAsync((User)null);
            
            var getUserHandler = new GetUserHandler(userRepositoryMock.Object, _mapper);
            
            // Act
            var response = await getUserHandler.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.Null(response);
            
            userRepositoryMock.Verify(x => x.Get(userId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
