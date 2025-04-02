using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;

namespace SiparisWorker.Services
{
    public class TokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public TokenService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (_cache.TryGetValue("access_token", out string token))
            {
                return token; // Token hâlâ geçerli
            }

            var response = await _httpClient.PostAsJsonAsync("https://reqres.in/api/login", new
            {
                email = "eve.holt@reqres.in",
                password = "cityslicka"
            });
            response.EnsureSuccessStatusCode();

            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) // Manuel 1 saat
            };

            _cache.Set("access_token", tokenResponse.token, options);
            return tokenResponse.token;
        }
    }

    public class TokenResponse
    {
        public string token { get; set; }
        public string token_type { get; set; } // istek atılan mock data yerindede bu tarz response türleri olmadığından dolayı sadece token üzerinden set işlemi yaptım
        public int expires_in { get; set; }
        public string access_token { get; set; }
    }
}
