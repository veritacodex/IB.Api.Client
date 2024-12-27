using System;
using IB.Api.Client.Helper;
using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Examples
{
    public static class BasicConnection
    {
        public static void Run(ConnectionDetails connectionDetails)
        {
            var ibClient = new IBClient();
            ibClient.NotificationReceived += new EventHandler<Notification>(ConnectionHelper.NotificationReceived);
            ibClient.AccountUpdateReceived += new EventHandler<AccountUpdate>(AccountUpdateReceived);
            ibClient.PortfolioUpdateReceived += new EventHandler<PortfolioUpdate>(PortfolioUpdateReceived);
            
            ConnectionHelper.StartIbClient(ibClient, connectionDetails);

            if (ibClient.IsConnected())
            {
                foreach (var accountId in ibClient.AccountIds)
                {
                    ibClient.SubscribeToAccountUpdates(accountId);
                }
            }            
            Common.KeepConsoleAlive();
        }

        private static void PortfolioUpdateReceived(object sender, PortfolioUpdate portfolioUpdate)
        {
            Console.WriteLine(portfolioUpdate.GetAsTable());
        }

        private static void AccountUpdateReceived(object sender, AccountUpdate accountUpdate)
        {
            Console.WriteLine(accountUpdate.GetAsTable());
        }
    }
}
