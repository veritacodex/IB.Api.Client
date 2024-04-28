using IBApi;

namespace IB.Api.Client.Model
{
    public class OpenOrderUpdate
    {
        public int OrderId { get; internal set; }
        public Contract Contract { get; internal set; }
        public Order Order { get; internal set; }
        public OrderState OrderState { get; internal set; }
    }
}
