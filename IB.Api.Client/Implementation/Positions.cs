using System;
using IB.Api.Client.IBKR;
using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Implementation
{
    //Positions
    public partial class IbClient
    {
        public event EventHandler<PortfolioUpdate> PortfolioUpdateReceived;

        void IEWrapper.updatePortfolio(Contract contract, decimal position, double marketPrice, double marketValue,
            double averageCost, double unrealizedPnl, double realizedPnl, string accountName)
        {
            var portfolioUpdate = new PortfolioUpdate
            {
                MarketPrice = marketPrice,
                MarketValue = marketValue,
                AccountName = accountName,
                UpdatedOn = DateTime.Now,
                AverageCost = averageCost,
                Contract = contract,
                Position = position,
                UnrealizedPnl = unrealizedPnl,
                RealizedPnl = realizedPnl
            };
            PortfolioUpdateReceived?.Invoke(this, portfolioUpdate);
        }
    }
}
