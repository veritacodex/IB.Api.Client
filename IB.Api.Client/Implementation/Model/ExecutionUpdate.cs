namespace IB.Api.Client.Implementation.Model
{
    public class ExecutionUpdate
    {
        public int ReqId { get; internal set; }
        public string Symbol { get; internal set; }
        public string SecType { get; internal set; }
        public string ExecutionId { get; internal set; }
        public string OrderRef { get; internal set; }
        public string Side { get; internal set; }
        public string Action
        {
            get
            {
                return Side == "BOT" ? "BUY" : "SELL";
            }
        }

        public string Account { get; internal set; }
        public double AvgPrice { get; internal set; }
    }
}
