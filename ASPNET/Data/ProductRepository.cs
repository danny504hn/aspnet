using ASPNET.Interfaces;
using ASPNET.Model;
using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;

namespace ASPNET.Data
{
    public class ProductRepository : IProductRepository
    {

        private readonly DBConnectionFactory connectionFactory;
        private readonly ProductLineRepository pLRepository;

        public ProductRepository(DBConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
            this.pLRepository = new ProductLineRepository(connectionFactory);
        }
        public void Add(Product product)
        {
            if (pLRepository.GetOne(product.ProductLine.ProductLineId) == null) {
                ProductLine newProductLine = new ProductLine
                {
                    ProductLineId = product.ProductLine.ProductLineId,
                    TextDescription = product.ProductLine.ProductLineId
                };
                pLRepository.Add(newProductLine);
            }

            using var conn = GetConn();
            using var cmd = new MySqlCommand(@"INSERT INTO products 
                                            (ProductCode, ProductName, ProductLine, ProductScale, ProductVendor, 
                                            ProductDescription, QuantityInStock, BuyPrice, MSRP) 
                                            VALUES 
                                            (@code, @name, @line, @scale, @vendor, @description, @stock, @price, @msrp)",conn);
            
            cmd.Parameters.AddWithValue("@code", product.ProductCode);
            cmd.Parameters.AddWithValue("@name", product.ProductName);

         
            cmd.Parameters.AddWithValue("@line", product.ProductLine.ProductLineId );

            cmd.Parameters.AddWithValue("@scale", product.ProductScale);
            cmd.Parameters.AddWithValue("@vendor", product.ProductVendor);
            cmd.Parameters.AddWithValue("@description", product.ProductDescription);
            cmd.Parameters.AddWithValue("@stock", product.QuantityInStock);
            cmd.Parameters.AddWithValue("@price", product.BuyPrice);
            cmd.Parameters.AddWithValue("@msrp", product.MSRP);

            cmd.ExecuteNonQuery();
        }

        public List<Product> GetAll(string productLineId)
        {
         using var conn = GetConn();
            var list = new List<Product>();
            using var cmd = new MySqlCommand(@"SELECT p.*, pl.textDescription FROM products p 
                                               INNER JOIN productlines pl 
                                               ON p.productLine = pl.productLine
                                               WHERE p.productLine = @productLineId",conn);
            cmd.Parameters.AddWithValue("@productLineId", productLineId);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                list.Add(
                    new Product
                    {
                        ProductCode = reader.GetString("productCode"),
                        ProductName = reader.GetString("productName"),
                        ProductLine = new ProductLine
                        {
                            ProductLineId = reader.GetString("productLine"),
                            TextDescription = reader.GetString("textDescription")
                        },
                        ProductScale = reader.GetString("productScale"),
                        ProductVendor = reader.GetString("productVendor"),
                        ProductDescription = reader.GetString("productDescription"),
                        QuantityInStock = reader.GetInt32("quantityInStock"),
                        BuyPrice = reader.GetDecimal("buyPrice"),
                        MSRP = reader.GetDecimal("MSRP")
                    });
            }
            return list;
        }

        public Product GetOne(string productCode)
        {
            Product product = null;
            using var conn = GetConn();
            using var cmd = new MySqlCommand(@"SELECT p.*, pl.textDescription FROM products p 
                                               INNER JOIN productlines pl 
                                               ON p.productLine = pl.productLine
                                               WHERE p.productCode = @productCode", conn);
            cmd.Parameters.AddWithValue("@productCode", productCode);

            var reader = cmd.ExecuteReader();
            if (reader.HasRows && reader.Read()) {
                product = new Product
                {
                    ProductCode = reader.GetString("productCode"),
                    ProductName = reader.GetString("productName"),
                    ProductLine = new ProductLine
                    {
                        ProductLineId = reader.GetString("productLine"),
                        TextDescription = reader.GetString("textDescription")
                    },
                    ProductScale = reader.GetString("productScale"),
                    ProductVendor = reader.GetString("productVendor"),
                    ProductDescription = reader.GetString("productDescription"),
                    QuantityInStock = reader.GetInt32("quantityInStock"),
                    BuyPrice = reader.GetDecimal("buyPrice"),
                    MSRP = reader.GetDecimal("MSRP")
                };
            }
            return product;
        }

        public List<int> GetOrders(string productCode)
        {
            List<int> orders = new List<int>();
            using var conn = GetConn();
            using var cmd = new MySqlCommand(@"SELECT orderNumber FROM orderDetails 
                                              WHERE productCode = @productCode", conn);
            cmd.Parameters.AddWithValue("@productCode", productCode);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                orders.Add(reader.GetInt32("orderNumber"));
            }
            return orders;
        }

        public decimal GetPrice(string productCode)
        {
            decimal price = -1;
            using var conn = GetConn();
            using var cmd = new MySqlCommand(@"SELECT p.*, pl.textDescription FROM products p 
                                               INNER JOIN productlines pl 
                                               ON p.productLine = pl.productLine
                                               WHERE p.productCode = @productCode", conn);
            cmd.Parameters.AddWithValue("@productCode", productCode);
            var reader = cmd.ExecuteReader();

            if (reader.HasRows && reader.Read())
            {
                price = reader.GetDecimal("buyPrice");
            }
            return price;

        }

        public void UpdatePrice(string productCode, decimal newPrice)
        {
            using var conn = GetConn();

            using var cmd = new MySqlCommand(@"UPDATE produtcs
                                               SET buyPrice = @newPrice
                                               WHERE productCode = @productCode", conn);

            if (newPrice <= 0) throw new Exception();

            cmd.Parameters.AddWithValue("@increment", newPrice);
            cmd.Parameters.AddWithValue("@productCode", productCode);
            cmd.ExecuteNonQuery();
        }

        public void UpdateStock(string productCode, int increment)
        {
            using var conn = GetConn();

            using var cmd = new MySqlCommand(@"UPDATE produtcs
                                               SET quantityinStock = qunatityinStock + @increment
                                               WHERE productCode = @productCode",conn);
            
            if (increment <= 0) throw new Exception();

            cmd.Parameters.AddWithValue("@increment", increment);
            cmd.Parameters.AddWithValue("@productCode", productCode);
            cmd.ExecuteNonQuery();
        }

        private MySqlConnection GetConn()
        {
            var conn = connectionFactory.CreateConnection();
            conn.Open();
            return conn;
        }
    }
}
