using BloodLink.Application.ViewModels;
using BloodLink.Core.Services;
using MediatR;

namespace BloodLink.Application.Queries.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, ResultViewModel<UserViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserProfileQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<UserViewModel>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return ResultViewModel<UserViewModel>.Error("User not found.");
            }

            var viewModel = new UserViewModel(
                user.Id,
                user.FullName,
                user.Email,
                user.Role,
                user.CreatedAt,
                user.IsActive
            );

            return ResultViewModel<UserViewModel>.Success(viewModel);
        }
    }
}
