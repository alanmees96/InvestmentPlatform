using Common.Extension;
using System.Net.Http;
using System.Threading.Tasks;

namespace InvestmentWebPlatform.Service
{
    public abstract class ServiceBase
    {
        private string FormUrl(string action)
        {
            return BaseUrl() + action;
        }

        protected abstract string BaseUrl();
        protected async Task<TResult> GetAsync<TResult>(string action)
        {
            var urlRequest = FormUrl(action);

            var response = await _client.GetAsync(urlRequest);

            var responseContent = await response.GetContentAs<TResult>();

            return responseContent;
        }

        protected async Task<HttpResponseMessage> PostAsync<T>(string action, T objectContent)
        {
            var url = FormUrl(action);

            var content = objectContent.ToStringContent();

            var response = await _client.PostAsync(url, content);

            return response;
        }

        public HttpClient _client { get; }

        public ServiceBase(HttpClient client)
        {
            _client = client;
        }
    }
}