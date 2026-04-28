using CLIENT.Model;
using CLIENT.Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        const string PREFIX_API = "https://localhost:5000/api/";

        #region PRODUCT LINE SERVICE
        var servicePL = new ProductLineService(PREFIX_API);

        // GET ALL inicial
        await servicePL.GetAllAsync();

        // POST (ADD)
        Console.WriteLine("Introdueix ID per a nova Product Line:");
        string newId = Console.ReadLine();
        Console.WriteLine("Introdueix descripcio per a nova Product Line:");
        string newDesc = Console.ReadLine();
        if (!string.IsNullOrEmpty(newId))
        {
            await servicePL.AddAsync(new ProductLine { ProductLineId = newId, TextDescription = newDesc });
            Console.WriteLine("DESPRES D'AFEGIR:");
            await servicePL.GetAllAsync();
        }

        // GET BY ID
        Console.WriteLine("Introdueix ID de Product Line a consultar:");
        string plId = Console.ReadLine();
        if (!string.IsNullOrEmpty(plId)) await servicePL.GetByIdAsync(plId);

        // GET BY SUBSTRING
        Console.WriteLine("Introdueix text per cercar en descripcio:");
        string subtext = Console.ReadLine();
        if (!string.IsNullOrEmpty(subtext)) await servicePL.GetByDescriptionAsync(subtext);

        // PUT (UPDATE)
        Console.WriteLine("Introdueix ID de Product Line a modificar:");
        string updateId = Console.ReadLine();
        Console.WriteLine("Introdueix la nova descripcio:");
        string updateDesc = Console.ReadLine();
        if (!string.IsNullOrEmpty(updateId))
        {
            Console.WriteLine("ABANS D'ACTUALITZAR:");
            await servicePL.GetByIdAsync(updateId);

            await servicePL.UpdateAsync(new ProductLine { ProductLineId = updateId, TextDescription = updateDesc });

            Console.WriteLine("DESPRES D'ACTUALITZAR:");
            await servicePL.GetByIdAsync(updateId);
        }

        // DELETE
        Console.WriteLine("Introdueix ID de Product Line a eliminar:");
        string deleteId = Console.ReadLine();
        if (!string.IsNullOrEmpty(deleteId))
        {
            Console.WriteLine("ABANS D'ELIMINAR:");
            await servicePL.GetByIdAsync(deleteId);

            await servicePL.DeleteAsync(deleteId);

            Console.WriteLine("DESPRES D'ELIMINAR (LLISTAT TOTAL):");
            await servicePL.GetAllAsync();
        }
        #endregion

        #region OFFICE SERVICE
        var serviceOffice = new OfficeService(PREFIX_API);

        // GET ALL
        await serviceOffice.GetAllAsync();

        // GET BY ID  
        Console.WriteLine("Introdueix el numero de oficina");
        string nOficina = Console.ReadLine();
        if (!string.IsNullOrEmpty(nOficina)) { await serviceOffice.GetByIdAsync(nOficina); }

        // UPDATE POSTAL CODE
        Console.WriteLine("Introdueix el numero de oficina a actualitzar");
        string nOficinaUpdate = Console.ReadLine();
        Console.WriteLine("Introdueix el nou codi postal");
        string newCP = Console.ReadLine();

        if (!string.IsNullOrEmpty(nOficinaUpdate) && !string.IsNullOrEmpty(newCP))
        {
            Console.WriteLine("ESTAT ANTERIOR:");
            await serviceOffice.GetByIdAsync(nOficinaUpdate);

            await serviceOffice.UpdatePostalCodeAsync(nOficinaUpdate, newCP);

            Console.WriteLine("ESTAT POSTERIOR:");
            await serviceOffice.GetByIdAsync(nOficinaUpdate);
        }
        #endregion

        #region PRODUCT SERVICE
        var serviceProd = new ProductService(PREFIX_API);

        // GET ALL (Requereix una línia, per exemple 'Motorcycles')
        Console.WriteLine("--- LLISTAT DE PRODUCTES (Motorcycles) ---");
        await serviceProd.GetAllAsync("Motorcycles");


        // GET ONE
        Console.WriteLine("Introdueix codi de producte a consultar (Ex: S10_1678):");
        string pCode = Console.ReadLine();
        if (!string.IsNullOrEmpty(pCode)) await serviceProd.GetOneAsync(pCode);


        // GET PRICE i ORDERS
        if (!string.IsNullOrEmpty(pCode))
        {
            await serviceProd.GetPriceAsync(pCode);
            await serviceProd.GetOrdersAsync(pCode);
        }



        // POST (ADD)
        var p = new Product();
        Console.Write("productCode: ");
        p.ProductCode = Console.ReadLine();
        Console.Write("productName: ");
        p.ProductName = Console.ReadLine();
        Console.Write("productLine ID: ");
        string lineInput = Console.ReadLine();
        Console.Write("productLine Description: ");
        string lineDesc = Console.ReadLine();
        p.ProductLine = new ProductLine { ProductLineId = lineInput, TextDescription = lineDesc };
        Console.Write("productScale: ");
        p.ProductScale = Console.ReadLine();
        Console.Write("productVendor: ");
        p.ProductVendor = Console.ReadLine();
        Console.Write("productDescription: ");
        p.ProductDescription = Console.ReadLine();
        Console.Write("quantityInStock: ");
        int.TryParse(Console.ReadLine(), out int stock);
        p.QuantityInStock = stock;
        Console.Write("buyPrice: ");
        decimal.TryParse(Console.ReadLine()?.Replace('.', ','), out decimal buyPrice);
        p.BuyPrice = buyPrice;
        Console.Write("msrp: ");
        decimal.TryParse(Console.ReadLine()?.Replace('.', ','), out decimal msrp);
        p.MSRP = msrp;
        await serviceProd.AddAsync(p);


        // PUT (UPDATE STOCK)
        Console.WriteLine("Introdueix codi de producte per actualitzar STOCK:");
        string codiStock = Console.ReadLine();
        if (!string.IsNullOrEmpty(codiStock))
        {
           
            Product? productStock = await serviceProd.GetOneAsync(codiStock);
            if (productStock != null)
            {
                Console.WriteLine("Introdueix l'increment d'estoc:");
                int.TryParse(Console.ReadLine(), out int increment);

                Console.WriteLine("STOCK ANTERIOR:");
                await serviceProd.GetOneAsync(codiStock);

                await serviceProd.UpdateStockAsync(codiStock, increment);

                Console.WriteLine("STOCK POSTERIOR:");
                await serviceProd.GetOneAsync(codiStock);
            }
        }

        // PUT (UPDATE PRICE)
        Console.WriteLine("Introdueix codi de producte per actualitzar PREU:");
        string codi = Console.ReadLine();
        if (!string.IsNullOrEmpty(codi))
        {

            Product? product = await serviceProd.GetOneAsync(codi);
            if (product != null)
            {
                Console.WriteLine("Introdueix el nou preu:");

                decimal.TryParse(Console.ReadLine()?.Replace('.', ','), out decimal preu);

                Console.WriteLine("PREU ANTERIOR:");
                await serviceProd.GetPriceAsync(codi);

                await serviceProd.UpdatePriceAsync(codi, preu);

                Console.WriteLine("PREU POSTERIOR:");
                await serviceProd.GetPriceAsync(codi);
            }
        }

        #endregion
    }
}