using BloodLink.Core.Entities;
using BloodLink.Core.Models;

namespace BloodLink.Core.Repositories
{
    public interface IDonationRepository
    {
        Task<PaginationResult<Donation>> GetAllAsync(int query, int page = 1);
        Task<PaginationResult<Donation>> GetDonationByDonorIdAsync(int donorId, int page = 1);
        Task<Donation> GetByIdAsync(int id);
        Task AddAsync(Donation donation);
        Task UpdateAsync(Donation donation);
        Task<List<Donation>> GetLastDaysDonationsAsync(int days);
        Task SaveChangesAsync();
    }
}
