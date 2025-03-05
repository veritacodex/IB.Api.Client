using System;
using System.Collections.Generic;
using System.Linq;
using IB.Api.Client.Helper;
using IB.Api.Client.IBKR;
using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Implementation
{
    //MarketData
    public partial class IbClient
    {
        private readonly Dictionary<int, PriceUpdate> _priceUpdates = [];
        private readonly Dictionary<int, OrderBookUpdate> _orderBookUpdates = [];
        private List<OptionParameterDefinition> _optionParameterDefinitions;
        public event EventHandler<OrderBookUpdate> OrderBookUpdateReceived;
        public event EventHandler<PriceUpdate> PriceUpdateReceived;
        public event EventHandler<HistoricalTickBidAsk> TimeAndSalesUpdateReceived;
        public event EventHandler<Bar> BarUpdateReceived;
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

            Notify(contract.Strike != 0
                ? $"Option chain real time data requested for {contract.Symbol}. Strike:{contract.Strike} Side:{contract.Right}"
                : $"Real time data for symbol {contract.Symbol} requested");
        }
        public void SubscribeToDefaultBar(int tickerId, Contract contract, WhatToShow whatToShow)
        {
            ClientSocket.reqRealTimeBars(tickerId, contract, 0, whatToShow.ToString(), false, null);
            Notify($"Default bar for symbol {contract.Symbol} requested");
        }
        public void ReqMarketDepth(int reqId, Contract contract, double ratio)
        {
            var orderBookUpdate = new OrderBookUpdate
            {
                Ratio = ratio,
                OrderBookLines = new OrderBookLine[20]
            };
            for (var iterator = 0; iterator < orderBookUpdate.OrderBookLines.Length; iterator++)
                orderBookUpdate.OrderBookLines[iterator] = new OrderBookLine();
            _orderBookUpdates.Add(reqId, orderBookUpdate);

            ClientSocket.reqMarketDepth(reqId, contract, 10, false, null);
            Notify($"Subscribed to {contract.Symbol} marketDepth");
        }
        public void ReqOptionOnFuturesParameters(int reqId, ContractDetails contractDetails)
        {
            _optionParameterDefinitions = [];
            Notify($"Derivatives parameters for symbol {contractDetails.Contract.Symbol} requested");
            ClientSocket.reqSecDefOptParams(reqId, contractDetails.Contract.Symbol, contractDetails.Contract.Exchange, contractDetails.Contract.SecType, contractDetails.Contract.ConId);
        }
        public void ReqOptionParameters(int reqId, ContractDetails contractDetails)
        {
            _optionParameterDefinitions = [];
            Notify($"Derivatives parameters for symbol {contractDetails.Contract.Symbol} requested");
            ClientSocket.reqSecDefOptParams(reqId, contractDetails.Contract.Symbol, string.Empty, contractDetails.Contract.SecType, contractDetails.Contract.ConId);
        }
        void IEWrapper.updateMktDepth(int tickerId, int position, int operation, int side, double price, decimal size)
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
        void IEWrapper.tickPrice(int tickerId, int field, double price, TickAttrib attribs)
        {
            _ = attribs;

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
        void IEWrapper.tickByTickBidAsk(int reqId, long time, double bidPrice, double askPrice, decimal bidSize, decimal askSize, TickAttribBidAsk tickAttribBidAsk)
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
        void IEWrapper.marketDataType(int reqId, int marketDataType)
        {
            _priceUpdates[reqId].MarketDataType = marketDataType;
        }
        void IEWrapper.tickReqParams(int tickerId, double minTick, string bboExchange, int snapshotPermissions)
        {
            _priceUpdates[tickerId].MinTick = minTick;
            _priceUpdates[tickerId].BboExchange = bboExchange;
            _priceUpdates[tickerId].SnapshotPermissions = snapshotPermissions;
        }
        void IEWrapper.tickSize(int tickerId, int field, decimal size)
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
                        _priceUpdates[tickerId].DateTime = DateTime.Now;
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
            }
            else
            {
                _priceUpdates[tickerId].Close = _priceUpdates[tickerId].Ask;
                _priceUpdates[tickerId].High = _priceUpdates[tickerId].Ask > _priceUpdates[tickerId].High ? _priceUpdates[tickerId].Ask : _priceUpdates[tickerId].High;
                _priceUpdates[tickerId].Low = _priceUpdates[tickerId].Bid < _priceUpdates[tickerId].Low ? _priceUpdates[tickerId].Bid : _priceUpdates[tickerId].Low;
            }
        }
        void IEWrapper.tickString(int tickerId, int field, string value)
        {
            DiscardImplementation(tickerId, field, value);
        }
        void IEWrapper.tickGeneric(int tickerId, int field, double value)
        {
            DiscardImplementation(field, tickerId, value);
        }
        void IEWrapper.realtimeBar(int reqId, long date, double open, double high, double low, double close, decimal volume, decimal wap, int count)
        {
            _ = reqId;
            var barUpdate = new Bar(date.ToString(), open,  high, low, close, volume, count, wap );
            BarUpdateReceived?.Invoke(this, barUpdate);
        }
        void IEWrapper.tickOptionComputation(int tickerId, int field, int tickAttrib, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
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
            _priceUpdates[tickerId].TickAttrib = tickAttrib;
            _priceUpdates[tickerId].ImpliedVolatility = impliedVolatility;
            _priceUpdates[tickerId].PvDividend = pvDividend;
        }
        void IEWrapper.securityDefinitionOptionParameter(int reqId, string exchange, int underlyingConId, string tradingClass, string multiplier, HashSet<string> expirations, HashSet<double> strikes)
        {
            _optionParameterDefinitions.Add(new OptionParameterDefinition
            {
                ReqId = reqId,
                Exchange = exchange,
                UnderlyingConId = underlyingConId,
                TradingClass = tradingClass,
                Multiplier = multiplier,
                Expirations = expirations,
                Strikes = strikes
            });
        }
        void IEWrapper.securityDefinitionOptionParameterEnd(int reqId)
        {
            _ = reqId;
            OptionParametersReceived?.Invoke(this, _optionParameterDefinitions);
        }
    }
}
