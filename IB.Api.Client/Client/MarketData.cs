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
            ClientSocket.ReqTickByTickData(reqId, contract, "BidAsk", 0, true);
            Notify($"Time and sales for symbol {contract.Symbol} requested");
        }
        public void SubscribeToRealTimePrice(int tickerId, Contract contract)
        {
            _priceUpdates.Add(tickerId, new PriceUpdate
            {
                TickerId = tickerId
            });
            ClientSocket.ReqMktData(tickerId, contract, string.Empty, false, false, null);
            Notify($"Real time data for symbol {contract.Symbol} requested");
        }
        public void SubscribeToOptionsChainStrike(int tickerId, OptionParameterDefinition optionParameter, double strike, Right right)
        {
            _priceUpdates.Add(tickerId, new PriceUpdate
            {
                TickerId = tickerId
            });
            var contract = new Contract
            {
                Strike = strike,
                Exchange = optionParameter.Exchange,
                ConId = optionParameter.UnderlyingConId,
                TradingClass = optionParameter.TradingClass,
                Multiplier = optionParameter.Multiplier,
                Right = right.ToString(),
                SecType = SecurityType.FOP.ToString()
            };
            ClientSocket.ReqMktData(tickerId, contract, string.Empty, false, false, null);
            Notify($"Real time options data for trading class {contract.TradingClass} requested");
        }
        public void SubscribeToDefaultBar(int tickerId, Contract contract)
        {
            ClientSocket.ReqRealTimeBars(tickerId, contract, 0, nameof(WhatToShow.TRADES), false, null);
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

            ClientSocket.ReqMarketDepth(reqId, contract, 10, false, null);
            Notify($"Subscribed to {contract.Symbol} marketDepth");
        }
        public void ReqOptionParameters(int reqId, ContractDetails contractDetails)
        {
            _optionParameterDefinitions = [];
            Notify($"Derivatives parameters for symbol {contractDetails.Contract.Symbol} requested");

            if (contractDetails.Contract.SecType == SecurityType.FUT.ToString())
                ClientSocket.ReqSecDefOptParams(reqId, contractDetails.Contract.Symbol, contractDetails.Contract.Exchange, contractDetails.Contract.SecType, contractDetails.Contract.ConId);
            else
                ClientSocket.ReqSecDefOptParams(reqId, contractDetails.Contract.Symbol, string.Empty, contractDetails.Contract.SecType, contractDetails.Contract.ConId);
        }
        public void UpdateMktDepth(int tickerId, int position, int operation, int side, double price, decimal size)
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
        public virtual void TickPrice(int tickerId, int field, double price, TickAttrib attribs)
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
        public void TickByTickBidAsk(int reqId, long time, double bidPrice, double askPrice, decimal bidSize, decimal askSize, TickAttribBidAsk tickAttribBidAsk)
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
        public void MarketDataType(int reqId, int marketDataType)
        {
            _priceUpdates[reqId].MarketDataType = marketDataType;
        }
        public void TickReqParams(int tickerId, double minTick, string bboExchange, int snapshotPermissions)
        {
            _priceUpdates[tickerId].MinTick = minTick;
            _priceUpdates[tickerId].BboExchange = bboExchange;
            _priceUpdates[tickerId].SnapshotPermissions = snapshotPermissions;
        }
        public virtual void TickSize(int tickerId, int field, decimal size)
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
        public virtual void TickString(int tickerId, int field, string value)
        {
        }
        public virtual void TickGeneric(int tickerId, int field, double value)
        {
        }
        public void RealtimeBar(int reqId, long date, double open, double high, double low, double close, decimal volume, decimal WAP, int count)
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
        public void TickOptionComputation(int tickerId, int field,
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
        }
        public void SecurityDefinitionOptionParameter(int reqId, string exchange, int underlyingConId, string tradingClass, string multiplier, HashSet<string> expirations, HashSet<double> strikes)
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
        public void SecurityDefinitionOptionParameterEnd(int reqId)
        {
            OptionParametersReceived?.Invoke(this, _optionParameterDefinitions);
        }
    }
}
