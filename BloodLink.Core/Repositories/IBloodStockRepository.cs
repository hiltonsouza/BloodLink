using BloodLink.Core.Entities;
using BloodLink.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLink.Core.Repositories
{
    public interface IBloodStockRepository
    {
        Task<PaginationResult<BloodStock>> GetAllAsync(string query, int page = 1);
        Task<List<BloodStock>> GetAllRawAsync();
        Task<BloodStock> GetByIdAsync(int id);
        Task<List<BloodStock>> GetBloodStockReportAsync(string bloodType, string rhFactor);
        Task<BloodStock> GetDetailsAsync(string bloodType, string rhFactor);
        Task AddAsync(BloodStock bloodStock);
        Task UpdateAsync(BloodStock bloodStock);
        Task SaveChangesAsync();
    }
}
