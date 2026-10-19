using BloodLink.Core.Entities;
using BloodLink.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BloodLink.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BloodLinkDbContext _dbContext;

        public UserRepository(BloodLinkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var normalizedEmail = email.ToLowerInvariant().Trim();
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == normalizedEmail);
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
