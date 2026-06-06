using MyShop.Application.Interfaces;
using MyShop.Domain.Entities;
using MyShop.Infrastructure.Data;

namespace MyShop.Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }
}