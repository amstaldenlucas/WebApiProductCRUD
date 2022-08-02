namespace WebApiProductCRUD.Data.Context
{
    internal static class ConfigureAppServices
    {
        internal static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<DbSeeder>();
            return services;
        }
    }
}
