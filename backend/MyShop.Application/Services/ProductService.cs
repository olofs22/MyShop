using MyShop.Application.DTOs;
using MyShop.Application.Interfaces;
using MyShop.Domain.Entities;

namespace MyShop.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllWithCategoryAsync();
        return products.Select(MapToDto);
    }
    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdWithCategoryAsync(id);
        return product is null ? null : MapToDto(product);
    }
    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Name is required.");
        if (dto.Price < 0)
            throw new ArgumentException("Price cannot be negative.");

        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            CategoryId = dto.CategoryId
        };

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return MapToDto(product);
    }
    public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null) return false;

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.CategoryId = dto.CategoryId;

        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null) return false;

        _productRepository.Delete(product);
        await _productRepository.SaveChangesAsync();
        return true;
    }
    private static ProductDto MapToDto(Product p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Price = p.Price,
        CategoryId = p.CategoryId,
        CategoryName = p.Category?.Name ?? string.Empty
    };
}