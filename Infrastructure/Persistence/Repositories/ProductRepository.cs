using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>
    {

        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate = null!, CancellationToken cancellationToken = default)
        {
            if (predicate is null)
            {
                var products = await dbSet
                    .Where(p => !p.IsDeleted)
                    .Include(p => p.Category)
                    .Include(p => p.ProductTags)
                        .ThenInclude(p => p.Tag)
                    .ToListAsync();

                foreach (var item in products)
                {
                    item.Variants = await _dbContext.ProductVariants
                        .Where(p => p.ProductId == item.Id&& !p.IsDeleted)
                        .Include(p => p.Color)
                        .Include(p => p.Inventories)
                            .ThenInclude(p => p.Branch).ToListAsync();
                }
                return products;
            }
            else
            {
                var products = await dbSet.Where(predicate)
                    .Include(p => p.Category)
                    .Include(p => p.ProductTags)
                        .ThenInclude(p => p.Tag)
                    .ToListAsync();

                foreach (var item in products)
                {
                    item.Variants = await _dbContext.ProductVariants.Where(p => p.ProductId == item.Id).Include(p => p.Color).Include(p => p.Inventories).ThenInclude(p => p.Branch).ToListAsync();
                }
                return products;
            }
        }

        public async override Task<Product> GetSingleAsync(Expression<Func<Product, bool>> predicate)
        {
            var product = await dbSet
                    .Include(p => p.Category)
                    .Include(p => p.Variants)
                        .ThenInclude(p => p.Inventories)
                    .Include(p => p.ProductTags)
                        .ThenInclude(p => p.Tag)
                    .FirstOrDefaultAsync(predicate);
            if (product != null)
                product.Variants = await _dbContext.ProductVariants.Where(p => p.ProductId == product.Id).Include(p => p.Color).Include(p => p.Inventories).ThenInclude(p => p.Branch).ToListAsync();
            return product;
        }
    }
}
