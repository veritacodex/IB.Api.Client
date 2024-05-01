using System;
using IB.Api.Client.Model;
using IB.Api.Client.Helper;

namespace IB.Api.Client.Examples
{
    public static class BasicConnection
    {
        public static void Run(ConnectionDetails connectionDetails)
        {
            var ibClient = new IBClient();
            ibClient.NotificationReceived += new EventHandler<Notification>(ConnectionHelper.NotificationReceived);
            ConnectionHelper.StartIbClient(ibClient, connectionDetails);
        }
    }
}
