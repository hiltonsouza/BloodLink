using BloodLink.Core.Repositories;
using BloodLink.Core.Services;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLink.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly BloodLinkDbContext _context;

        public UnitOfWork(
            BloodLinkDbContext dbContext,
            IDonationRepository donations,
            IBloodStockRepository bloodStocks,
            IDonorRepository donors
            )
        {
            _context = dbContext;
            Donations = donations;
            BloodStocks = bloodStocks;
            Donors = donors;
        }

        public IDonationRepository Donations { get; }

        public IDonorRepository Donors { get; }

        public IBloodStockRepository BloodStocks { get; }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
        }
    }
}
