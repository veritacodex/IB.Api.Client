using Newtonsoft.Json;

namespace IB.Api.Client.Model
{
    public class OrderUpdate
    {
        [JsonProperty("orderId")]
        public int OrderId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("filledAmount")]
        public decimal FilledAmount { get; set; }

        [JsonProperty("remainingAmount")]
        public decimal RemainingAmount { get; set; }

        [JsonProperty("avgFillPrice")]
        public double AvgFillPrice { get; set; }

        [JsonProperty("permId")]
        public int PermId { get; set; }

        [JsonProperty("parentId")]
        public int ParentId { get; set; }

        [JsonProperty("lastFillPrice")]
        public double LastFillPrice { get; set; }

        [JsonProperty("clientId")]
        public int ClientId { get; set; }

        [JsonProperty("whyHeld")]
        public string WhyHeld { get; set; }

        [JsonProperty("mktCapPrice")]
        public double MktCapPrice { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }
    }
}
