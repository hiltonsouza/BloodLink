using BloodLink.Application.Commands.Login;
using BloodLink.Core.Entities;
using BloodLink.Core.Enums;
using BloodLink.Core.Repositories;
using BloodLink.Core.Services;
using Moq;
using Xunit;

namespace BloodLink.UnitTests.Commands
{
    public class LoginCommandHandlerTest
    {
        [Fact]
        public async Task ValidCredentials_Executed_ReturnsToken()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            var user = new User("Dr. Roberto", "roberto.doctor@bloodlink.com", "stored_hash", UserRole.Doctor);
            userRepositoryMock.Setup(r => r.GetByEmailAsync("roberto.doctor@bloodlink.com"))
                .ReturnsAsync(user);

            authServiceMock.Setup(a => a.VerifyPassword("ValidSecret123", "stored_hash"))
                .Returns(true);

            authServiceMock.Setup(a => a.GenerateJwtToken(user))
                .Returns("mocked-jwt-token-string");

            unitOfWorkMock.SetupGet(u => u.Users).Returns(userRepositoryMock.Object);

            var command = new LoginCommand
            {
                Email = "roberto.doctor@bloodlink.com",
                Password = "ValidSecret123"
            };

            var handler = new LoginCommandHandler(unitOfWorkMock.Object, authServiceMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal("mocked-jwt-token-string", result.Data.Token);
            Assert.Equal("roberto.doctor@bloodlink.com", result.Data.Email);
            Assert.Equal(UserRole.Doctor.ToString(), result.Data.Role);
        }

        [Fact]
        public async Task InvalidPassword_Executed_ReturnsError()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();

            var user = new User("Dr. Roberto", "roberto.doctor@bloodlink.com", "stored_hash", UserRole.Doctor);
            userRepositoryMock.Setup(r => r.GetByEmailAsync("roberto.doctor@bloodlink.com"))
                .ReturnsAsync(user);

            authServiceMock.Setup(a => a.VerifyPassword("WrongPassword", "stored_hash"))
                .Returns(false);

            unitOfWorkMock.SetupGet(u => u.Users).Returns(userRepositoryMock.Object);

            var command = new LoginCommand
            {
                Email = "roberto.doctor@bloodlink.com",
                Password = "WrongPassword"
            };

            var handler = new LoginCommandHandler(unitOfWorkMock.Object, authServiceMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid email or password.", result.Message);
        }
    }
}
