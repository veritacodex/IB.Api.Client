using IBApi;

namespace IB.Api.Client.Model;

public class MarketRule
{
    public int MarketRuleId { get; internal set; }
    public PriceIncrement[] PriceIncrements { get; internal set; }
}
