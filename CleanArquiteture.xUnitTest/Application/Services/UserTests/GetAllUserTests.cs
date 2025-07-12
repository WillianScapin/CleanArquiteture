using AutoMapper;
using CleanArchiteture.Application.UseCases.GetAllUser;
using CleanArchiteture.Domain.Entities;
using CleanArchiteture.Domain.Interfaces;
using CleanArquiteture.UnitTest.Fixtures;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArquiteture.UnitTest.Application.Services.UserTests
{
    public class GetAllUserTests : IClassFixture<AutoMapperFixture>
    {
        private readonly IMapper _mapper;

        public GetAllUserTests(AutoMapperFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ValidUsers_GetAllIsCalled_ReturnValidGetAllUserResponse()
        {
            // Arrange
            var request = new GetAllUserRequest();
            var users = new List<User>
            {
                new User("user1@test.com", "User 1") { Id = Guid.NewGuid() },
                new User("user2@test.com", "User 2") { Id = Guid.NewGuid() },
                new User("user3@test.com", "User 3") { Id = Guid.NewGuid() }
            };
            
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetAll(It.IsAny<CancellationToken>()))
                             .ReturnsAsync(users);
            
            var getAllUserHandler = new GetAllUserHandler(userRepositoryMock.Object, _mapper);
            
            // Act
            var response = await getAllUserHandler.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.NotNull(response);
            Assert.Equal(users.Count, response.Count);
            
            userRepositoryMock.Verify(x => x.GetAll(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task EmptyList_GetAllIsCalled_ReturnEmptyList()
        {
            // Arrange
            var request = new GetAllUserRequest();
            var emptyUsers = new List<User>();
            
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetAll(It.IsAny<CancellationToken>()))
                             .ReturnsAsync(emptyUsers);
            
            var getAllUserHandler = new GetAllUserHandler(userRepositoryMock.Object, _mapper);
            
            // Act
            var response = await getAllUserHandler.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.NotNull(response);
            Assert.Empty(response);
            
            userRepositoryMock.Verify(x => x.GetAll(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
