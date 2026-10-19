using BloodLink.Core.Repositories;

namespace BloodLink.Core.Services
{
    public interface IUnitOfWork
    {
        IDonationRepository Donations { get; }
        IDonorRepository Donors { get; }
        IBloodStockRepository BloodStocks { get; }
        IUserRepository Users { get; }

        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
