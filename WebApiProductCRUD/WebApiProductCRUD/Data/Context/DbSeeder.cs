using Microsoft.AspNetCore.Identity;
using WebApiProductCRUD.Models;

namespace WebApiProductCRUD.Data.Context
{
    public class DbSeeder
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<DbUser> _userManager;
        private readonly Random _random;
        public DbSeeder(AppDbContext dbContext, UserManager<DbUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _random = new Random();
        }

        public async Task SeedAsync(string[] args)
        {
            await _dbContext.Database.EnsureCreatedAsync();
            if (Array.IndexOf(args, "-cleanDb") >= 0)
                return;

            await CreateUser();
            await CreateProducts(5);
        }

        private async Task CreateUser()
        {
            var userEmail = "local@tests.io";
            var password = "@dmin123";

            var dbUser = await _userManager.FindByNameAsync(userEmail);
            if (dbUser is null)
            {
                dbUser = new DbUser
                {
                    Email = userEmail,
                    UserName = userEmail,
                    Name = "Fake user",
                    Document = "xxx.xxx.xxx-xx",
                };
                var result = await _userManager.CreateAsync(dbUser, password);
                if (result != IdentityResult.Success)
                    throw new InvalidOperationException("Could not create user");
            }
        }

        private async Task CreateProducts(int quantity)
        {
            var items = new List<Product>();
            int i = 1;
            while (i <= quantity)
            {
                items.Add(new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = $"Product {i++}",
                    Price = GetRandomPrice(),
                    Stock = GetRandomStock()
                });
            }

            await _dbContext.AddRangeAsync(items);
            await _dbContext.SaveChangesAsync();
        }

        private double GetRandomPrice()
        {
            var decimalValue = Math.Round(_random.NextDouble(), 2);
            return _random.Next(10, 50) + decimalValue;
        }

        private double GetRandomStock() => _random.Next(20, 300);
    }
}
