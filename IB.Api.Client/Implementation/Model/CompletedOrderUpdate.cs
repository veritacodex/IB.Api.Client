using IB.Api.Client.IBKR;

namespace IB.Api.Client.Implementation.Model;

public class CompletedOrderUpdate
{
    public Contract Contract { get; internal set; }
    public Order Order { get; internal set; }
    public OrderState OrderState { get; internal set; }
}