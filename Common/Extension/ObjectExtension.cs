using Common.Helper;
using System.Net.Http;
using System.Text;

namespace Common.Extension
{
    public static class ObjectExtension
    {
        public static StringContent ToStringContent(this object value)
        {
            var contentJson = JsonHelper.ToJson(value);

            return new StringContent(contentJson, Encoding.UTF8, "application/json");
        }
    }
}