using ASPNET.Model;
using System.Text.Json;

namespace ASPNET.Services
{
    public class ProductLineService
    {
        private readonly string prefixAPI;
        private readonly HttpClient client;

        public ProductLineService(string prefixApi)
        {
            this.prefixAPI = prefixApi;
            client = new HttpClient();
        }

        public async Task GeatAllAsync()
        {
            var response = await client.GetAsync($"{prefixAPI}ProductLine");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var productLines = JsonSerializer.Deserialize<List<ProductLine>>(json);
                foreach(var e in productLines)
                {
                    Console.WriteLine(e.ToString());
                }
            }

        }
    }
}
