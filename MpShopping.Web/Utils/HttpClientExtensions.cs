using System.Text.Json;
using System.Net.Http.Headers;

namespace MpShopping.Web.Utils
{
    public static class HttpClientExtensions
    {
        private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Ocorreu algo errado ao chamar a API : " + 
                    $"{response.ReasonPhrase}");

            var dataString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(dataString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public static Task<HttpResponseMessage> PostAsJson<T> (this HttpClient httpClient, string url, T data)
        {
            var stringData = JsonSerializer.Serialize(data);
            var content = new StringContent(stringData);
            content.Headers.ContentType = contentType;  
            return httpClient.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var stringData = JsonSerializer.Serialize(data);
            var content = new StringContent(stringData);
            content.Headers.ContentType = contentType;
            return httpClient.PutAsync(url, content);
        }
    }
}
