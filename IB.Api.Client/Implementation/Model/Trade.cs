using System;
using System.Text.Json.Serialization;

namespace IB.Api.Client.Implementation.Model
{
    public class Trade
    {
        [JsonPropertyName("orderId")]
        public int? OrderId { get; set; }

        [JsonPropertyName("orderRef")]
        public string OrderRef { get; set; }

        [JsonPropertyName("executionId")]
        public string ExecutionId { get; set; }

        [JsonPropertyName("at")]
        public DateTime? At { get; set; }

        [JsonPropertyName("atControl")]
        public DateTime? AtControl { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("localSymbol")]
        public string LocalSymbol { get; set; }

        [JsonPropertyName("tradeAction")]
        public string TradeAction { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("orderType")]
        public string OrderType { get; set; }

        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }

        [JsonPropertyName("multiplier")]
        public string Multiplier { get; set; }

        [JsonPropertyName("commission")]
        public double Commission { get; set; }

        [JsonPropertyName("pnl")]
        public decimal Pnl { get; set; }

        [JsonPropertyName("limitPrice")]
        public double LimitPrice { get; set; }

        [JsonPropertyName("stopPrice")]
        public double StopPrice { get; set; }

        [JsonPropertyName("fillPrice")]
        public double FillPrice { get; set; }

        [JsonPropertyName("avgPrice")]
        public decimal AvgPrice { get; set; }

        [JsonPropertyName("drawdown")]
        public decimal Drawdown { get; set; }

        [JsonPropertyName("lastPrice")]
        public decimal LastPrice { get; set; }

        [JsonPropertyName("initialStop")]
        public double InitialStop { get; set; }
    }
}
