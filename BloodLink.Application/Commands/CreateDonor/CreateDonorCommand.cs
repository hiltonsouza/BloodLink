using BloodLink.Application.ViewModels;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BloodLink.Application.Commands.CreateDonor
{
    public class CreateDonorCommand : IRequest<ResultViewModel<int>>
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public double Weight { get; set; }
        public string BloodType { get; set; } = string.Empty;
        public string RhFactor { get; set; } = string.Empty;
        //public List<string> Role { get; set; }
        public string ZipCode { get; set; } = string.Empty;
    }
}
