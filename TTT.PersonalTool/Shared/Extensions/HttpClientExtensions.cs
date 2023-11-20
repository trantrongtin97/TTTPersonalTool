using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace TTT.PersonalTool.Shared.Extensions
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Extension for Get Request handle token in header
        /// </summary>
        /// <typeparam name="T">Generic Object</typeparam>
        /// <param name="httpClient">Dynamic Client Proxy</param>
        /// <param name="url">Dynamic Client Proxy</param>
        /// <param name="token">JWT token</param>
        /// <returns>Object Type T after deserialize json string</returns>
        public static async Task<T> GetAsync<T>(this HttpClient httpClient,
            string url,
            string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.SendAsync(request);
            
            
            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<T>(responseBody, jsonSerializerOptions);
        }

        /// <summary>
        /// Extension for Post Request handle token in header
        /// </summary>
        /// <typeparam name="T">Generic Object</typeparam>
        /// <param name="httpClient">Dynamic Client Proxy</param>
        /// <param name="url">Dynamic Client Proxy</param>
        /// <param name="data">Data request</param>
        /// <param name="token">JWT token</param>
        /// <returns>Object Type T after deserialize json string</returns>
        public static async Task<T> PostAsync<T>(this HttpClient httpClient,
            string url,
            object data,
            string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token); ;

            request.Content = new StringContent(JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<T>(responseBody, jsonSerializerOptions);
        }

        /// <summary>
        /// Extension for Put Request handle token in header
        /// </summary>
        /// <typeparam name="T">Generic Object</typeparam>
        /// <param name="httpClient">Dynamic Client Proxy</param>
        /// <param name="url">Dynamic Client Proxy</param>
        /// <param name="data">Data request</param>
        /// <param name="token">JWT token</param>
        /// <returns>Object Type T after deserialize json string</returns>
        public static async Task<T> PutAsync<T>(this HttpClient httpClient,
            string url,
            object data,
            string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token); ;

            request.Content = new StringContent(JsonSerializer.Serialize(data),
               Encoding.UTF8,
               "application/json");

            var response = await httpClient.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<T>(responseBody, jsonSerializerOptions);
        }

        /// <summary>
        /// Extension for Delete Request handle token in header
        /// </summary>
        /// <typeparam name="T">Generic Object</typeparam>
        /// <param name="httpClient">Dynamic Client Proxy</param>
        /// <param name="url">Dynamic Client Proxy</param>
        /// <param name="token">JWT token</param>
        /// <returns>Int number after deserialize json string</returns>
        public static async Task<int> DeleteAsync(this HttpClient httpClient,
            string url,
            string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token); ;

            var response = await httpClient.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<int>(responseBody, jsonSerializerOptions);
        }
    }
}
