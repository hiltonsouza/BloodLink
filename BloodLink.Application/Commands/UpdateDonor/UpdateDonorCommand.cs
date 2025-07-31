using BloodLink.Application.ViewModels;
using BloodLink.Core.Entities;
using MediatR;

namespace BloodLink.Application.Commands.UpdateDonor
{
    public class UpdateDonorCommand : IRequest<ResultViewModel<Unit>>
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = string.Empty;
        public double Weight { get; set; }
        public string BloodType { get; set; } = string.Empty;
        public string RhFactor { get; set; } = string.Empty;
        public Address Address { get; set; }
        //public bool Active { get; set; } = true; // Para soft delete ou desativação
        public DateTime UpdatedAt { get; set; } = DateTime.Now; // Para rastrear alterações
    }
}
