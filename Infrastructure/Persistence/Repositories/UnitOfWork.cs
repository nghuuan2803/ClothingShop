using Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbContextTransaction _transaction = null!;
        private readonly IServiceProvider _serviceProvider;
        private readonly AppDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(IServiceProvider serviceProvider, AppDbContext appDbContext)
        {
            _serviceProvider = serviceProvider;
            _dbContext = appDbContext;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction has not been started.");

            try
            {
                await _dbContext.SaveChangesAsync();
                await _transaction.CommitAsync();
                DisposeTransaction();
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
                throw;
            }

        }
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                DisposeTransaction();
            }
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
        private void DisposeTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null!;
            }
        }
        public void Dispose()
        {
            DisposeTransaction();
            _dbContext.Dispose();
            _repositories.Clear();
        }
    }
}
