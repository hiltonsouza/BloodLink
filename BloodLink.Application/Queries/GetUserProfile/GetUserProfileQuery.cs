using BloodLink.Application.ViewModels;
using MediatR;

namespace BloodLink.Application.Queries.GetUserProfile
{
    public class GetUserProfileQuery : IRequest<ResultViewModel<UserViewModel>>
    {
        public GetUserProfileQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
    }
}
