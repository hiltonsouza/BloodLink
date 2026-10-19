using BloodLink.Application.ViewModels;
using MediatR;

namespace BloodLink.Application.Commands.Login
{
    public class LoginCommand : IRequest<ResultViewModel<LoginResponseViewModel>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
