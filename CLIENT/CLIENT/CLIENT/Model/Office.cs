using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CLIENT.Model
{
    public class Office
    {
        [JsonPropertyName("officeCode")]
        public string OfficeCode { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("addresLineOne")]
        public string AddresLineOne { get; set; }

        [JsonPropertyName("addresLineTwo")]
        public string? AddresLineTwo { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("territory")]
        public string Territory { get; set; }

        public override string ToString()
        {

            return $"DADES DE LA OFICINA: {OfficeCode}\n" +
           $"--------------------------------------------\n" +
           $"Ciutat: {City}\n" +
           $"Telefon: {Phone}\n" +
           $"Direccio 1: {AddresLineOne}\n" +
           $"Direccio 2: {AddresLineTwo ?? "N/A"}\n" +
           $"Estat: {State ?? "N/A"}\n" +
           $"Pais: {Country}\n" +
           $"Codi Postal: {PostalCode}\n" +
           $"Territori: {Territory}\n" +
           $"--------------------------------------------\n";
        }
    }
}
