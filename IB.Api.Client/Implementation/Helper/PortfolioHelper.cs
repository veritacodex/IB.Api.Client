using System;
using System.Collections.Generic;
using System.Linq;
using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Implementation.Helper
{
    public static class PortfolioHelper
    {
        public static void CalculatePortfolioPnl(PortfolioUpdate portfolioUpdate, List<Trade> trades)
        {
            var pnl = trades.Where(x => x.TradeAction == portfolioUpdate.Action).Sum(x => x.Pnl);
            portfolioUpdate.UnrealizedPnlCalculated = Math.Round(pnl, 2);
        }
        public static void CalculatePortfolioPnl(PortfolioUpdate portfolioUpdate, List<TrailingTrade> trades)
        {
            var pnl = trades.Where(x => x.ParentTrade.TradeAction == portfolioUpdate.Action).Sum(x => x.ParentTrade.Pnl);
            portfolioUpdate.UnrealizedPnlCalculated = Math.Round(pnl, 2);
        }
        public static void CalculatePositionPnl(PortfolioUpdate portfolioUpdate, double currentPrice, string multiplier, double commissionPerSide)
        {
            var priceDifference = currentPrice - portfolioUpdate.MarketPrice;
            var pnl = Convert.ToDecimal(priceDifference) * Convert.ToDecimal(multiplier) * portfolioUpdate.Position;
            var commission = Convert.ToDecimal(commissionPerSide * 3);
            portfolioUpdate.UnrealizedPnlCalculated = portfolioUpdate.Action == nameof(TradeAction.BUY) ? Math.Round(pnl - commission, 2) : Math.Round((pnl * -1.0M) - commission, 2);

        }
        public static decimal CalculateTradePnl(TrailingTrade trade, double currentPrice)
        {
            var priceDifference = currentPrice - trade.ParentTrade.FillPrice;
            var pnl = Convert.ToDecimal(priceDifference) * decimal.Parse(trade.ParentTrade.Multiplier) * trade.ParentTrade.Quantity;
            var commission = Convert.ToDecimal(trade.ParentTrade.Commission * 3);
            return trade.ParentTrade.TradeAction == nameof(TradeAction.BUY) ? Math.Round(pnl - commission, 2) : Math.Round((pnl * -1.0M) - commission, 2);
        }
        public static void CalculateTradesPnl(List<TrailingTrade> trades, double price)
        {
            if (trades.Count > 0)
            {
                trades.ForEach(trade =>
                {
                    trade.ParentTrade.Pnl = trade.ParentTrade.Status == OrderStatus.FILLED ? CalculateTradePnl(trade, price) : 0;
                });
            }
        }
    }
}
