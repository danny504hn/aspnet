using CLIENT.Model;
using System.Text;
using System.Text.Json;

namespace CLIENT.Services
{
    internal class OfficeService
    {
        private readonly string prefixAPI;
        private readonly HttpClient client;

        public OfficeService(string prefixApi)
        {
            this.prefixAPI = prefixApi;
            client = new HttpClient();
        }

        public async Task GetAllAsync()
        {
            var response = await client.GetAsync($"{prefixAPI}Office");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var offices = JsonSerializer.Deserialize<List<Office>>(json);
                foreach (var e in offices)
                {
                    Console.WriteLine(e.ToString());
                }
            }

        }
        public async Task GetByIdAsync(string officeCode)
        {
            var response = await client.GetAsync($"{prefixAPI}Office/{officeCode}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var office = JsonSerializer.Deserialize<Office>(json);

                if (office != null)
                {
                    Console.WriteLine(office.ToString());
                }
            }
            else
            {
                Console.WriteLine($"Oficina amb codi {officeCode} no trobada.");
            }
        }

        public async Task UpdatePostalCodeAsync(string officeCode, string newPostalCode)
        {
           
            var url = $"{prefixAPI}Office?officeCode={officeCode}&newPostalCode={newPostalCode}";

           
            var response = await client.PutAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Codi Postal actualitzat.");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }
}
