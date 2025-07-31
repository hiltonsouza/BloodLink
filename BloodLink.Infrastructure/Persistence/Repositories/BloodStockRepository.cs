using BloodLink.Core.Entities;
using BloodLink.Core.Models;
using BloodLink.Core.Repositories;
using BloodLink.Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BloodLink.Infrastructure.Persistence.Repositories
{
    public class BloodStockRepository : IBloodStockRepository
    {
        private const int PAGE_SIZE = 10;
        private readonly BloodLinkDbContext _dbContext;

        public BloodStockRepository(BloodLinkDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(BloodStock bloodStock)
        {
            await _dbContext.BloodStocks.AddAsync(bloodStock);
        }
      
        public async Task<PaginationResult<BloodStock>> GetAllAsync(string query, int page)
        {
            IQueryable<BloodStock> bloodStocks = _dbContext.BloodStocks;

            if (!string.IsNullOrWhiteSpace(query))
            {
                var (bloodType, factorRh) = BloodService.ParseBloodQuery(query);

                bloodStocks = bloodStocks.Where(b =>
           (string.IsNullOrEmpty(bloodType) || b.BloodType == bloodType &&
           (string.IsNullOrEmpty(factorRh) || b.FactorRh == factorRh )));
            }

            Console.WriteLine($"Total after filtering: {bloodStocks.Count()}");

            return await bloodStocks.GetPaged<BloodStock>(page, PAGE_SIZE);
        }

        public async Task<List<BloodStock>> GetBloodStockReportAsync(string bloodType, string rhFactor)
        {
            var query = _dbContext.BloodStocks.AsNoTracking().AsQueryable();

            if(!string.IsNullOrEmpty(bloodType))
            {
                query = query.Where(bs => bs.BloodType == bloodType);
            }

            if(!string.IsNullOrEmpty(rhFactor))
            {
                query = query.Where(bs => bs.FactorRh == rhFactor);
            }

            return await query.ToListAsync();
        }

        public async Task<BloodStock> GetByIdAsync(int id)
        {
            return await _dbContext
                .BloodStocks
                .AsNoTracking()
                .SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BloodStock> GetDetailsAsync(string bloodType, string rhFactor)
        {
            return await _dbContext
                .BloodStocks
                .AsNoTracking()
                .Where(bs => bs.BloodType == bloodType && bs.FactorRh == rhFactor)
                .FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(BloodStock bloodStock)
        {
            _dbContext.BloodStocks.Update(bloodStock);

        }
    }
}
