using BloodLink.Application.Commands.CreateDonor;
using BloodLink.Core.Entities;
using BloodLink.Core.Repositories;
using BloodLink.Core.Services;
using BloodLink.Core.Results;
using Moq;

namespace BloodLink.UnitTests.Commands
{
    public class CreateDonorCommandHandlerTest
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnDonorId()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var viaCepServiceMock = new Mock<IViaCepService>();
            var donorRepositoryMock = new Mock<IDonorRepository>();

            unitOfWorkMock.SetupGet(uow => uow.Donors).Returns(donorRepositoryMock.Object);

            viaCepServiceMock
                .Setup(v => v.GetAddressByZipCodeAsync(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(Result<Address>.Ok(new Address("Rua Exemplo", "Cidade Exemplo", "SP", "00000000")));

            // GetByEmailAsync retorna false (ou seja, não existe doador com esse e-mail)
            donorRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var createDonorCommand = new CreateDonorCommand
            {
                FullName = "Joao",
                Email = "joao.silva@exemple.com",
                BirthDate = DateTime.Now,
                Gender = "Male",
                Weight = 78,
                BloodType = "O",
                RhFactor = "+",
                ZipCode = "00000000"
            };

            var createDonorCommandHandler = new CreateDonorCommandHandler(unitOfWorkMock.Object, viaCepServiceMock.Object);

            //Act
            var id = await createDonorCommandHandler.Handle(createDonorCommand, new CancellationToken());

            //assert 
            Assert.True(id.Data >= 0);

            donorRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<Donor>()), Times.Once);

            unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }
        [Fact]
        public async Task EmailAlreadyExists_ReturnsError()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var donorRepositopryMock = new Mock<IDonorRepository>();
            var viaCepServiceMock = new Mock<IViaCepService>();

            unitOfWorkMock.SetupGet(u => u.Donors).Returns(donorRepositopryMock.Object);
            donorRepositopryMock.Setup(u => u.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(true);

            var handler = new CreateDonorCommandHandler(unitOfWorkMock.Object, viaCepServiceMock.Object);
            var command = new CreateDonorCommand { Email = "test@example.com" };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("A donor with this email already exists", result.Message);
            donorRepositopryMock.Verify(repo => repo.AddAsync(It.IsAny<Donor>()), Times.Never);
            unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
        }
        [Fact]
        public async Task InvalidZipCode_ReturnsError()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var viaCepServiceMock = new Mock<IViaCepService>();
            viaCepServiceMock
                .Setup(v => v.GetAddressByZipCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<Address>.Fail("Zipcode Not found"));

            var handler = new CreateDonorCommandHandler(unitOfWorkMock.Object, viaCepServiceMock.Object);
            var command = new CreateDonorCommand { ZipCode = "invalid_zip" };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Zipcode Not found", result.Message);
        }

        [Fact]
        public async Task UnderAgeDonor_ReturnsError()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var viaCepServiceMock = new Mock<IViaCepService>();
            var donorRepositoryMock = new Mock<IDonorRepository>();

            unitOfWorkMock.SetupGet(uow => uow.Donors).Returns(donorRepositoryMock.Object);

            viaCepServiceMock
                .Setup(v => v.GetAddressByZipCodeAsync(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(Result<Address>.Ok(new Address("Rua Exemplo", "Cidade Exemplo", "SP", "00000000")));
            donorRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var createDonorCommand = new CreateDonorCommand
            {
                FullName = "Young Donor",
                Email = "donor@exemplo.com",
                BirthDate = DateTime.Now.AddYears(-15), // 16 years old
                Gender = "Male",
                Weight = 60,
                BloodType = "A",
                RhFactor = "+",
                ZipCode = "00000000"
            };
            var handler = new CreateDonorCommandHandler(unitOfWorkMock.Object, viaCepServiceMock.Object);

            // Act
            var result = await handler.Handle(createDonorCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("idade", result.Message, StringComparison.OrdinalIgnoreCase);
            donorRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Donor>()), Times.Never);
            unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
        }

        [Fact]
        public async Task DonorWithInvalidWeight_ReturnsError()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var viaCepServiceMock = new Mock<IViaCepService>();
            var donorRepositoryMock = new Mock<IDonorRepository>();

            unitOfWorkMock.SetupGet(uow => uow.Donors).Returns(donorRepositoryMock.Object);
            viaCepServiceMock
                .Setup(v => v.GetAddressByZipCodeAsync(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(Result<Address>.Ok(new Address("Rua Exemplo", "Cidade Exemplo", "SP", "00000000")));

            donorRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var createDonorCommand = new CreateDonorCommand
            {
                FullName = "Joana",
                Email = "Joana@exemplo.com",
                BirthDate = DateTime.Now.AddYears(-25),
                Gender = "Female",
                Weight = 45, // Weight below the minimum threshold
                BloodType = "B",
                RhFactor = "-",
                ZipCode = "00000000"
            };
            var handler = new CreateDonorCommandHandler(unitOfWorkMock.Object, viaCepServiceMock.Object);

            // Act
            var result = await handler.Handle(createDonorCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("weight", result.Message, StringComparison.OrdinalIgnoreCase);
            donorRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Donor>()), Times.Never);
            unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
        }

        [Fact]
        public async Task ViaCepServiceThrowsException_ReturnsError()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var viaCepServiceMock = new Mock<IViaCepService>();
            var donorRepositoryMock = new Mock<IDonorRepository>();

            unitOfWorkMock.SetupGet(uow => uow.Donors).Returns(donorRepositoryMock.Object);
            viaCepServiceMock
                .Setup(v => v.GetAddressByZipCodeAsync(It.IsAny<string>(), CancellationToken.None))
                .ThrowsAsync(new Exception("ViaCep unavailable"));

            donorRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var createDonorCommand = new CreateDonorCommand
            {
                FullName = "Joao",
                Email = "joao@exemplo.com",
                BirthDate = DateTime.Now.AddYears(-30),
                Gender = "Male",
                Weight = 80,
                BloodType = "O",
                RhFactor = "+",
                ZipCode = "00000000"
            };
            var handler = new CreateDonorCommandHandler(unitOfWorkMock.Object, viaCepServiceMock.Object);

            // Act
            var result = await handler.Handle(createDonorCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("ViaCep", result.Message, StringComparison.OrdinalIgnoreCase);
            donorRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Donor>()), Times.Never);
            unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
        }
    }
}