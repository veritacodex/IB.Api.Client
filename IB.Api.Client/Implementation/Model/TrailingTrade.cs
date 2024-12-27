using IBApi;

namespace IB.Api.Client.Implementation.Model
{
    public class TrailingTrade
    {
        public Trade ParentTrade { get; set; }
        public Trade TrailingStop { get; set; }
        public Order TrailingStopOrder { get; set; }
    }
}
