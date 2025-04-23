using System;
using IB.Api.Client.Implementation;
using IB.Api.Client.Implementation.Helper;
using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Examples
{
    public static class BasicConnection
    {
        public static void Run(ConnectionDetails connectionDetails)
        {
            var ibClient = new IbClient();
            ibClient.NotificationReceived += ConnectionHelper.NotificationReceived;
            ibClient.AccountUpdateReceived += AccountUpdateReceived;
            ibClient.PortfolioUpdateReceived += PortfolioUpdateReceived;
            
            ConnectionHelper.StartIbClient(ibClient, connectionDetails);

            if (ibClient.IsConnected())
            {
                foreach (var accountId in ibClient.AccountIds)
                {
                    ibClient.ReqAccountUpdates(accountId);
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
