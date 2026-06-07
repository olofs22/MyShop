using MyShop.Application.DTOs;
using MyShop.Application.Interfaces;
using MyShop.Application.Services;
using MyShop.Domain.Entities;
using NSubstitute;
using Xunit;

namespace MyShop.Tests
{
    public class ProductServiceTests
    {
        private readonly IProductRepository _repo;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _repo = Substitute.For<IProductRepository>();
            _service = new ProductService(_repo);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new() { Id = 1, Name = "Pripps", Price = 55, CategoryId = 1, Category = new Category { Id = 1, Name = "Öl" } },
                new() { Id = 2, Name = "Kaptenen", Price = 33, CategoryId = 2, Category = new Category { Id = 2, Name = "Sprit" } }
            };
            _repo.GetAllWithCategoryAsync().Returns(products);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Pripps", Price = 55, CategoryId = 1, Category = new Category { Id = 1, Name = "Öl" } };
            _repo.GetByIdWithCategoryAsync(1).Returns(product);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Pripps", result!.Name);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            _repo.GetByIdWithCategoryAsync(99).Returns((Product?)null);

            // Act
            var result = await _service.GetByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ValidProduct_ReturnsCreatedProduct()
        {
            // Arrange
            var dto = new CreateProductDto { Name = "Fernet", Price = 43, CategoryId = 2 };

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.Equal("Fernet", result.Name);
            await _repo.Received(1).AddAsync(Arg.Any<Product>());
            await _repo.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task CreateAsync_EmptyName_ThrowsArgumentException()
        {
            // Arrange
            var dto = new CreateProductDto { Name = "", Price = 500, CategoryId = 1 };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(dto));
        }

        [Fact]
        public async Task CreateAsync_NegativePrice_ThrowsArgumentException()
        {
            // Arrange
            var dto = new CreateProductDto { Name = "Phone", Price = -10, CategoryId = 1 };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(dto));
        }

        [Fact]
        public async Task UpdateAsync_ExistingProduct_ReturnsTrue()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Old", Price = 100, CategoryId = 1 };
            _repo.GetByIdAsync(1).Returns(product);
            var dto = new UpdateProductDto { Name = "New", Price = 200, CategoryId = 1 };

            // Act
            var result = await _service.UpdateAsync(1, dto);

            // Assert
            Assert.True(result);
            _repo.Received(1).Update(Arg.Is<Product>(p => p.Name == "New"));
        }

        [Fact]
        public async Task UpdateAsync_NonExistingProduct_ReturnsFalse()
        {
            // Arrange
            _repo.GetByIdAsync(99).Returns((Product?)null);
            var dto = new UpdateProductDto { Name = "New", Price = 200, CategoryId = 1 };

            // Act
            var result = await _service.UpdateAsync(99, dto);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ExistingProduct_ReturnsTrue()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Laptop", Price = 100, CategoryId = 1 };
            _repo.GetByIdAsync(1).Returns(product);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result);
            _repo.Received(1).Delete(product);
        }

        [Fact]
        public async Task DeleteAsync_NonExistingProduct_ReturnsFalse()
        {
            // Arrange
            _repo.GetByIdAsync(99).Returns((Product?)null);

            // Act
            var result = await _service.DeleteAsync(99);

            // Assert
            Assert.False(result);
        }
    }
}