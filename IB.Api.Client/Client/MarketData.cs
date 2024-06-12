using System;
using System.Collections.Generic;
using System.Linq;
using IB.Api.Client.Model;
using IBApi;
using IB.Api.Client.Helper;

namespace IB.Api.Client
{
    //MarketData
    public partial class IBClient
    {
        private readonly Dictionary<int, PriceUpdate> _priceUpdates = [];
        private readonly Dictionary<int, OrderBookUpdate> _orderBookUpdates = [];
        private List<OptionParameterDefinition> _optionParameterDefinitions;
        public event EventHandler<OrderBookUpdate> OrderBookUpdateReceived;
        public event EventHandler<PriceUpdate> PriceUpdateReceived;
        public event EventHandler<HistoricalTickBidAsk> TimeAndSalesUpdateReceived;
        public event EventHandler<RealTimeBarUpdate> BarUpdateReceived;
        public event EventHandler<List<OptionParameterDefinition>> OptionParametersReceived;
        public void SubscribeToTimeAndSales(int reqId, Contract contract)
        {
            ClientSocket.reqTickByTickData(reqId, contract, "BidAsk", 0, true);
            Notify($"Time and sales for symbol {contract.Symbol} requested");
        }
        public void SubscribeToRealTimePrice(int tickerId, Contract contract, string genericTickList)
        {
            _priceUpdates.Add(tickerId, new PriceUpdate
            {
                TickerId = tickerId
            });
            ClientSocket.reqMktData(tickerId, contract, genericTickList, false, false, null);
            Notify($"Real time data for symbol {contract.Symbol} requested");
        }
        public void SubscribeToRealTimePrice(int tickerId, Contract contract)
        {
            _priceUpdates.Add(tickerId, new PriceUpdate
            {
                TickerId = tickerId
            });
            ClientSocket.reqMktData(tickerId, contract, string.Empty, false, false, null);
            Notify($"Real time data for symbol {contract.Symbol} requested");
        }
        public void SubscribeToDefaultBar(int tickerId, Contract contract)
        {
            ClientSocket.reqRealTimeBars(tickerId, contract, 0, nameof(WhatToShow.TRADES), false, null);
        }
        public void ReqMarketDepth(int reqId, Contract contract, double ratio)
        {
            var orderBookUpdate = new OrderBookUpdate
            {
                Ratio = ratio,
                OrderBookLines = new OrderBookLine[20]
            };
            for (int iterator = 0; iterator < orderBookUpdate.OrderBookLines.Length; iterator++)
                orderBookUpdate.OrderBookLines[iterator] = new OrderBookLine();
            _orderBookUpdates.Add(reqId, orderBookUpdate);

            ClientSocket.reqMarketDepth(reqId, contract, 10, false, null);
            Notify($"Subscribed to {contract.Symbol} marketDepth");
        }
        public void ReqOptionParameters(int reqId, ContractDetails contractDetails)
        {
            _optionParameterDefinitions = [];
            Notify($"Derivatives parameters for symbol {contractDetails.Contract.Symbol} requested");
            ClientSocket.reqSecDefOptParams(reqId, contractDetails.Contract.Symbol, string.Empty, contractDetails.Contract.SecType, contractDetails.Contract.ConId);
        }
        
        void IEWrapper.UpdateMktDepth(int tickerId, int position, int operation, int side, double price, decimal size)
        {
            if (side == 0)
                position += 10;

            _orderBookUpdates[tickerId].OrderBookLines[position] = new OrderBookLine
            {
                Position = position,
                Operation = operation,
                Side = side,
                Price = price,
                Size = size
            };
            _orderBookUpdates[tickerId].TickerId = tickerId;
            var max = _orderBookUpdates[tickerId].OrderBookLines.Max(x => x.Size);
            var sumBySide = _orderBookUpdates[tickerId].OrderBookLines.Where(x => x.Side == side).Sum(x => x.Size);

            if (side == 1)
                _orderBookUpdates[tickerId].SumBid = sumBySide;
            else
                _orderBookUpdates[tickerId].SumOffer = sumBySide;
            _orderBookUpdates[tickerId].OrderBookLines[position].PercentageOfBook = Math.Round(100 * _orderBookUpdates[tickerId].OrderBookLines[position].Size / max);
            _orderBookUpdates[tickerId].CurrentPrice = _orderBookUpdates[tickerId].OrderBookLines[0].Price;

            if (position == 19)
                OrderBookUpdateReceived?.Invoke(this, _orderBookUpdates[tickerId]);
        }
        void IEWrapper.TickPrice(int tickerId, int field, double price, TickAttrib attribs)
        {
            switch (field)
            {
                case 1:
                    {
                        _priceUpdates[tickerId].Bid = price;
                        break;
                    }
                case 2:
                    {
                        _priceUpdates[tickerId].Ask = price;
                        break;
                    }
            }
        }
        void IEWrapper.TickByTickBidAsk(int reqId, long time, double bidPrice, double askPrice, decimal bidSize, decimal askSize, TickAttribBidAsk tickAttribBidAsk)
        {
            _ = reqId;
            var tick = new HistoricalTickBidAsk
            {
                Time = time,
                PriceAsk = askPrice,
                PriceBid = bidPrice,
                SizeAsk = askSize,
                SizeBid = bidSize,
                TickAttribBidAsk = tickAttribBidAsk
            };
            TimeAndSalesUpdateReceived?.Invoke(this, tick);
        }
        void IEWrapper.MarketDataType(int reqId, int marketDataType)
        {
            _priceUpdates[reqId].MarketDataType = marketDataType;
        }
        void IEWrapper.TickReqParams(int tickerId, double minTick, string bboExchange, int snapshotPermissions)
        {
            _priceUpdates[tickerId].MinTick = minTick;
            _priceUpdates[tickerId].BboExchange = bboExchange;
            _priceUpdates[tickerId].SnapshotPermissions = snapshotPermissions;
        }
        void IEWrapper.TickSize(int tickerId, int field, decimal size)
        {
            switch (field)
            {
                case 0:
                    {
                        _priceUpdates[tickerId].BidSize = size;
                        break;
                    }
                case 3:
                    {
                        _priceUpdates[tickerId].AskSize = size;
                        SetPriceBar(tickerId);
                        PriceUpdateReceived?.Invoke(this, _priceUpdates[tickerId]);
                        break;
                    }
            }
        }
        private void SetPriceBar(int tickerId)
        {
            var tzi = TimeZoneInfo.FindSystemTimeZoneById("US/Central");
            var now = TimeZoneInfo.ConvertTime(DateTime.Now, tzi);
            var epochTimeHour = DateHelper.DateToEpoch(new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Utc));

            if (epochTimeHour != _priceUpdates[tickerId].Time)
            {
                _priceUpdates[tickerId].Time = epochTimeHour;
                _priceUpdates[tickerId].Open = _priceUpdates[tickerId].Bid;
                _priceUpdates[tickerId].Close = _priceUpdates[tickerId].Ask;
                _priceUpdates[tickerId].High = _priceUpdates[tickerId].Ask;
                _priceUpdates[tickerId].Low = _priceUpdates[tickerId].Bid;
                _priceUpdates[tickerId].Volume = 0;
                _priceUpdates[tickerId].Volume += _priceUpdates[tickerId].BidSize;
                _priceUpdates[tickerId].Volume -= _priceUpdates[tickerId].AskSize;
            }
            else
            {
                _priceUpdates[tickerId].Close = _priceUpdates[tickerId].Ask;
                _priceUpdates[tickerId].High = _priceUpdates[tickerId].Ask > _priceUpdates[tickerId].High ? _priceUpdates[tickerId].Ask : _priceUpdates[tickerId].High;
                _priceUpdates[tickerId].Low = _priceUpdates[tickerId].Bid < _priceUpdates[tickerId].Low ? _priceUpdates[tickerId].Bid : _priceUpdates[tickerId].Low;
                _priceUpdates[tickerId].Volume += _priceUpdates[tickerId].BidSize;
                _priceUpdates[tickerId].Volume -= _priceUpdates[tickerId].AskSize;
            }
        }
        void IEWrapper.TickString(int tickerId, int field, string value)
        {
        }
        void IEWrapper.TickGeneric(int tickerId, int field, double value)
        {
        }
        void IEWrapper.RealtimeBar(int reqId, long date, double open, double high, double low, double close, decimal volume, decimal WAP, int count)
        {
            _ = reqId;
            var realtimeBarUpdate = new RealTimeBarUpdate
            {
                Date = date,
                Open = open,
                High = high,
                Low = low,
                Close = close,
                Volume = volume,
                Count = count,
                Vwap = WAP
            };
            BarUpdateReceived?.Invoke(this, realtimeBarUpdate);
        }
        void IEWrapper.TickOptionComputation(int tickerId, int field,
        int tickAttrib, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
        {
            switch (field)
            {
                case 10:
                    {
                        _priceUpdates[tickerId].OptionBid = optPrice;
                        _priceUpdates[tickerId].Bid = undPrice;
                        break;
                    }
                case 11:
                    {
                        _priceUpdates[tickerId].OptionAsk = optPrice;
                        _priceUpdates[tickerId].Ask = undPrice;
                        break;
                    }
            }
            _priceUpdates[tickerId].Gamma = gamma;
            _priceUpdates[tickerId].Delta = delta;
            _priceUpdates[tickerId].Vega = vega;
            _priceUpdates[tickerId].Theta = theta;
        }
        void IEWrapper.SecurityDefinitionOptionParameter(int reqId, string exchange, int underlyingConId, string tradingClass, string multiplier, HashSet<string> expirations, HashSet<double> strikes)
        {
            _optionParameterDefinitions.Add(new OptionParameterDefinition
            {
                Exchange = exchange,
                UnderlyingConId = underlyingConId,
                TradingClass = tradingClass,
                Multiplier = multiplier,
                Expirations = expirations,
                Strikes = strikes
            });
        }
        void IEWrapper.SecurityDefinitionOptionParameterEnd(int reqId)
        {
            OptionParametersReceived?.Invoke(this, _optionParameterDefinitions);
        }
    }
}
