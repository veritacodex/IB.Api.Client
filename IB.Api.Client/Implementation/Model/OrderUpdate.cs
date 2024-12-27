using System.Text.Json.Serialization;

namespace IB.Api.Client.Implementation.Model
{
    public class OrderUpdate
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("filledAmount")]
        public decimal FilledAmount { get; set; }

        [JsonPropertyName("remainingAmount")]
        public decimal RemainingAmount { get; set; }

        [JsonPropertyName("avgFillPrice")]
        public double AvgFillPrice { get; set; }

        [JsonPropertyName("permId")]
        public int PermId { get; set; }

        [JsonPropertyName("parentId")]
        public int ParentId { get; set; }

        [JsonPropertyName("lastFillPrice")]
        public double LastFillPrice { get; set; }

        [JsonPropertyName("clientId")]
        public int ClientId { get; set; }

        [JsonPropertyName("whyHeld")]
        public string WhyHeld { get; set; }

        [JsonPropertyName("mktCapPrice")]
        public double MktCapPrice { get; set; }

        [JsonPropertyName("action")]
        public string Action { get; set; }
    }
}
