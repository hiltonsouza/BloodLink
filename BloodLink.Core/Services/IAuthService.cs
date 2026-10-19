using BloodLink.Core.Entities;

namespace BloodLink.Core.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
        string ComputeSha256Hash(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
