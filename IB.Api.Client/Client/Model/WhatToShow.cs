namespace IB.Api.Client.Model
{
    /// <summary>
    /// Ref: https://interactivebrokers.github.io/tws-api/historical_bars.html#hd_what_to_show
    /// </summary>
    public enum WhatToShow
    {
        TRADES,
        MIDPOINT,
        BID,
        ASK,
        BID_ASK,
        ADJUSTED_LAST,
        HISTORICAL_VOLATILITY,
        OPTION_IMPLIED_VOLATILITY,
        REBATE_RATE,
        FEE_RATE,
        YIELD_BID,
        YIELD_ASK,
        YIELD_BID_ASK,
        YIELD_LAST,
        SCHEDULE
    }
}
