using BloodLink.Core.Entities;
using BloodLink.Core.Models;
using BloodLink.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BloodLink.Infrastructure.Persistence.Repositories
{
    public class DonationRepository : IDonationRepository
    {
        private const int PAGE_SIZE = 10;
        private readonly BloodLinkDbContext _dbContext;
        public DonationRepository(BloodLinkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Donation donation)
        {
            await _dbContext.Donations.AddAsync(donation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginationResult<Donation>> GetAllAsync(int query, int page)
        {
            IQueryable<Donation> donations = _dbContext.Donations.Include(d => d.Donor);
            if (query > 0)
            {
                donations = donations
                    .Where(p =>
                    p.DonorId.Equals(query));
            }

            return await donations.GetPaged<Donation>(page, PAGE_SIZE);
        }
        public async Task<Donation> GetByIdAsync(int id)
        {
            return await _dbContext.Donations.Include(d => d.Donor).SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<PaginationResult<Donation>> GetDonationByDonorIdAsync(int donorId, int page = 1)
        {
            IQueryable<Donation> donations = _dbContext.Donations
                .Include(d => d.Donor)
                .Where(d => d.DonorId == donorId);

                return await donations.GetPaged<Donation>(page, PAGE_SIZE);
        }

        public async Task<List<Donation>> GetLastDaysDonationsAsync(int days)
        {
            var dateThreshold = DateTime.Now.AddDays(-days);

            return await _dbContext.Donations
                .Include(d => d.Donor)
                .Where(d => d.DonationDate >= dateThreshold)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Donation donation)
        {
            _dbContext.Donations.Update(donation);
        }
    }
}
