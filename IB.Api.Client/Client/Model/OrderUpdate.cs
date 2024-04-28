using Newtonsoft.Json;

namespace IB.Api.Client.Model
{
    public class OrderUpdate
    {
        [JsonProperty("orderId")]
        public int OrderId { get; set; }

        [JsonProperty("status")]
        public string Status { get; internal set; }

        [JsonProperty("filledAmount")]
        public decimal FilledAmount { get; internal set; }

        [JsonProperty("remainingAmount")]
        public decimal RemainingAmount { get; internal set; }

        [JsonProperty("avgFillPrice")]
        public double AvgFillPrice { get; internal set; }

        [JsonProperty("permId")]
        public int PermId { get; internal set; }

        [JsonProperty("parentId")]
        public int ParentId { get; internal set; }

        [JsonProperty("lastFillPrice")]
        public double LastFillPrice { get; internal set; }

        [JsonProperty("clientId")]
        public int ClientId { get; internal set; }

        [JsonProperty("whyHeld")]
        public string WhyHeld { get; internal set; }

        [JsonProperty("mktCapPrice")]
        public double MktCapPrice { get; internal set; }

        [JsonProperty("action")]
        public string Action { get; set; }
    }
}
