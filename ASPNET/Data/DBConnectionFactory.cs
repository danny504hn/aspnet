using MySql.Data.MySqlClient;
namespace ASPNET.Data
{
    public class DBConnectionFactory
    {
        private readonly string connectionString;
        private readonly string xmlFilePath;

        public DBConnectionFactory(string connectionString,string xmlFilePath) {
        
            this.connectionString = connectionString;
            this.xmlFilePath = xmlFilePath;
           
        }

        public MySqlConnection CreateConnection() {
            return new MySqlConnection(connectionString);
        }

        public XmlDocument LoadDocument(string xmlFilePath) {
            XmlDocument doc = new XmlDocument();
            return doc.LoadXml(xmlFilePath);

        }
    }
}
