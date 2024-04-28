using System;
using System.Collections.Generic;
using IB.Api.Client.Model;
using IB.Api.Client.Helper;
using IBApi;

namespace IB.Api.Client.Examples
{
    public static class DataDownload
    {
        private static readonly Contract _contract = new Contract
        {
            Symbol = "EUR",
            SecType = "CASH",
            Currency = "GBP",
            Exchange = "IDEALPRO"
        };
        public static void RunBasicDownload(ConnectionDetails connectionDetails)
        {
            var ibClient = new IBClient();
            ibClient.NotificationReceived += new EventHandler<Notification>(Common.NotificationReceived);
            ibClient.HistoricalDataUpdateReceived += new EventHandler<BarUpdate>(HistoricalDataUpdateEndReceived);
            ConnectionHelper.StartIbClient(ibClient, connectionDetails);

            var duration = Duration.GetDuration(1, DurationType.D);
            var barSize = BarSize.GetBarSize(5, BarSizeType.min);
            ibClient.GetHistoricalData(1005, _contract, duration, barSize, WhatToShow.MIDPOINT, Rth.No, false);
        }
        public static void RunDownloadWithUpdates(ConnectionDetails connectionDetails)
        {
            var ibClient = new IBClient();
            ibClient.NotificationReceived += new EventHandler<Notification>(Common.NotificationReceived);
            ibClient.HistoricalDataUpdateReceived += new EventHandler<BarUpdate>(HistoricalDataUpdateEndReceived);
            ibClient.BarUpdateReceived += new EventHandler<RealTimeBarUpdate>(BarUpdateReceived);
            ConnectionHelper.StartIbClient(ibClient, connectionDetails);

            var duration = Duration.GetDuration(1, DurationType.D);
            var barSize = BarSize.GetBarSize(5, BarSizeType.min);
            ibClient.GetHistoricalData(1005, _contract, duration, barSize, WhatToShow.MIDPOINT, Rth.No, true);
        }       

        public static void RunGetTimeAndSales(ConnectionDetails connectionDetails)
        {
            var ibClient = new IBClient();
            ibClient.NotificationReceived += new EventHandler<Notification>(Common.NotificationReceived);
            ConnectionHelper.StartIbClient(ibClient, connectionDetails);
            ibClient.GetLatestHistoricalTimeAndSales(1005, _contract, WhatToShow.TRADES);
        }
        public static void RunGetNews(ConnectionDetails connectionDetails)
        {
            var ibClient = new IBClient();
            ibClient.NotificationReceived += new EventHandler<Notification>(Common.NotificationReceived);
            ConnectionHelper.StartIbClient(ibClient, connectionDetails);
        }
        private static void BarUpdateReceived(object sender, RealTimeBarUpdate barUpdate)
        {
            Console.WriteLine($"{DateTime.Now}: Open:{barUpdate.Open} High:{barUpdate.High} Close:{barUpdate.Close} Low:{barUpdate.Low}");
        }
        private static void HistoricalDataUpdateEndReceived(object sender, BarUpdate barUpdate)
        {
            Console.WriteLine($"{DateTime.Now}: Open:{barUpdate.Bar.Open} High:{barUpdate.Bar.High} Close:{barUpdate.Bar.Close} Low:{barUpdate.Bar.Low}");
        }
    }
}
