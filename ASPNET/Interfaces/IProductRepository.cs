using ASPNET.Model;

namespace ASPNET.Interfaces
{
    public interface IProductRepository
    {
        #region /*HTTPGET*/
        /*
         * Retorna totes les PRODUCTS en format List<Product> de la
         * productLine amb clau primària = productLineId . Si no n’hi ha cap, retorna una llista no nul·la
         * buida → Retorna totes les PRODUCTS en format List<Product> de la
         * productLine amb clau primària = productLineId . Si no n’hi ha cap,
         * retorna una llista no nul·la buida
         */
        public List<Product> GetAll(string productLineId);


        /*
         * Retorna el product que tingui com a clau
         * primària = productCode. Retorna null si no el troba
         */
        public Product GetOne(string productCode);


        /*
         * retorna el buyPrice del producte.
         */
        public decimal GetPrice(string productCode);
        
        
        /*
         * retorna una List<int> amb tots els orderNumber de
         *  les comandes on apareix el productCode
         */
        public List<int> GetOrders(string productCode);
        #endregion

        #region /*HTTPPOST*/
        /*
         * A partir d’una instància de Product passada com a paràmetre, es donarà
         * el Product, el qual conté també la clau forana de la seva productLine. Si la clau forana no
         * existeix a la taula PRODUCTLINES, es donarà d’alta també la nova productLine. En aquest
         * cas el TEXTDESCRIPTION de la producline serà el mateix valor que la clau forana, i la resta
         * de camps de la productline seran null
         */
        public void Add(Product product);
        #endregion

        #region /*HTTPPUT*/
        /* Si existeix el productCode,,
         * actualitzarem sumarem l’increment donat a l’estoc actual ( camp quantityInStock) . Si no
         *  existeix el producte, no farà res,
         */
        public void UpdateStock(string productCode, int increment);

        /*
         * Si existeix el productCode,modificarem el buyPrice amb el valor newPrice. 
         * Si no existeix el producte, no farà res,
         */
        public void UpdatePrice(string productCode, decimal newPrice);
        #endregion


    }
}
