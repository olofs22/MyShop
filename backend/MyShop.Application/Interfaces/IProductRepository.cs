using System;
using System.Collections.Generic;
using System.Text;
using MyShop.Domain.Entities;

namespace MyShop.Application.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();
        Task<Product?> GetByIdWithCategoryAsync(int id);
    }
}
