using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class VariantRepository : Repository<ProductVariant>
    {
        public VariantRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<ProductVariant> GetSingleAsync(Expression<Func<ProductVariant, bool>> predicate)
        {
            return await dbSet
                .Include(p=>p.Product)
                .Include(p=>p.Inventories)
                .FirstOrDefaultAsync(predicate);
        }
    }
}
