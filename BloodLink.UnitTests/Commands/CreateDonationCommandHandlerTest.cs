using BloodLink.Application.Commands.CreateDonation;
using BloodLink.Core.Entities;
using BloodLink.Core.Repositories;
using BloodLink.Core.Resources;
using BloodLink.Core.Services;
using MediatR;
using Moq;

namespace BloodLink.UnitTests.Commands
{
    public class CreateDonationCommandHandlerTest
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnDonationId()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var donationRepository = new Mock<IDonationRepository>();
            var donorRepository = new Mock<IDonorRepository>();
            var notificationService = new Mock<INotificationService>();
            var mediatorMock = new Mock<IMediator>();

            unitOfWorkMock.SetupGet(uow => uow.Donations).Returns(donationRepository.Object);
            unitOfWorkMock.SetupGet(uow => uow.Donors).Returns(donorRepository.Object);

            // Ensure the donor exists so the handler proceeds to AddAsync
            donorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Donor(
                    "Test Donor",
                    "test@email.com",
                    new DateTime(1990, 1, 1),
                    "Male",
                    70,
                    "A",
                    "+",
                    new Address
                    (
                    "Rua teste",
                    "São Paulo",
                    "SP",
                    "12345-678"
                    )

                ));

            var createDonationCommand = new CreateDonationCommand
            {
                DonorId = 2,
                BloodVolumeInML = 470
            };

            var createDonationCommandHandler = new CreateDonationCommandHandler(unitOfWorkMock.Object, notificationService.Object, mediatorMock.Object);

            //Act
            var id = await createDonationCommandHandler.Handle(createDonationCommand, new CancellationToken());

            //Assert
            Assert.True(id.IsSuccess); // or similar, depending on your result type

            donationRepository.Verify(pr => pr.AddAsync(It.IsAny<Donation>()), Times.Once);

            unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task RepositoryThrowsException_ReturnsErroResult()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var donationRepository = new Mock<IDonationRepository>();
            var donorRepository = new Mock<IDonorRepository>();
            var notificationService = new Mock<INotificationService>();
            var mediatorMock = new Mock<IMediator>();

            unitOfWorkMock.SetupGet(uow => uow.Donations).Returns(donationRepository.Object);
            unitOfWorkMock.SetupGet(uow => uow.Donors).Returns(donorRepository.Object);

            // Ensure the donor exists so the handler proceeds to AddAsync
            donorRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Donor(
                "Test Donor",                // fullName
                "test@email.com",            // email
                new DateTime(1990, 1, 1),    // birthDate
                "Male",                      // gender
                70,                          // weight
                "A",                         // bloodType
                "+",                         // rhFactor
                new Address(
                    "Rua teste",             // street
                    "São Paulo",             // city
                    "SP",                    // state
                    "12345-678"              // zipCode
        )
    ));

            donationRepository.Setup(repo => repo.AddAsync(It.IsAny<Donation>()))
                .ThrowsAsync(new Exception(Messages.DonorTransactionFailed));

            var createDonationCommand = new CreateDonationCommand
            {
                DonorId = 2,
                BloodVolumeInML = 400
            };

            var handler = new CreateDonationCommandHandler(unitOfWorkMock.Object, notificationService.Object, mediatorMock.Object);

            // Act
            var result = await handler.Handle(createDonationCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(Messages.DonationTransactionFailed, result.Message);

            donationRepository.Verify(pr => pr.AddAsync(It.IsAny<Donation>()), Times.Once);
            unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
        }
    }
}
