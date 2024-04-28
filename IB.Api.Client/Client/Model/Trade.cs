using System;
using Newtonsoft.Json;

namespace IB.Api.Client.Model
{
    public class Trade
    {
        [JsonProperty("orderId")]
        public int? OrderId { get; set; }

        [JsonProperty("orderRef")]
        public string OrderRef { get; set; }

        [JsonProperty("executionId")]
        public string ExecutionId { get; set; }

        [JsonProperty("at")]
        public DateTime? At { get; set; }

        [JsonProperty("atControl")]
        public DateTime? AtControl { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("localSymbol")]
        public string LocalSymbol { get; set; }

        [JsonProperty("tradeAction")]
        public string TradeAction { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }        

        [JsonProperty("orderType")]
        public string OrderType { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("multiplier")]
        public string Multiplier { get; set; }

        [JsonProperty("commission")]
        public double Commission { get; set; }

        [JsonProperty("pnl")]
        public decimal Pnl { get; set; }        

        [JsonProperty("limitPrice")]
        public double LimitPrice { get; set; }

        [JsonProperty("stopPrice")]
        public double StopPrice { get; set; }

        [JsonProperty("fillPrice")]
        public double FillPrice { get; set; }

        [JsonProperty("avgPrice")]
        public decimal AvgPrice { get; set; }

        [JsonProperty("drawdown")]
        public decimal Drawdown { get; set; }

        [JsonProperty("lastPrice")]
        public decimal LastPrice { get; set; }

        [JsonProperty("initialStop")]
        public double InitialStop { get; set; }
    }
}
