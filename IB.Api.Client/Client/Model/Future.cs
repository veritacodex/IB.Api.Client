using IBApi;

namespace IB.Api.Client.Model
{
    public class Future : Contract
    {
        public int ReqTickerId { get; set; }
        public Future(int reqTickerId, string symbol, string localSymbol, string lastTradeDateOrContractMonth, string exchange, string multiplier, string currency)
        {
            ReqTickerId = reqTickerId;
            Symbol = symbol;
            LocalSymbol = localSymbol;
            LastTradeDateOrContractMonth = lastTradeDateOrContractMonth;
            Exchange = exchange;
            Multiplier = multiplier;
            Currency = currency;
            SecType = "FUT";
        }
    }
}
