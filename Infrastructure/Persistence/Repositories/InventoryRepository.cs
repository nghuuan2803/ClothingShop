using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class InventoryRepository : Repository<Inventory>
    {
        public InventoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Inventory> GetSingleAsync(Expression<Func<Inventory, bool>> predicate)
        {
            return await dbSet
                .Include(p=>p.Variant)
                    .ThenInclude(p=>p.Product)
                .Include(p=>p.Variant)
                    .ThenInclude(p=>p.Color)
                .Include(p=>p.Size)
                .FirstOrDefaultAsync(predicate);
        }
    }
}
