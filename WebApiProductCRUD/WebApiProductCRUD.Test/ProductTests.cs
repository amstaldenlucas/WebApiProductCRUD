using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebApiProductCRUD.Data;
using WebApiProductCRUD.Models;
using WebApiProductCRUD.Repositories;

namespace WebApiProductCRUD.Test
{
    public class ProductTests
    {
        [Fact]
        public async Task CreateValidProduct_CreateIsCalled_ReturnValidProduct()
        {
            // Arrange
            var product = new Fixture().Create<Product>();
            product.Id = null;
            //var appDbContextMock = new Mock<AppDbContext>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(nameof(ProductTests));
            var dbContext = new AppDbContext(options.Options);
            dbContext.Database.EnsureCreated();


            var productRepository = new ProductRepository(dbContext);
            // Act
            var result = await productRepository.Edit(product);

            //Asert
            Assert.Equal(product.Name, result.Model.Name);
            Assert.True(result.Model.Stock >= 0);
        }
    }
}
