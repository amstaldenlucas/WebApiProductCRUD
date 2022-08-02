using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProductCRUD.Data.Configuration
{
    internal static class ServiceDbContextConfiguration
    {
        internal static IServiceCollection ConfigureDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                Func<DbContextOptionsBuilder, DbContextOptionsBuilder> optionsHandler
                    = configuration[Const.UseInMemoryDatabase] switch
                    {
                        Const.StringTrue => GetHandlerUseInMemory(),
                        _ => GetHandlerUseSqlServer(configuration),
                    };

                optionsHandler(options);
            });

            return services;
        }

        private static Func<DbContextOptionsBuilder, DbContextOptionsBuilder> GetHandlerUseInMemory()
            => (options) => options.UseInMemoryDatabase("Test");

        public static Func<DbContextOptionsBuilder, DbContextOptionsBuilder> GetHandlerUseSqlServer(IConfiguration configuration)
        {
            return (options) =>
            {
                options.EnableSensitiveDataLogging();

                options.UseSqlServer(configuration.GetConnectionString("ConnDbWebApiProductCRUD"));
                return options;
            };
        }
    }
}
