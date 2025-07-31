using MediatR;

namespace BloodLink.Application.Commands.CreateBloodStock
{
    public class CreateBloodStockCommand : IRequest<int>
    {
        public string BloodType { get; set; }
        public string FactorRh { get; set; }
        public int BloodVolumeInML { get; set; }
    }
}
