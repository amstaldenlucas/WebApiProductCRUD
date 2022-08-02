using AutoMapper;
using WebApiProductCRUD.Data.AutoMapper;

namespace WebApiProductCRUD.Data.Configuration
{
    public static class AutoMapperConfiguration
    {
        internal static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DbMappingProfile(provider.GetRequiredService<AppDbContext>()));
            }).CreateMapper());
            return services;
        }
    }
}
