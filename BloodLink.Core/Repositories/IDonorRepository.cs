using BloodLink.Core.Entities;
using BloodLink.Core.Models;

namespace BloodLink.Core.Repositories
{
    public interface IDonorRepository
    {
        Task<Donor> GetByIdAsync(int id);
        Task<PaginationResult<Donor>> GetAllAsync(string query, int page = 1);
        Task<bool> GetByEmailAsync(string email);
        Task AddAsync(Donor donor);
        Task UpdateAsync(Donor donor);
    }
}
