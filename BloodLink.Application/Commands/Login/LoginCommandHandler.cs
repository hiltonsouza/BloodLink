using BloodLink.Application.ViewModels;
using BloodLink.Core.Services;
using MediatR;

namespace BloodLink.Application.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResultViewModel<LoginResponseViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<ResultViewModel<LoginResponseViewModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return ResultViewModel<LoginResponseViewModel>.Error("Invalid email or password.");
            }

            if (!user.IsActive)
            {
                return ResultViewModel<LoginResponseViewModel>.Error("User account is inactive.");
            }

            var isPasswordValid = _authService.VerifyPassword(request.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return ResultViewModel<LoginResponseViewModel>.Error("Invalid email or password.");
            }

            var token = _authService.GenerateJwtToken(user);
            var response = new LoginResponseViewModel(
                token,
                user.Email,
                user.FullName,
                user.Role.ToString(),
                DateTime.UtcNow.AddHours(8)
            );

            return ResultViewModel<LoginResponseViewModel>.Success(response);
        }
    }
}
