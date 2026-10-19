using BloodLink.Application.Commands.CreateBloodStock;
using BloodLink.Core.Entities;
using BloodLink.Core.Repositories;
using BloodLink.Core.Services;
using Moq;
using System.Reflection;

namespace BloodLink.UnitTests.Commands
{
    public class CreateBloodStockCommandHandlerTest
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnsBloodStockId()
        {
            // arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var bloodStockRepositoryMock = new Mock<IBloodStockRepository>();

            unitOfWorkMock.SetupGet(u => u.BloodStocks).Returns(bloodStockRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.BeginTransactionAsync()).Returns(Task.CompletedTask);
            unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1); // Simulate successful save
            unitOfWorkMock.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);

            var command = new CreateBloodStockCommand
            {
                BloodType = "A",
                FactorRh = "+",
                BloodVolumeInML = 450
            };

            // Simula o comportamento do AddAsync para definir o ID do BloodStock após a adição
            bloodStockRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<BloodStock>()))
                .Returns(Task.CompletedTask);

            var handler = new CreateBloodStockCommandHandler(unitOfWorkMock.Object);

            // act

            var result = await handler.Handle(command, CancellationToken.None);

            // assert
            bloodStockRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<BloodStock>()), Times.Once);
            unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
            unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
            unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
