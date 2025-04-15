using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate = null!, CancellationToken cancellationToken = default)
        {
            if(predicate == null)
            {
                var order = await dbSet.Include(p => p.Customer)
                            .Include(p => p.Items)
                                .ThenInclude(p=>p.Variant)
                                .ThenInclude(p=>p.Product)
                            .Include(p=>p.Items)
                                .ThenInclude(p=>p.Size)
                            .Include(p=>p.Items)
                                .ThenInclude(p=>p.Variant)
                                .ThenInclude(p=>p.Color)
                            .ToListAsync();

                return order;
            }
            else
            {
                var order = await dbSet.Where(predicate)
                            .Include(p => p.Customer)
                            .Include(p => p.Items)
                                .ThenInclude(p => p.Variant)
                                .ThenInclude(p => p.Product)
                            .Include(p => p.Items)
                                .ThenInclude(p => p.Size)
                            .Include(p => p.Items)
                                .ThenInclude(p => p.Variant)
                                .ThenInclude(p => p.Color)
                            .ToListAsync();

                return order;
            }
        }

        public async override Task<Order> GetSingleAsync(Expression<Func<Order, bool>> predicate)
        {
            return await dbSet.Include(p => p.Customer)
                           .Include(p => p.Items)
                               .ThenInclude(p => p.Variant)
                               .ThenInclude(p => p.Product)
                           .Include(p => p.Items)
                               .ThenInclude(p => p.Size)
                           .Include(p => p.Items)
                               .ThenInclude(p => p.Variant)
                               .ThenInclude(p => p.Color)
                           .FirstOrDefaultAsync(predicate);
        }
    }
}
