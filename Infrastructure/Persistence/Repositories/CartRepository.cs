using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class CartRepository : Repository<CartItem>
    {
        public CartRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<CartItem>> GetAllAsync(Expression<Func<CartItem, bool>> predicate = null!, CancellationToken cancellationToken = default)
        {
            return await dbSet.Where(predicate)
                .Include(p=>p.Variant)
                    .ThenInclude(p=>p.Product)
                .Include(p=>p.Variant)
                    .ThenInclude(p=>p.Color)
                .Include(p=>p.Size)
                .ToListAsync(cancellationToken);
        }
    }
}
