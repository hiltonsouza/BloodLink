namespace BloodLink.Application.ViewModels
{
    public class LoginResponseViewModel
    {
        public LoginResponseViewModel() { }

        public LoginResponseViewModel(string token, string email, string fullName, string role, DateTime expiresAt)
        {
            Token = token;
            Email = email;
            FullName = fullName;
            Role = role;
            ExpiresAt = expiresAt;
        }

        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
