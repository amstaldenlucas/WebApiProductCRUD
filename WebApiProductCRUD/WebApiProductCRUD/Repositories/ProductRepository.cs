using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProductCRUD.Data;
using WebApiProductCRUD.DataResult;
using WebApiProductCRUD.Models;

namespace WebApiProductCRUD.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public override async Task<IEnumerable<Product>> Get(DateTime? date = null)
            => await base.Get(date);

        public override async Task<StatusResult<Product>> Edit(Product model)
        {
            // do diferente thinks
            return await base.Edit(model);
        }

        public async Task<StatusResult<Product>> Delete(string id)
            => await Delete(id);
    }
}
