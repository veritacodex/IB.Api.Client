using System;
using System.Collections.Generic;
using System.Linq;
using IB.Api.Client.Model;
namespace IB.Api.Client.Helper
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
        public static void CalculatePositionPnl(PortfolioUpdate portfolioUpdate, double currentPrice, string multiplier, double comissionPerSide)
        {
            var priceDifference = currentPrice - portfolioUpdate.MarketPrice;
            var pnl = Convert.ToDecimal(priceDifference) * Convert.ToDecimal(multiplier) * portfolioUpdate.Position;
            var commission = Convert.ToDecimal(comissionPerSide * 3);
            if (portfolioUpdate.Action == nameof(TradeAction.BUY))
                portfolioUpdate.UnrealizedPnlCalculated = Math.Round(pnl - commission, 2);
            else
                portfolioUpdate.UnrealizedPnlCalculated = Math.Round((pnl * -1.0M) - commission, 2);

        }
        public static decimal CalculateTradePnl(TrailingTrade trade, double currentPrice)
        {
            var priceDifference = currentPrice - trade.ParentTrade.FillPrice;
            var pnl = Convert.ToDecimal(priceDifference) * Decimal.Parse(trade.ParentTrade.Multiplier) * trade.ParentTrade.Quantity;
            var commission = Convert.ToDecimal(trade.ParentTrade.Commission * 3);
            if (trade.ParentTrade.TradeAction == nameof(TradeAction.BUY))
                return Math.Round(pnl - commission, 2);
            else
                return Math.Round((pnl * -1.0M) - commission, 2);
        }
        public static void CalculateTradesPnl(List<TrailingTrade> trades, double price)
        {
            if (trades.Count > 0)
            {
                trades.ForEach(trade =>
                {
                    if (trade.ParentTrade.Status == OrderStatus.FILLED)
                        trade.ParentTrade.Pnl = CalculateTradePnl(trade, price);
                    else trade.ParentTrade.Pnl = 0;
                });
            }
        }
    }
}
