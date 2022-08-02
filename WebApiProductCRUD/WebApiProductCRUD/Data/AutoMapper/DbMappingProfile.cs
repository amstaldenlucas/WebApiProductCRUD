using AutoMapper;
using WebApiProductCRUD.Areas.Web.ViewModels;
using WebApiProductCRUD.Models;
using WebApiProductCRUD.Utils;

namespace WebApiProductCRUD.Data.AutoMapper
{
    public class DbMappingProfile : Profile
    {
        private readonly AppDbContext _dbContext;

        public DbMappingProfile(AppDbContext dbContext)
        {
            _dbContext = dbContext;

            CreateMap<Product, ProductVm>()
                .ForMember(x => x.DisplayPrice,
                    cfg => cfg.MapFrom((md) => GetDisplayPrice(md.Price)))
                .ReverseMap();
        }

        private string GetDisplayPrice(double price)
            => price.ToDisplay();
    }
}
