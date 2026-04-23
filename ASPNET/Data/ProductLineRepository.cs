using ASPNET.Interfaces;
using ASPNET.Model;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace ASPNET.Data
{
    public class ProductLineRepository : IProductLineRepository
    {
        private readonly DBConnectionFactory connectionFactory;
        public ProductLineRepository(DBConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Add(ProductLine productLine)
        {
            using var conn = GetConn();

            var cmd = new MySqlCommand(
                @"INSERT INTO PRODUCTLINES (productLine, TextDescription)
                    VALUES (@pl,@td)", conn);

            cmd.Parameters.AddWithValue("@pl", productLine.ProductLineId);
            cmd.Parameters.AddWithValue("@td", productLine.TextDescription);
            cmd.ExecuteNonQuery();

        }

        public void Delete(string id)
        {
            using var conn = GetConn();

            var cmd = new MySqlCommand(@"DELETE FROM PRODUCTLINES WHERE PRODUCTLINE = @id",conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public List<ProductLine> GetAll()
        {
            var list = new List<ProductLine>();
            using var conn = GetConn();

            var cmd = new MySqlCommand("SELECT * FROM PRODUCTLINES",conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ProductLine
                {
                    ProductLineId = reader.GetString("productLine"),
                    TextDescription = reader.GetString("textDescription"),
                    
                });
            }
            return list;
        }

        public List<ProductLine> GetFromDescription(string subTextDescription)
        {
            var list = new List<ProductLine>();
            using var conn = GetConn();
            var cmd = new MySqlCommand("SELECT * FROM PRODUCTLINES WHERE TEXTDESCRIPTION LIKE @subTextDescription", conn);
            cmd.Parameters.AddWithValue("@subTextDescription","% " + subTextDescription + " %");
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ProductLine
                {
                    ProductLineId = reader.GetString("productLine"),
                    TextDescription = reader.GetString("textDescription")
                });
            }
            return list;

        }

        public ProductLine GetOne(string id)
        {
            ProductLine productline = null;
            using var conn = GetConn();
            using var cmd = new MySqlCommand("SELECT * FROM PRODUCTLINES WHERE productLine = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();

            if (reader.HasRows && reader.Read() )
            {
                productline = new ProductLine
                {
                    ProductLineId = reader.GetString("productLine"),
                    TextDescription = reader.GetString("textDescription")
                };
            }

            return productline;
        }

        public void Update(ProductLine productLine)
        {
            using var conn = GetConn();

            var cmd = new MySqlCommand(@"
                    UPDATE PRODUCTLINES    
                    SET TEXTDESCRIPTION = @newTextDescription
                    where productLine = @id
                    ",conn);
            cmd.Parameters.AddWithValue("@newTextDescription", productLine.TextDescription);
            cmd.Parameters.AddWithValue("@id", productLine.ProductLineId);
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
