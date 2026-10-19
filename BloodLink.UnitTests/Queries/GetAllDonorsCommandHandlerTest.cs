using BloodLink.Application.Queries.GetAllDonors;
using BloodLink.Core.Entities;
using BloodLink.Core.Models;
using BloodLink.Core.Repositories;
using Moq;

namespace BloodLink.UnitTests.Queries
{
    public class GetAllDonorsCommandHandlerTest
    {
        [Fact]
        public async Task ThreeDonorsExist_Executed_ReturnDonorViewModels()
        {
            //padrão AAA

            //arrange

            var address1 = new Address("Rua Exemplo 1", "Cidade Exemplo", "SP", "00000000");
            var address2 = new Address("Rua Exemplo 2", "Cidade Exemplo", "SP", "00000001");
            var address3 = new Address("Rua Exemplo 3", "Cidade Exemplo", "SP", "00000002");


            var donors = new PaginationResult<Donor>
            {
                Data = new List<Donor>
                {
                    new Donor("Joao","joao.silva@exemple.com",DateTime.Now,"Male",78, "O","+", address1),
                    new Donor("Maria", "maria.silva@exemple.com", DateTime.Now, "Female", 65, "A", "-", address2),
                    new Donor("Carlos", "carlos.silva@exemple.com", DateTime.Now, "Male", 85, "B", "+", address3)
                }
            };

            var donorRepositoryMock = new Mock<IDonorRepository>();
            donorRepositoryMock.Setup(donor => donor.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result).Returns(donors);

            var getAllDonorsQuery = new GetAllDonorsQuery { SearchTerm = "", Page = 1 };
            var getAllDonorsQueryHandler = new GetAllDonorsQueryHandler(donorRepositoryMock.Object);

            //act
            var paginationDonorViewModelList = await getAllDonorsQueryHandler.Handle(getAllDonorsQuery, new CancellationToken());

            //assert
            Assert.NotNull(paginationDonorViewModelList);
            Assert.NotEmpty(paginationDonorViewModelList.Data);
            Assert.Equal(donors.Data.Count, paginationDonorViewModelList.Data.Count);

            donorRepositoryMock.Verify(pr => pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result, Times.Once);
        }
    }
}
