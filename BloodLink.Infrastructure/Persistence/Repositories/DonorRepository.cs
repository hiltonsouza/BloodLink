using BloodLink.Core.Entities;
using BloodLink.Core.Models;
using BloodLink.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BloodLink.Infrastructure.Persistence.Repositories
{
    public class DonorRepository : IDonorRepository
    {
        private const int PAGE_SIZE = 10;
        private readonly BloodLinkDbContext _dbContext;

        public DonorRepository(BloodLinkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Donor donor)
        {
            await _dbContext.AddAsync(donor);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Donor> GetByIdAsync(int id)
        {
            return await _dbContext
                .Donors
                .Include(d => d.Donations) // Include donations 
                .SingleOrDefaultAsync(u => u.Id == id);
        }
        public async Task<PaginationResult<Donor>> GetAllAsync(string query, int page)
        {
            IQueryable<Donor> donors = _dbContext.Donors;
            if (!string.IsNullOrWhiteSpace(query))
            {
                donors = donors
                    .Where(p =>
                    p.Email.Contains(query) ||
                    p.FullName.Contains(query));
            }
            return await donors.GetPaged<Donor>(page, PAGE_SIZE);
        }

        public async Task<bool> GetByEmailAsync(string email)
        {
            return await _dbContext.Donors.AnyAsync(d => d.Email == email);
        }

        public async Task UpdateAsync(Donor donor)
        {
            _dbContext.Donors.Update(donor);
        }
    }
}
