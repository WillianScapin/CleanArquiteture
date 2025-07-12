using AutoMapper;
using CleanArchiteture.Application.UseCases.DeleteUser;
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
    public class DeleteUserTests : IClassFixture<AutoMapperFixture>
    {
        private readonly IMapper _mapper;

        public DeleteUserTests(AutoMapperFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ValidUser_DeleteIsCalled_ReturnValidDeleteUserResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new DeleteUserRequest(userId);
            var user = new User("test@test.com", "Test User") { Id = userId };
            
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            
            userRepositoryMock.Setup(x => x.Get(userId, It.IsAny<CancellationToken>()))
                             .ReturnsAsync(user);
            
            var deleteUserHandler = new DeleteUserHandler(unitOfWorkMock.Object, userRepositoryMock.Object, _mapper);
            
            // Act
            var response = await deleteUserHandler.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.NotNull(response);
            Assert.Equal(userId, response.Id);
            
            userRepositoryMock.Verify(x => x.Get(userId, It.IsAny<CancellationToken>()), Times.Once);
            userRepositoryMock.Verify(x => x.Delete(user), Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task InvalidUser_DeleteIsCalled_ReturnNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new DeleteUserRequest(userId);
            
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            
            userRepositoryMock.Setup(x => x.Get(userId, It.IsAny<CancellationToken>()))
                             .ReturnsAsync((User)null);
            
            var deleteUserHandler = new DeleteUserHandler(unitOfWorkMock.Object, userRepositoryMock.Object, _mapper);
            
            // Act
            var response = await deleteUserHandler.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.Null(response);
            
            userRepositoryMock.Verify(x => x.Get(userId, It.IsAny<CancellationToken>()), Times.Once);
            userRepositoryMock.Verify(x => x.Delete(It.IsAny<User>()), Times.Never);
            unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
