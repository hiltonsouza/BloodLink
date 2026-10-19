using BloodLink.Core.Enums;

namespace BloodLink.Core.Entities
{
    public class User : BaseEntity
    {
        private User() { }

        public User(string fullName, string email, string passwordHash, UserRole role)
        {
            FullName = fullName;
            Email = email.ToLowerInvariant().Trim();
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public string FullName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public UserRole Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
        public void UpdatePassword(string newHash) => PasswordHash = newHash;
    }
}
