using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchiteture.Application.UseCases.UpdateUser;
using CleanArchiteture.Domain.Entities;
using CleanArchiteture.Domain.Interfaces;
using CleanArquiteture.UnitTest.Fixtures;
using Moq;
using Xunit;

namespace CleanArquiteture.UnitTest.Application.Services.UserTests
{
    public class UpdateUserTests : IClassFixture<AutoMapperFixture>
    {
        private readonly IMapper _mapper;

        public UpdateUserTests(AutoMapperFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ValidUser_UpdateCalled_ReturnValidUpdateUserResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new UpdateUserRequest(userId, "scapinwill@gmail.com", "Willian Teste");
            var user = new User("old@test.com", "Old Name") { Id = userId };
            
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            
            userRepositoryMock.Setup(x => x.Get(userId, It.IsAny<CancellationToken>()))
                             .ReturnsAsync(user);
            
            var updateUserHandler = new UpdateUserHandler(unitOfWorkMock.Object, userRepositoryMock.Object, _mapper);
            
            // Act
            var response = await updateUserHandler.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.Name, response.Name);
            Assert.Equal(request.Email, response.Email);
            Assert.Equal(userId, response.Id);
            
            userRepositoryMock.Verify(x => x.Get(userId, It.IsAny<CancellationToken>()), Times.Once);
            userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task InvalidUser_UpdateCalled_ReturnNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new UpdateUserRequest(userId, "scapinwill@gmail.com", "Willian Teste");
            
            var userRepositoryMock = new Mock<IUserRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            
            userRepositoryMock.Setup(x => x.Get(userId, It.IsAny<CancellationToken>()))
                             .ReturnsAsync((User)null);
            
            var updateUserHandler = new UpdateUserHandler(unitOfWorkMock.Object, userRepositoryMock.Object, _mapper);
            
            // Act
            var response = await updateUserHandler.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.Null(response);
            
            userRepositoryMock.Verify(x => x.Get(userId, It.IsAny<CancellationToken>()), Times.Once);
            userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
            unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
