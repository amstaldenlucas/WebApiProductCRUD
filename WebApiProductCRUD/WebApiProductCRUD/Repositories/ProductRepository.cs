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

        protected override bool ModelIsValid(Product item, out StatusResult<Product> modelValidationResult)
        {
            var message = string.Empty;
            modelValidationResult = new StatusResult<Product> { Success = true };

            if (string.IsNullOrWhiteSpace(item.Name?.Trim()))
            {
                message = $"Invalid value for [{nameof(item.Name)}]";
                modelValidationResult.Success = false;
                modelValidationResult.Messages.Add(message);
                modelValidationResult.KeyAndErrorMessages.Add(new KeyAndErrorMessage(nameof(item.Name), message));
            }

            if (item.Price < 0)
            {
                message = $"Invalid value for [{nameof(item.Price)}]";
                modelValidationResult.Success = false;
                modelValidationResult.Messages.Add(message);
                modelValidationResult.KeyAndErrorMessages.Add(new KeyAndErrorMessage(nameof(item.Price), message));
            }

            if (item.Stock < 0)
            {
                message = $"Invalid value for [{nameof(item.Stock)}]";
                modelValidationResult.Success = false;
                modelValidationResult.Messages.Add(message);
                modelValidationResult.KeyAndErrorMessages.Add(new KeyAndErrorMessage(nameof(item.Stock), message));
            }

            return modelValidationResult.Success;
        }
    }
}
