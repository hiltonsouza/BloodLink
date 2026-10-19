using BloodLink.Application.Queries.GetAllDonation;
using BloodLink.Application.Queries.GetAllDonations;
using BloodLink.Core.Entities;
using BloodLink.Core.Models;
using BloodLink.Core.Repositories;
using Moq;

namespace BloodLink.UnitTests.Queries
{
    public class GetAllDonationsCommandHandlerTests
    {
        [Fact]
        public async Task ThreeDonationsExist_Executed_ReturnDonationViewModels()
        {
            //padrão AAA

            //arrange
            var donations = new PaginationResult<Donation>
            {
                Data = new List<Donation>
                {
                    new Donation(1, 400),
                    new Donation(2, 420),
                    new Donation(3, 480),
                }
            };

            var donationRepositoryMock = new Mock<IDonationRepository>();
            donationRepositoryMock.Setup(donation => donation.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()).Result).Returns(donations);

            var getAllDonationsQuery = new GetAllDonationsQuery { Query = 0, Page = 1 };
            var getAllDonationsQueryHandler = new GetAllDonationsQueryHandler(donationRepositoryMock.Object);

            //act
            var paginationDonationViewModelList = await getAllDonationsQueryHandler.Handle(getAllDonationsQuery, new CancellationToken());

            //assert
            Assert.NotNull(paginationDonationViewModelList);
            Assert.NotEmpty(paginationDonationViewModelList.Data);
            Assert.Equal(donations.Data.Count, paginationDonationViewModelList.Data.Count);

            donationRepositoryMock.Verify(pr => pr.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()).Result, Times.Once);
        }
    }
}
