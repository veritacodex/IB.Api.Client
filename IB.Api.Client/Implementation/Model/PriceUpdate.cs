using System;
using System.Text.Json.Serialization;

namespace IB.Api.Client.Implementation.Model
{
    public class PriceUpdate
    {
        [JsonPropertyName("tickerId")]
        public int TickerId { get; internal set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }

        [JsonPropertyName("timeString")]
        public string TimeString { get; set; }

        [JsonPropertyName("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonPropertyName("open")]
        public double Open { get; set; }

        [JsonPropertyName("high")]
        public double High { get; set; }

        [JsonPropertyName("low")]
        public double Low { get; set; }

        [JsonPropertyName("close")]
        public double Close { get; set; }

        [JsonPropertyName("bid")]
        public double Bid { get; set; }

        [JsonPropertyName("bidSize")]
        public decimal BidSize { get; set; }

        [JsonPropertyName("ask")]
        public double Ask { get; set; }

        [JsonPropertyName("askSize")]
        public decimal AskSize { get; set; }

        [JsonPropertyName("marketDataType")]
        public int MarketDataType { get; set; }

        [JsonPropertyName("minTick")]
        public double MinTick { get; set; }

        [JsonPropertyName("snapshotPermissions")]
        public int SnapshotPermissions { get; set; }

        [JsonPropertyName("bboExchange")]
        public string BboExchange { get; set; }

        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }

        [JsonPropertyName("optionBid")]
        public double OptionBid { get; set; }

        [JsonPropertyName("optionAsk")]
        public double OptionAsk { get; set; }

        [JsonPropertyName("gamma")]
        public double Gamma { get; set; }

        [JsonPropertyName("delta")]
        public double Delta { get; internal set; }

        [JsonPropertyName("vega")]
        public double Vega { get; internal set; }

        [JsonPropertyName("tetha")]
        public double Theta { get; internal set; }

        [JsonPropertyName("tickAttrib")]
        public double TickAttrib { get; internal set; }

        [JsonPropertyName("impliedVolatility")]
        public double ImpliedVolatility { get; internal set; }

        [JsonPropertyName("pvDividend")]
        public double PvDividend { get; internal set; }
    }
}