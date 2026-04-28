namespace ASPNET.Model
{
    public class Product
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        public ProductLine? ProductLine { get; set; }

        public string ProductScale { get; set; }

        public string ProductVendor { get; set; }

        public string ProductDescription { get; set; }

        public int QuantityInStock { get; set; }

        public decimal BuyPrice { get; set; }

        public decimal MSRP { get; set; }
    }
}
