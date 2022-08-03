using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebApiProductCRUD.Data;
using WebApiProductCRUD.Models;
using WebApiProductCRUD.Repositories;
using Shouldly;

namespace WebApiProductCRUD.Test
{
    public class ProductTests
    {
        private readonly AppDbContext _dbContext;
        public ProductTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(nameof(ProductTests));
            _dbContext = new AppDbContext(options.Options);
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task CreateValidProduct_CreateIsCalled_ReturnValidProduct()
        {
            // Arrange
            var product = new Fixture().Create<Product>();
            product.Id = null;

            //var options = new DbContextOptionsBuilder<AppDbContext>()
            //    .UseInMemoryDatabase(nameof(ProductTests));
            //var dbContext = new AppDbContext(options.Options);
            //dbContext.Database.EnsureCreated();
            IProductRepository productRepository = new ProductRepository(_dbContext);

            // Act
            var result = await productRepository.Edit(product);


            //Asert
            Assert.Equal(product.Name, result.Model.Name);
            result.Model.Id.ShouldNotBe(string.Empty);
            result.Model.Id.ShouldNotBe(null);
            Assert.True(result.Model.Stock >= 0);
            Assert.Single(_dbContext.Products);
        }

        [Fact]
        public void CreateValidProduct_SetInvalidPrice_KeepLastValidValue()
        {
            //Arrange
            var product = new Fixture().Create<Product>();
            product.Id = null;

            //Act
            var oldPrice = product.Price;
            product.Price = -12.2;

            //Assert
            Assert.True(product.Price >= 0);
        }

        [Fact]
        public async Task CreateValidProduct_CreateIsCalledThenDeleteIsCalled_DeleteProduct()
        {
            var product = new Fixture().Create<Product>();
            product.Id = null;

            IProductRepository productRepository = new ProductRepository(_dbContext);

            var result = await productRepository.Edit(product);
            var delete = await productRepository.Delete(result.Model);


            Assert.True(!_dbContext.Products
                .Where(x => x.Id == result.Model.Id)
                .Where(x => !x.Deleted)
                .Any());
        }
    }
}
