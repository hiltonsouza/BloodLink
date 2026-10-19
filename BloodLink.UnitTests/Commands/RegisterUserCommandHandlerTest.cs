using BloodLink.Application.Commands.RegisterUser;
using BloodLink.Core.Entities;
using BloodLink.Core.Enums;
using BloodLink.Core.Repositories;
using BloodLink.Core.Services;
using Moq;
using Xunit;

namespace BloodLink.UnitTests.Commands
{
    public class RegisterUserCommandHandlerTest
    {
        [Fact]
        public async Task InputDataIsValid_Executed_ReturnsUserId()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            userRepositoryMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            authServiceMock.Setup(a => a.ComputeSha256Hash(It.IsAny<string>()))
                .Returns("hashed_password_sample");

            unitOfWorkMock.SetupGet(u => u.Users).Returns(userRepositoryMock.Object);

            var command = new RegisterUserCommand
            {
                FullName = "Enfermeiro Carlos",
                Email = "carlos.nurse@bloodlink.com",
                Password = "Password123!",
                Role = UserRole.Nurse
            };

            var handler = new RegisterUserCommandHandler(unitOfWorkMock.Object, authServiceMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
            unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task EmailAlreadyExists_Executed_ReturnsError()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            var existingUser = new User("User Exists", "exists@bloodlink.com", "hash", UserRole.Attendant);
            userRepositoryMock.Setup(r => r.GetByEmailAsync("exists@bloodlink.com"))
                .ReturnsAsync(existingUser);

            unitOfWorkMock.SetupGet(u => u.Users).Returns(userRepositoryMock.Object);

            var command = new RegisterUserCommand
            {
                FullName = "New User",
                Email = "exists@bloodlink.com",
                Password = "Password123!",
                Role = UserRole.Attendant
            };

            var handler = new RegisterUserCommandHandler(unitOfWorkMock.Object, authServiceMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("A user with this email already exists.", result.Message);
            userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
        }
    }
}
