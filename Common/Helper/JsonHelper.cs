using System.Text.Json;

namespace Common.Helper
{
    public static class JsonHelper
    {
        public static string ToJson<T>(T content)
        {
            return JsonSerializer.Serialize(content);
        }

        public static T FromJson<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default;
            }

            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(json, jsonOptions);
        }
    }
}