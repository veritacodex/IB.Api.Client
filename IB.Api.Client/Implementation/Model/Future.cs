using IB.Api.Client.IBKR;

namespace IB.Api.Client.Implementation.Model
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
