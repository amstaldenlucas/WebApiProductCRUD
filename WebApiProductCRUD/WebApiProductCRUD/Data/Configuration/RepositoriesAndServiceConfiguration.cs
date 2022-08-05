using WebApiProductCRUD.Repositories;
using WebApiProductCRUD.Services.Security;
using WebApiProductCRUD.Services.WebData;

namespace WebApiProductCRUD.Data.Configuration
{
    internal static class RepositoriesAndServiceConfiguration
    {
        internal static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
