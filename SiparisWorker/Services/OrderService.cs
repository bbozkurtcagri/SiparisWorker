using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisWorker.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenService _tokenService;

        public OrderService(HttpClient httpClient, TokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task GetOrderListAsync()
        {
            var token = await _tokenService.GetAccessTokenAsync();

            var request = new HttpRequestMessage(HttpMethod.Get, "https://jsonplaceholder.typicode.com/posts");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("📦 Sipariş listesi (örnek veri):\n" + json.Substring(0, 300) + "...");
        }
    }
}
