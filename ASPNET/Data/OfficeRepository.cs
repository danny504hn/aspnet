using ASPNET.Interfaces;
using ASPNET.Model;
using System.Xml;

namespace ASPNET.Data
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly DBConnectionFactory connectionFactory;
        public OfficeRepository(DBConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public List<Office> GetAll()
        {
            List<Office> offices = new List<Office>();
            var xml = GetContent();

            var taules = xml.SelectNodes("/database/table");

            foreach (XmlNode column in taules)
            {
                if (column != null)
                {
                    offices.Add(new Office
                    {
                        
                        OfficeCode = column.SelectSingleNode("column[@name='officeCode']")?.InnerText,
                        City = column.SelectSingleNode("column[@name='city']")?.InnerText,
                        Phone = column.SelectSingleNode("column[@name='phone']")?.InnerText,
                        AddresLineOne = column.SelectSingleNode("column[@name='addressLine1']")?.InnerText,
                        AddresLineTwo = column.SelectSingleNode("column[@name='addressLine2']")?.InnerText,
                        State = column.SelectSingleNode("column[@name='state']")?.InnerText,
                        Country = column.SelectSingleNode("column[@name='country']")?.InnerText,
                        PostalCode = column.SelectSingleNode("column[@name='postalCode']")?.InnerText,
                        Territory = column.SelectSingleNode("column[@name='territory']")?.InnerText
                    });
                }
            }
            return offices;
        }

        public Office GetOne(string officeCode)
        {
            Office target = null;
            var xml = GetContent();
            var taula = xml.SelectSingleNode($"/database/table[column[@name='officeCode']='{officeCode}']");
            if (taula != null) {
                
                target = new Office {
                            OfficeCode = taula.SelectSingleNode("column[@name='officeCode']")?.InnerText,
                            City = taula.SelectSingleNode("column[@name='city']")?.InnerText,
                            Phone = taula.SelectSingleNode("column[@name='phone']")?.InnerText,
                            AddresLineOne = taula.SelectSingleNode("column[@name='addressLine1']")?.InnerText,
                            AddresLineTwo = taula.SelectSingleNode("column[@name='addressLine2']")?.InnerText,
                            State = taula.SelectSingleNode("column[@name='state']")?.InnerText,
                            Country = taula.SelectSingleNode("column[@name='country']")?.InnerText,
                            PostalCode = taula.SelectSingleNode("column[@name='postalCode']")?.InnerText,
                            Territory = taula.SelectSingleNode("column[@name='territory']")?.InnerText
                        };
                 
            }
            return target;
        }

        public void UpdatePostalCode(string officeCode, string newPostalCode)
        {
            Office target = GetOne(officeCode);
            if(target != null)
            {

                var xml = GetContent();
                var taula = xml.SelectSingleNode($"/database/table[column[@name='officeCode']='{officeCode}']");

                if (taula != null)
                {
                    taula.SelectSingleNode("column[@name='postalCode']")?.InnerText = newPostalCode;
                    SaveXmlDocument(xml);
                    target.PostalCode = newPostalCode;
                    
                }
            }

        }


        private XmlDocument GetContent()
        {
            var conn = connectionFactory.LoadDocument();
            return conn;
        }

        private void SaveXmlDocument(XmlDocument document)
        {
            connectionFactory.SaveDocument(document);
        }

    }
}
