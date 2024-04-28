using System;
using Newtonsoft.Json;

namespace IB.Api.Client.Model
{
    public class TradingHours
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [JsonProperty("startToString")]
        public string StartToString
        {
            get
            {
                return $"{Start.ToShortDateString()} {Start.ToShortTimeString()} ";
            }
        }

        [JsonProperty("endToString")]
        public string EndToString
        {
            get
            {
                return $"{End.ToShortDateString()} {End.ToShortTimeString()} ";
            }
        }

        [JsonProperty("lastTradeDate")]
        public string LastTradeDate { get; internal set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}
