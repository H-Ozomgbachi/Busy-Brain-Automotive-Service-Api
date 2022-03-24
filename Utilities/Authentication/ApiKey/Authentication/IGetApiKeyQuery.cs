using System.Threading.Tasks;

namespace Common.Api.Configuration.Authentication
{
    public interface IGetApiKeyQuery
    {
        Task<ApiKey> Execute(string providedApiKey);
    }
}
