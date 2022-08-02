using Microsoft.EntityFrameworkCore;
using WebApiProductCRUD.Data;
using WebApiProductCRUD.DataResult;
using WebApiProductCRUD.Models;

namespace WebApiProductCRUD.Repositories
{
    public abstract class BaseRepository<T> where T : class, IAppEntity
    {
        protected AppDbContext DbContext;
        protected DateTime CurrentDateTimeUtc = DateTime.UtcNow;

        protected BaseRepository(AppDbContext appDbContext)
        {
            DbContext = appDbContext;
        }

        public virtual async Task<IEnumerable<T>> Get(DateTime? date = null)
        {
            var baseQueryable = DbContext.Set<T>();

            var result = date.HasValue
                ? baseQueryable.Where(x => x.LastUpdateUtc > date)
                : baseQueryable;

            return await Task.FromResult(result);
        }

        public virtual async Task<T> Get(string id)
        {
            var item = await DbContext.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id);
            return item;
        }

        public virtual async Task<StatusResult<T>> CreateSingle(T model)
        {
            if (model is null)
                return new StatusResult<T>("Data model was null");

            if (model.Id != null)
                return new StatusResult<T>("Id cannot be set when creating new item");

            if (!ModelIsValid(model, out StatusResult<T> modelValidationResult))
                return modelValidationResult;

            DbContext.Add(model);
            await DbContext.SaveChangesAsync();

            return new StatusResult<T>
            {
                Success = true,
                Model = model,
            };
        }

        public virtual async Task<StatusResult<T>> Edit(T model)
        {
            if (!ModelIsValid(model, out StatusResult<T> modelValidationResult))
                return modelValidationResult;

            bool newRecord = string.IsNullOrWhiteSpace(model.Id);
            if (newRecord)
            {
                model.Id = Guid.NewGuid().ToString();
                model.CreatedUtc = DateTime.UtcNow;
            }
            model.LastUpdateUtc = DateTime.UtcNow;

            try
            {
                _ = newRecord ? DbContext.Add(model) : DbContext.Update(model);
                await DbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return new StatusResult<T>($"Error to edit item. Message: [{ex.Message}]");
            }

            return new StatusResult<T>()
            {
                Success = true,
                Model = model,
            };
        }

        public virtual async Task<StatusResult<T>> DeleteSingle(T model)
        {
            if (model is null)
                return new StatusResult<T>("Data model was null");

            if (model.Id != null)
                return new StatusResult<T>("Cannot delete item with id null");

            var item = await DbContext.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (item is null)
                return new StatusResult<T>("Item not found");

            item.LastUpdateUtc = DateTime.UtcNow;
            item.Deleted = true;
            await DbContext.SaveChangesAsync();

            return new StatusResult<T>()
            {
                Success = true,
                Model = item,
            };
        }

        protected virtual bool ModelIsValid(T item, out StatusResult<T> modelValidationResult)
        {
            modelValidationResult = new StatusResult<T>();
            return true;
        }
    }
}
