using ASPNET.Model;

namespace ASPNET.Interfaces
{
    public interface IOfficeRepository
    {
        
        #region /*HTTPGET*/

        public List<Office> GetAll();

        public Office GetOne(string officeCode);
        #endregion

        #region/*HTTPPUT*/
        public void UpdatePostalCode(string officeCode, string newPostalCode);

        #endregion

    }
}
