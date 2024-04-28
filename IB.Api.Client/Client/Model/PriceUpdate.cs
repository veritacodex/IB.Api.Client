using Newtonsoft.Json;
using System;

namespace IB.Api.Client.Model
{
    public class PriceUpdate
    {
        [JsonProperty("tickerId")]
        public int TickerId { get; internal set; }
        
        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("timeString")]
        public string TimeString { get; set; }

        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("open")]
        public double Open { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("close")]
        public double Close { get; set; }

        [JsonProperty("bid")]
        public double Bid { get; set; }

        [JsonProperty("bidSize")]
        public decimal BidSize { get; set; }

        [JsonProperty("ask")]
        public double Ask { get; set; }

        [JsonProperty("askSize")]
        public decimal AskSize { get; set; }

        [JsonProperty("marketDataType")]
        public int MarketDataType { get; set; }

        [JsonProperty("minTick")]
        public double MinTick { get; set; }

        [JsonProperty("snapshotPermissions")]
        public int SnapshotPermissions { get; set; }

        [JsonProperty("bboExchange")]
        public string BboExchange { get; set; }

        [JsonProperty("volume")]
        public decimal Volume { get; set; }

        [JsonProperty("ema1")]
        public double Ema1 { get; set; }

        [JsonProperty("ema2")]
        public double Ema2 { get; set; }

        [JsonProperty("ema3")]
        public double Ema3 { get; set; }

        [JsonProperty("optionBid")]
        public double OptionBid { get; set; }

        [JsonProperty("optionAsk")]
        public double OptionAsk { get; set; }
    }
}