using BloodLink.Application.ViewModels;
using BloodLink.Core.Enums;
using MediatR;

namespace BloodLink.Application.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<ResultViewModel<int>>
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Attendant;
    }
}
