using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebApiProductCRUD.Data;
using WebApiProductCRUD.DataResult;
using WebApiProductCRUD.Models;
using WebApiProductCRUD.Models.Security;
using WebApiProductCRUD.Services.Security;

namespace WebApiProductCRUD.Services.WebData
{
    public class HttpClientDataService : IWebDataService
    {
        private readonly AppDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Token? _token;
        private readonly string _baseUrl;

        public HttpClientDataService(AppDbContext dbContext, ITokenService tokenService, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
            _baseUrl = GetBaseUrl();
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetListResult<T>> Get<T>(string? id = null) where T : IAppEntity
        {
            try
            {
                await EnsureTokenInRequestAsync();
                var uri = ApiEndpoint(typeof(T)) + "/Get";
                uri += $"/{id}" ?? "";
                var response = await _httpClient.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                    return GetListResult<T>.ErrorWithMessages("Não houve resposta de sucesso na chamada ao servidor");

                var responseAsString = await response.Content.ReadAsStringAsync();
                var deserializedList = JsonConvert.DeserializeObject<IEnumerable<T>>(responseAsString);

                if (deserializedList is null)
                    return new GetListResult<T>();

                return GetListResult<T>.SuccessWithItems(deserializedList);
            }
            catch (Exception e)
            {
                return GetListResult<T>.ErrorWithMessages(e.ToString());
            }
        }

        public async Task<StatusResult<T>> Post<T>(string uri, object data)
        {
            var serializedData = JsonConvert.SerializeObject(data);
            await EnsureTokenInRequestAsync();

            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(serializedData),
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return new StatusResult<T>("Não houve resposta de sucesso na chamada ao servidor");

            var responseAsString = await response.Content.ReadAsStringAsync();
            var deserializedItem = JsonConvert.DeserializeObject<StatusResult<T>>(responseAsString);

            if (deserializedItem is null)
                return new StatusResult<T>();

            return deserializedItem;
        }


        public string ApiEndpoint(Type type)
            => $"{_baseUrl}/Api/{type.Name}";

        private string GetBaseUrl(string? baseUrl = null)
        {
            baseUrl ??= "https://localhost:2020";
            return baseUrl;
        }

        private async Task EnsureTokenInRequestAsync()
        {
            if (_token != null && TokenExpirationString.ToDateTime(_token.Expiration) > DateTime.UtcNow.AddSeconds(2))
            {
                SetTokenInHttpClient(_httpClient);
                return;
            }

            if (await ConfigureToken())
            {
                SetTokenInHttpClient(_httpClient);
                return;
            }

            throw new SecurityTokenExpiredException();
        }

        private void SetTokenInHttpClient(HttpClient httpClient)
        {
            if (httpClient.DefaultRequestHeaders.Authorization?.Parameter != _token.AccessToken)
            {
                httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", _token.AccessToken);
            }
        }

        private async Task<bool> ConfigureToken()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (userName is null)
                return false;

            var dbUser = await _dbContext.DbUsers
                .FirstOrDefaultAsync(x => x.Email == userName);

            _token = _tokenService.GenerateToken(dbUser);
            return true;
        }
    }
}
