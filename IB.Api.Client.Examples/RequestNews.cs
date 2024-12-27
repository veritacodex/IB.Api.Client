using System;
using IB.Api.Client.Helper;
using IB.Api.Client.Implementation;
using IB.Api.Client.Implementation.Model;
using IBApi;

namespace IB.Api.Client.Examples
{
    public static class RequestNews
    {
        public static void RunGetNewsProviders(ConnectionDetails connectionDetails)
        {
            var ibClient = new IBClient();
            ibClient.NotificationReceived += new EventHandler<Notification>(ConnectionHelper.NotificationReceived);
            ibClient.NewsProvidersUpdateReceived += new EventHandler<NewsProvider[]>(NewsProvidersUpdateReceived);
            ConnectionHelper.StartIbClient(ibClient, connectionDetails);
            ibClient.GetNewsProviders();
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
