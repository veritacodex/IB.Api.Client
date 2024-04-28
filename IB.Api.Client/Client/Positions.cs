using System;
using IB.Api.Client.Model;
using IBApi;

namespace IB.Api.Client
{
    //Positions
    public partial class IBClient
    {
        public event EventHandler<PortfolioUpdate> PortfolioUpdateReceived;

        public void UpdatePortfolio(Contract contract, decimal position, double marketPrice, double marketValue,
            double averageCost, double unrealizedPNL, double realizedPNL, string accountName)
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
                UnrealizedPnl = unrealizedPNL,
                RealizedPnl = realizedPNL
            };
            PortfolioUpdateReceived?.Invoke(this, portfolioUpdate);
        }
    }
}
