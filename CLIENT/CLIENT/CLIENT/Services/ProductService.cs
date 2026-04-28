using CLIENT.Model;
using System.Net.Http.Json;
using System.Text.Json;

namespace CLIENT.Services
{
    public class ProductService
    {
        private readonly string prefixAPI;
        private readonly HttpClient client;

        public ProductService(string prefixApi)
        {
            this.prefixAPI = prefixApi;
            client = new HttpClient();
        }

        // GET: api/Product?productLineId=Motorcycles
        public async Task<List<Product>> GetAllAsync(string productLineId)
        {
            var response = await client.GetAsync($"{prefixAPI}Product?productLineId={productLineId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
                foreach (var p in products)
                    Console.WriteLine(p.ToString());
                return products;
            }

            Console.WriteLine($"No s'han trobat productes per a la línia: {productLineId}");
            return new List<Product>();
        }


        // GET: api/Product/{productCode}
        public async Task<Product?> GetOneAsync(string productCode)
        {
            var response = await client.GetAsync($"{prefixAPI}Product/{productCode}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var product = JsonSerializer.Deserialize<Product>(json);
                Console.WriteLine(product?.ToString());
                return product;
            }

            Console.WriteLine($"Producte {productCode} no trobat.");
            return null;
        }


        // GET: api/Product/{productCode}/preu
        public async Task<decimal> GetPriceAsync(string productCode)
        {
            var response = await client.GetAsync($"{prefixAPI}Product/{productCode}/preu");

            if (response.IsSuccessStatusCode)
            {
                var priceStr = await response.Content.ReadAsStringAsync();
                if (decimal.TryParse(priceStr, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal price))
                {
                    Console.WriteLine($"Preu del producte {productCode}: {price}€");
                    return price;
                }
            }

            Console.WriteLine($"No s'ha pogut obtenir el preu de {productCode}");
            return -1;
        }


        // GET: api/Product/{productCode}/comandes
        public async Task<List<int>> GetOrdersAsync(string productCode)
        {
            var response = await client.GetAsync($"{prefixAPI}Product/{productCode}/comandes");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var orders = JsonSerializer.Deserialize<List<int>>(json) ?? new List<int>();
                Console.WriteLine($"Comandes del producte {productCode}: {string.Join(", ", orders)}");
                return orders;
            }

            Console.WriteLine($"No s'han trobat comandes per al producte {productCode}");
            return new List<int>();
        }


        // POST: api/Product
        public async Task AddAsync(Product product)
        {

            string fullUrl = $"{prefixAPI}Product";
            Console.WriteLine($"[URL]: {fullUrl}");

            // 2. Ver el JSON exacto que se enviará
            string jsonEnviado = JsonSerializer.Serialize(product, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine($"[BODY]:\n{jsonEnviado}");

            var response = await client.PostAsJsonAsync(fullUrl, product);

            // 3. Ver qué respondió el servidor si hubo error
            if (!response.IsSuccessStatusCode)
            {
                string errorServer = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[ERROR SERVER]: {errorServer}");
            }
        }

        // PUT: api/Product/{productCode}?increment=10 (UpdateStock)
        public async Task UpdateStockAsync(string productCode, int increment)
        {
            // Segons el teu controlador [HttpPut("{produtCode}")]
            var response = await client.PutAsync($"{prefixAPI}Product/{productCode}/stock?increment={increment}", null);

            if (response.IsSuccessStatusCode) Console.WriteLine($"Stock de {productCode} actualitzat.");
            else Console.WriteLine($"Error al actualitzar stock de {productCode}");
        }

        // PUT: api/Product?productCode=S10_1670&increment=5 (UpdatePrice)
        public async Task UpdatePriceAsync(string productCode, decimal newPrice)
        {
            var response = await client.PutAsync(
                $"{prefixAPI}Product/{productCode}/price?newPrice={newPrice.ToString(System.Globalization.CultureInfo.InvariantCulture)}", null);

            if (response.IsSuccessStatusCode)
                Console.WriteLine($"Preu de {productCode} actualitzat.");
            else
                Console.WriteLine($"Error al actualitzar preu de {productCode}");
        }

    }
}