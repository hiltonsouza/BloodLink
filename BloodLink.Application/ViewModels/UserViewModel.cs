using BloodLink.Core.Enums;

namespace BloodLink.Application.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel() { }

        public UserViewModel(int id, string fullName, string email, UserRole role, DateTime createdAt, bool isActive)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Role = role.ToString();
            CreatedAt = createdAt;
            IsActive = isActive;
        }

        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
