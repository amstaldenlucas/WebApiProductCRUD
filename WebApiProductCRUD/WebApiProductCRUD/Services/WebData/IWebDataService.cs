using WebApiProductCRUD.DataResult;
using WebApiProductCRUD.Models;

namespace WebApiProductCRUD.Services.WebData
{
    public interface IWebDataService
    {
        Task<GetListResult<T>> Get<T>(string? id = null) where T : IAppEntity;

        string ApiEndpoint(Type type);

        Task<StatusResult<T>> Post<T>(string uri, object data);
    }
}
