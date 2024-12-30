using System;
using System.Text.Json.Serialization;
using IB.Api.Client.Helper;
using IB.Api.Client.IBKR;

namespace IB.Api.Client.Implementation.Model
{
    public class PortfolioUpdate
    {
        [JsonPropertyName("updatedOn")]
        public DateTime UpdatedOn { get; set; }

        [JsonPropertyName("updated")]
        public string Updated
        {
            get
            {
                return $"{UpdatedOn.ToShortDateString()} {UpdatedOn.ToShortTimeString()} ";
            }
        }

        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("contract")]
        public Contract Contract { get; set; }

        [JsonPropertyName("position")]
        public decimal Position { get; set; }

        [JsonPropertyName("unrealizedPnl")]
        public double UnrealizedPnl { get; set; }

        [JsonPropertyName("unrealizedPnlCalculated")]
        public decimal UnrealizedPnlCalculated { get; set; }

        [JsonPropertyName("realizedPnl")]
        public double RealizedPnl { get; internal set; }

        [JsonPropertyName("accountName")]
        public string AccountName { get; internal set; }

        [JsonPropertyName("marketPrice")]
        public double MarketPrice { get; internal set; }

        [JsonPropertyName("marketValue")]
        public double MarketValue { get; internal set; }

        [JsonPropertyName("averageCost")]
        public double AverageCost { get; internal set; }

        public string GetAsTable()
        {
            var table = new Table("Positions", "Symbol", "Size", "MarketPrice", "MarketValue", "Cost/Premium", "Strike");
            table.AddRow(UpdatedOn, Contract.Symbol, Position, MarketPrice, MarketValue, AverageCost, Contract.Strike);
            return table.ToString();
        }
    }
}
