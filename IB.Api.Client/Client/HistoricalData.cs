using System;
using System.Collections.Generic;
using IB.Api.Client.Model;
using IB.Api.Client.Helper;
using IBApi;

namespace IB.Api.Client
{
    public partial class IBClient
    {
        private readonly List<WhatToShow> _allowedTimeAndSalesTickTypes = [WhatToShow.TRADES, WhatToShow.BID_ASK, WhatToShow.MIDPOINT];
        private readonly Dictionary<int, List<Bar>> _historicalData = [];
        private Dictionary<int, List<HistoricalTick>> _historicalTicks;
        private Dictionary<int, List<HistoricalTickBidAsk>> _historicalTickBidAsk;
        private Dictionary<int, List<HistoricalTickLast>> _historicalTickLast;
        public event EventHandler<Tuple<int, List<Bar>>> HistoricalDataReceived;
        public event EventHandler<BarUpdate> HistoricalDataUpdateReceived;
        public event EventHandler<Tuple<int, List<HistoricalTick>>> HistoricalTimeAndSalesTickUpdateReceived;
        public event EventHandler<Tuple<int, List<HistoricalTickBidAsk>>> HistoricalTimeAndSalesTickBidAskUpdateReceived;
        public event EventHandler<Tuple<int, List<HistoricalTickLast>>> HistoricalTimeAndSalesTickLastUpdateReceived;

        /// <summary>
        /// For Bar Sizes check BarSize class
        /// For durations check Duration class
        /// </summary>
        public void GetHistoricalData(int reqId, Contract contract, string duration, string barSize, WhatToShow whatToShow, Rth rth, bool keepUpToDate)
        {
            _historicalData.Add(reqId, []);
            ClientSocket.ReqHistoricalData(reqId, contract, string.Empty, duration, barSize, whatToShow.ToString(), (int)rth, 1, keepUpToDate, null);
            Notify($"Historical data for symbol {contract.Symbol} requested");
        }

        /// <summary>
        /// Specify either start or end date. It doesn't accept both
        /// </summary>
        /// <param name="reqId"></param>
        /// <param name="contract"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="whatToShow"></param>
        public void GetLatestHistoricalTimeAndSales(int reqId, Contract contract, WhatToShow whatToShow)
        {
            _historicalTicks = [];
            _historicalTickBidAsk = [];
            _historicalTickLast = [];

            if (_allowedTimeAndSalesTickTypes.Contains(whatToShow))
            {
                string endTime = DateHelper.ConvertToApiDate(DateTime.Now);
                InitializeHistoricalTickDictionary(reqId, whatToShow);
                ClientSocket.ReqHistoricalTicks(reqId, contract, null, endTime, 1000, whatToShow.ToString(), 0, true, null);
                Notify($"Time and Sales for symbol {contract.Symbol} requested");
            }
            else
            {
                Notify($"WhatToShow tick type: {whatToShow} not allowed");
            }
        }
        private void InitializeHistoricalTickDictionary(int reqId, WhatToShow whatToShow)
        {
            switch (whatToShow)
            {
                case WhatToShow.TRADES:
                    {
                        _historicalTickLast[reqId] = [];
                        break;
                    }
                case WhatToShow.MIDPOINT:
                    {
                        _historicalTicks[reqId] = [];
                        break;
                    }
                case WhatToShow.BID_ASK:
                    {
                        _historicalTickBidAsk[reqId] = [];
                        break;
                    }
            }
        }
        void IEWrapper.HistoricalData(int reqId, Bar bar)
        {
            _historicalData[reqId].Add(bar);
        }
        void IEWrapper.HistoricalDataEnd(int reqId, string start, string end)
        {
            var data = _historicalData[reqId];
            HistoricalDataReceived?.Invoke(this, new Tuple<int, List<Bar>>(reqId, data));
        }
        void IEWrapper.HistoricalDataUpdate(int reqId, Bar bar)
        {
            var barUpdate = new BarUpdate
            {
                RequestId = reqId,
                Bar = bar
            };
            HistoricalDataUpdateReceived?.Invoke(this, barUpdate);
        }
        void IEWrapper.HistoricalTicks(int reqId, HistoricalTick[] ticks, bool done)
        {
            _historicalTicks[reqId].AddRange(ticks);
            if (done)
            {
                HistoricalTimeAndSalesTickUpdateReceived?.Invoke(this, new Tuple<int, List<HistoricalTick>>(reqId, _historicalTicks[reqId]));
                _historicalTicks[reqId] = [];
            }
        }
        void IEWrapper.HistoricalTicksBidAsk(int reqId, HistoricalTickBidAsk[] ticks, bool done)
        {
            _historicalTickBidAsk[reqId].AddRange(ticks);
            if (done)
            {
                HistoricalTimeAndSalesTickBidAskUpdateReceived?.Invoke(this, new Tuple<int, List<HistoricalTickBidAsk>>(reqId, _historicalTickBidAsk[reqId]));
                _historicalTickBidAsk[reqId] = [];
            }
        }
        void IEWrapper.HistoricalTicksLast(int reqId, HistoricalTickLast[] ticks, bool done)
        {
            _historicalTickLast[reqId].AddRange(ticks);
            if (done)
            {
                HistoricalTimeAndSalesTickLastUpdateReceived?.Invoke(this, new Tuple<int, List<HistoricalTickLast>>(reqId, _historicalTickLast[reqId]));
                _historicalTickLast[reqId] = [];
            }
        }
    }
}
