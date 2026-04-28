using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CLIENT.Model
{
    public class Product
    {
        [JsonPropertyName("productCode")]
        public string ProductCode { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [JsonPropertyName("productLine")]
        public ProductLine ProductLine { get; set; }

        [JsonPropertyName("productScale")]
        public string ProductScale { get; set; }

        [JsonPropertyName("productVendor")]
        public string ProductVendor { get; set; }

        [JsonPropertyName("productDescription")]
        public string ProductDescription { get; set; }

        [JsonPropertyName("quantityInStock")]
        public int QuantityInStock { get; set; }

        [JsonPropertyName("buyPrice")]
        public decimal BuyPrice { get; set; }

        [JsonPropertyName("msrp")]
        public decimal MSRP { get; set; }

        public override string ToString()
        {
            return $"DADES DEL PRODUCTE: {ProductCode}\n" +
                   $"--------------------------------------------\n" +
                   $"Nom: {ProductName}\n" +
                   $"Línia: {ProductLine?.ProductLineId ?? "N/A"}\n" +
                   $"Escala: {ProductScale}\n" +
                   $"Venedor: {ProductVendor}\n" +
                   $"Descripció: {ProductDescription}\n" +
                   $"Quantitat en Stock: {QuantityInStock}\n" +
                   $"Preu de Compra: {BuyPrice:C2}\n" +
                   $"MSRP: {MSRP:C2}\n" +
                   $"--------------------------------------------\n";
        }
    }
}
