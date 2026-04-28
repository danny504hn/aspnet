using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CLIENT.Model
{
    public class ProductLine
    {
        [JsonPropertyName("productLineId")]
        public string ProductLineId { get; set; }
        [JsonPropertyName("textDescription")]
        public string TextDescription { get; set; }

        public override string ToString()
        {
            int maxChars = 30;
            string descripcioCurta = "";
            if(TextDescription.Length > maxChars)
            {
                descripcioCurta = TextDescription.Substring(0,maxChars) + "...";
            }
            else { descripcioCurta = TextDescription; }

            return @$"DADES DE LA PRODUCTLINE: {ProductLineId}"+ "\n" +
                $"--------------------------------------------\n" +
                $"Descripcio: {descripcioCurta}\n" +
                $"--------------------------------------------\n";
        }
    }
}
