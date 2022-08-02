using WebApiProductCRUD.Repositories;

namespace WebApiProductCRUD.Data.Configuration
{
    internal static class RepositoriesServiceConfiguration
    {
        internal static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
