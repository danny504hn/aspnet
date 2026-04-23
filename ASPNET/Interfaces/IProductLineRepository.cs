using ASPNET.Model;
using Mysqlx.Crud;

namespace ASPNET.Interfaces
{
    public interface IProductLineRepository
    {

        /*
         * Retorna totes les PRODUCTLINES en format List<ProductLine> .
         * Si no n’hi ha cap, retorna una llista no nul·la buida.
        */
        public List<ProductLine> GetAll();

        /* Retorna la producline que tingui com a clau primària = id.
         * Retorna null si no la troba
        */

        public ProductLine GetOne(string id);

        /* Retorna totes les PRODUCTLINES en format List<ProductLine> tals que
         * subTextDescription sigui una subcadena del camp textDescription . Per exemple
         * si volem si subTextDescription = “Car” retornaria el ProductLine dels Vintage Cars
         * i dels Classic Cars.
         */

        public List<ProductLine> GetFromDescription(string subTextDescription);

        /*
         * A partir d’una instància de ProductLine passada com a paràmetre,
         * es donarà la ProductLine a la taula PRODUCTLINES
        */

        public void Add(ProductLine productLine);

        /* 
         * Si existeix la productLine, actualitzarem tots els seus camps
         * (excepte la clau primària), La clau primària ha d’existir per poder fer l’update 
        */

        public void Update(ProductLine productLine);

        /* Si existeix la clau primària, elimina la productLine associada */
        public void Delete(string id);
    }
}
