using BloodLink.Application.ViewModels;
using BloodLink.Core.Entities;
using BloodLink.Core.Services;
using MediatR;

namespace BloodLink.Application.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResultViewModel<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<ResultViewModel<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return ResultViewModel<int>.Error("A user with this email already exists.");
            }

            var passwordHash = _authService.ComputeSha256Hash(request.Password);
            var user = new User(request.FullName, request.Email, passwordHash, request.Role);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitAsync();

                return ResultViewModel<int>.Success(user.Id);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return ResultViewModel<int>.Error($"An error occurred while creating user: {ex.Message}");
            }
        }
    }
}
