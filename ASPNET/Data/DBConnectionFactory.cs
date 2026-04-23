using MySql.Data.MySqlClient;
namespace ASPNET.Data
{
    public class DBConnectionFactory
    {
        private readonly string connectionString;
        private readonly string xmlFilePath;

        public DBConnectionFactory(string connectionString) {
        
            this.connectionString = connectionString;
           
        }

        public MySqlConnection CreateConnection() {
            return new MySqlConnection(connectionString);
        }
    }
}
