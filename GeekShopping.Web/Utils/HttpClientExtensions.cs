using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.Web.Utils
{
    public static class HttpClientExtensions
    {
        #region Attr
        private static MediaTypeHeaderValue contentType
            = new MediaTypeHeaderValue("application/json");
        #endregion

        #region General Methods(ReadContentAs, PostAsJson, PutAsJson) to consume API datas
        public static async Task<T> ReadContentAs<T>(
            this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling" +
                    $" the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(dataAsString,
                        new JsonSerializerOptions
                        { PropertyNameCaseInsensitive = true });
        }

        public static Task<HttpResponseMessage> PostAsJson<T>(
            this HttpClient httpClient, 
            string url, 
            T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJson<T>(
            this HttpClient httpClient,
            string url,
            T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PutAsync(url, content);
        }
        #endregion
    }
}
