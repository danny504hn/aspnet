using ASPNET.Services;

namespace ASPNET
{
    public class Application
    {
        public static async Task Main(string[] args)
        {
            const string PREFIX_API = "https://localhost:5000/api/";

            var service = new ProductLineService(PREFIX_API);
            await service.GeatAllAsync();


        }
    }
}
