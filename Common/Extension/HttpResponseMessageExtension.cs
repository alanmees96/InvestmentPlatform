using Common.Helper;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common.Extension
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<T> GetContentAs<T>(this HttpResponseMessage value)
        {
            var contentJson = await value?.Content?.ReadAsStringAsync();

            if (string.IsNullOrEmpty(contentJson))
            {
                return default;
            }

            var contentObject = JsonHelper.FromJson<T>(contentJson);

            return contentObject;
        }
    }
}