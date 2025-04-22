using System;
using IB.Api.Client.Helper;
using IB.Api.Client.IBKR;
using IB.Api.Client.Implementation.Model;
using IB.Api.Client.Implementation;

namespace IB.Api.Client.Examples
{
    public static class DataDownload
    {
        private static readonly Contract Contract = new()
        {
            Symbol = "EUR",
            SecType = "CASH",
            Currency = "GBP",
            Exchange = "IDEALPRO"
        };
        public static void RunBasicDownload(ConnectionDetails connectionDetails)
        {
            var ibClient = new IbClient();
            ibClient.NotificationReceived += ConnectionHelper.NotificationReceived;
            ibClient.HistoricalDataUpdateReceived += HistoricalDataUpdateEndReceived;
            ConnectionHelper.StartIbClient(ibClient, connectionDetails);

            ibClient.GetHistoricalData(1005, Contract, 1, DurationType.D, 5, BarSizeType.mins, WhatToShow.MIDPOINT, Rth.No, false);
        }
        public static void RunDownloadWithUpdates(ConnectionDetails connectionDetails)
        {
            var ibClient = new IbClient();
            ibClient.NotificationReceived += ConnectionHelper.NotificationReceived;
            ibClient.HistoricalDataUpdateReceived += HistoricalDataUpdateEndReceived;
            ibClient.BarUpdateReceived += BarUpdateReceived;
            ConnectionHelper.StartIbClient(ibClient, connectionDetails);
            
            ibClient.GetHistoricalData(1005, Contract, 1, DurationType.D, 5, BarSizeType.mins, WhatToShow.MIDPOINT, Rth.No, true);
        }       

        public static void RunGetTimeAndSales(ConnectionDetails connectionDetails)
        {
            var ibClient = new IbClient();
            ibClient.NotificationReceived += ConnectionHelper.NotificationReceived;
            ConnectionHelper.StartIbClient(ibClient, connectionDetails);
            ibClient.GetLatestHistoricalTimeAndSales(1005, Contract, WhatToShow.TRADES);
        }
        public static void RunGetNews(ConnectionDetails connectionDetails)
        {
            var ibClient = new IbClient();
            ibClient.NotificationReceived += ConnectionHelper.NotificationReceived;
            ConnectionHelper.StartIbClient(ibClient, connectionDetails);
        }
        private static void BarUpdateReceived(object sender, Bar barUpdate)
        {
            Console.WriteLine($"{DateTime.Now}: Open:{barUpdate.Open} High:{barUpdate.High} Close:{barUpdate.Close} Low:{barUpdate.Low}");
        }
        private static void HistoricalDataUpdateEndReceived(object sender, BarUpdate barUpdate)
        {
            Console.WriteLine($"{DateTime.Now}: Open:{barUpdate.Bar.Open} High:{barUpdate.Bar.High} Close:{barUpdate.Bar.Close} Low:{barUpdate.Bar.Low}");
        }
    }
}
