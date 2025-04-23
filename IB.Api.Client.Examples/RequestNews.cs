using System;
using IB.Api.Client.IBKR;
using IB.Api.Client.Implementation;
using IB.Api.Client.Implementation.Helper;
using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Examples
{
    public static class RequestNews
    {
        public static void RunGetNewsProviders(ConnectionDetails connectionDetails)
        {
            var ibClient = new IbClient();
            ibClient.NotificationReceived += ConnectionHelper.NotificationReceived;
            ibClient.NewsProvidersUpdateReceived += NewsProvidersUpdateReceived;
            ConnectionHelper.StartIbClient(ibClient, connectionDetails);
            ibClient.ReqNewsProviders();
        }
        private static void NewsProvidersUpdateReceived(object sender, NewsProvider[] newsProviders)
        {
            foreach (var newsProvider in newsProviders)
            {
                Console.WriteLine($"Code:{newsProvider.ProviderCode} Name:{newsProvider.ProviderName}");
            }
        }
    }
}
