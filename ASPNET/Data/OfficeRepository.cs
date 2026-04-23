using ASPNET.Interfaces;
using ASPNET.Model;

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
            return new List<Office>();
        }

        public Office GetOne(string officeCode)
        {
            return new Office();
        }

        public void UpdatePostalCode(string officeCode, string newPostalCode)
        {
            
        }

    }
}
