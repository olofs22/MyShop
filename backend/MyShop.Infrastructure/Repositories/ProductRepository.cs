using Microsoft.EntityFrameworkCore;
using MyShop.Application.Interfaces;
using MyShop.Domain.Entities;
using MyShop.Infrastructure.Data;

namespace MyShop.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync() =>
            await _dbSet.Include(p => p.Category).ToListAsync();
        public async Task<Product?> GetByIdWithCategoryAsync(int id) =>
        await _dbSet.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }
}
