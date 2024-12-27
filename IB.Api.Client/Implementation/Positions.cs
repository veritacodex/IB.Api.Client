using System;
using IB.Api.Client.Implementation.Model;
using IBApi;

namespace IB.Api.Client.Implementation
{
    //Positions
    public partial class IBClient
    {
        public event EventHandler<PortfolioUpdate> PortfolioUpdateReceived;

        void EWrapper.updatePortfolio(Contract contract, decimal position, double marketPrice, double marketValue,
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
