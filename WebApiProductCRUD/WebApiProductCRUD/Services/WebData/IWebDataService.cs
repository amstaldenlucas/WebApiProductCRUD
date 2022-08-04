using WebApiProductCRUD.DataResult;
using WebApiProductCRUD.Models;
using WebApiProductCRUD.Models.Security;

namespace WebApiProductCRUD.Services.WebData
{
    public interface IWebDataService
    {
        Task<GetListResult<T>> Get<T>(string? id = null) where T : IAppEntity;

        string ApiEndpoint(Type type);

        Task<HttpResponseMessage> Post(string uri, object data);
    }
}
