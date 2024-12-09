using System;
using System.Text.Json.Serialization;

namespace IB.Api.Client.Model
{
    public class TradingHours
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [JsonPropertyName("startToString")]
        public string StartToString
        {
            get
            {
                return $"{Start.ToShortDateString()} {Start.ToShortTimeString()} ";
            }
        }

        [JsonPropertyName("endToString")]
        public string EndToString
        {
            get
            {
                return $"{End.ToShortDateString()} {End.ToShortTimeString()} ";
            }
        }

        [JsonPropertyName("lastTradeDate")]
        public string LastTradeDate { get; internal set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
    }
}
