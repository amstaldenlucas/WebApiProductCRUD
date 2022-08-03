using WebApiProductCRUD.DataResult;
using WebApiProductCRUD.Models;

namespace WebApiProductCRUD.Repositories
{
    public interface IProductRepository
    {
        Task<Product> Get(string id);
        Task<IEnumerable<Product>> Get(DateTime? date = null);
        Task<StatusResult<Product>> Edit(Product model);
        Task<StatusResult<Product>> Delete(Product model);
    }
}
