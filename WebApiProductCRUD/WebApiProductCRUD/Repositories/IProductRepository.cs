using WebApiProductCRUD.DataResult;
using WebApiProductCRUD.Models;

namespace WebApiProductCRUD.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product?>> Get(string? id = null);
        Task<StatusResult<Product>> Edit(Product model);
        Task<StatusResult<Product>> Delete(Product model);
    }
}
