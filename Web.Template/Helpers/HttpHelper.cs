namespace Web.Template.Helpers
{
    using System.Net;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;

    public class HttpHelper
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        public HttpHelper()
        {
            _httpClient = new HttpClient();
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<T> SendHttpRequestAsync<T>(
            HttpMethod method,
            string url,
            string? body = null,
            string? jwtToken = null) where T : new()
        {
            var request = new HttpRequestMessage(method, url);
            
            if (!string.IsNullOrEmpty(jwtToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            }
            
            if (!string.IsNullOrEmpty(body))
            {
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }
            
            var response = await _httpClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, _options) ?? new T();
        }
    }
}
