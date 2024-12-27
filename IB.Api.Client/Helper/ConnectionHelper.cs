using System;
using System.Threading;
using IB.Api.Client.Implementation;
using IB.Api.Client.Implementation.Model;
using IBApi;

namespace IB.Api.Client.Helper
{
    public static class ConnectionHelper
    {
        public static void StartIbClient(IBClient ibClient, ConnectionDetails connectionDetails)
        {
            ibClient.ClientSocket.EConnect(connectionDetails.Host, connectionDetails.Port, connectionDetails.ClientId);
            var reader = new EReader(ibClient.ClientSocket, ibClient.Signal);
            reader.Start();
            new Thread(() =>
            {
                while (ibClient.ClientSocket.IsConnected())
                {
                    ibClient.Signal.waitForSignal();
                    reader.ProcessMsgs();
                }
            })
            { IsBackground = true }.Start();

            //Force the thread to sleep in order to get all notifications from the gateway before going ahead
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }

        public static void NotificationReceived(object sender, Notification notification)
        {
            ArgumentNullException.ThrowIfNull(sender);
            Console.WriteLine($"Type:{notification.NotificationType} Code:{notification.Code} Id:{notification.Id} Message: {notification.Message}");
        }
    }
}
