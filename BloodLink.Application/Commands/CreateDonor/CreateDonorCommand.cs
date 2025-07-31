using BloodLink.Application.ViewModels;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BloodLink.Application.Commands.CreateDonor
{
    public class CreateDonorCommand : IRequest<ResultViewModel<int>>
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public double Weight { get; set; }
        public string BloodType { get; set; }
        public string RhFactor { get; set; }
        //public List<string> Role { get; set; }
        public string ZipCode { get; set; }
    }
}
