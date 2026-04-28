using CLIENT.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CLIENT.Services
{
    public class ProductLineService
    {
        private readonly string prefixAPI;
        private readonly HttpClient client;

        public ProductLineService(string prefixApi)
        {
            this.prefixAPI = prefixApi;
            this.prefixAPI += "ProductLine/";
            client = new HttpClient();
        }

        public async Task GetAllAsync()
        {
            var response = await client.GetAsync($"{prefixAPI}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var productLines = JsonSerializer.Deserialize<List<ProductLine>>(json);
                foreach (var e in productLines)
                {
                    Console.WriteLine(e.ToString());
                }
            }

        }
        public async Task GetByIdAsync(string id)
        {
            var response = await client.GetAsync($"{prefixAPI}{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var productLine = JsonSerializer.Deserialize<ProductLine>(json);
                Console.WriteLine(productLine?.ToString());
            }
            else Console.WriteLine($"Línia de producte {id} no trobada.");
        }

        public async Task GetByDescriptionAsync(string subText)
        {
            
            var response = await client.GetAsync($"{prefixAPI}substring?subTextDescription={subText}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var productLines = JsonSerializer.Deserialize<List<ProductLine>>(json);
                foreach (var e in productLines)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else Console.WriteLine($"No s'ha trobat cap línia amb el text: {subText}");
        }

        public async Task AddAsync(ProductLine productLine)
        {
            var json = JsonSerializer.Serialize(productLine);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{prefixAPI}", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Línia de producte creada correctament.");
            }
            else Console.WriteLine("Error al crear la línia de producte.");
        }

        public async Task UpdateAsync(ProductLine productLine)
        {
            var json = JsonSerializer.Serialize(productLine);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{prefixAPI}", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Línia de producte actualizada correctament.");
            }
            else Console.WriteLine($"Error al actualizar: {response.StatusCode}");
        }

        public async Task DeleteAsync(string id)
        {
            var response = await client.DeleteAsync($"{prefixAPI}{id}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Línia de producte {id} eliminada.");
            }
            else Console.WriteLine($"No s'ha pogut eliminar la línia {id}.");
        }
    }
}
