using MySql.Data.MySqlClient;
using System.Xml;
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

        public XmlDocument LoadDocument() {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);
            return doc;
        }

        public void SaveDocument(XmlDocument doc)
        {
            doc.Save(xmlFilePath);
        }
    }
}
