using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IB.Api.Client.IBKR;
using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Implementation.Helper
{
    public static class ContractHelper
    {
        public static List<TradingHours> GetTradingHours(ContractDetails contractDetails)
        {
            const string dateFormat = "yyyyMMdd:HHmm";
            var tradingHours = contractDetails.TradingHours.Split(';');
            var output = (from item in tradingHours
                select item.Trim()
                into tradingHoursItem
                where tradingHoursItem.Length > 0 && ValidTradingHoursItem(tradingHoursItem)
                select tradingHoursItem.Split('-')
                into hours
                select new TradingHours
                {
                    Symbol = contractDetails.Contract.Symbol,
                    Start = DateTime.ParseExact(hours[0].Trim(), dateFormat, CultureInfo.InvariantCulture).ToLocalTime(),
                    End = DateTime.ParseExact(hours[1].Trim(), dateFormat, CultureInfo.InvariantCulture).ToLocalTime(),
                    LastTradeDate = contractDetails.Contract.LastTradeDateOrContractMonth != null
                        ? DateTime.ParseExact(contractDetails.Contract.LastTradeDateOrContractMonth.Trim(), "yyyyMMdd", CultureInfo.InvariantCulture).ToShortDateString()
                        : ""
                }).ToList();

            return output.OrderBy(x => x.Start).ToList();
        }

        public static void GetMarginRequirements(IbClient ibClient, Contract contract)
        {
            var nextId = ibClient.NextOrderId++;
            var order = new Order
            {
                OrderType = nameof(OrderType.MARKET),
                Action = nameof(TradeAction.BUY),
                TotalQuantity = 1
            };
            ibClient.WhatIf(nextId, contract, order);
        }

        public static TradingHours GetNextSession(ContractDetails contractDetails)
        {
            var tradingHours = GetTradingHours(contractDetails);

            return tradingHours.Find(x =>
                (DateTime.Now > x.Start && DateTime.Now < x.End) ||
                (DateTime.Now < x.Start && DateTime.Now < x.End));
        }

        private static bool ValidTradingHoursItem(string tradingHoursItem)
        {
            return !tradingHoursItem.Contains("CLOSED");
        }

        /// <summary>
        /// An example of EUR/USD - symbol=EUR and currency=USD
        /// </summary>
        /// <param name="ibClient"></param>
        /// <param name="symbol"></param>
        /// <param name="currency"></param>
        public static void RequestForexContract(IbClient ibClient, string symbol, string currency)
        {
            ibClient.ReqContractDetails(1020, new Contract
            {
                Symbol = symbol,
                SecType = nameof(SecurityType.CASH),
                Exchange = "IDEALPRO",
                Currency = currency
            });
        }

        public static void RequestStockContract(IbClient ibClient, string symbol)
        {
            ibClient.ReqContractDetails(symbol, SecurityType.STK);
        }

        public static void RequestFuturesContract(IbClient ibClient, string symbol)
        {
            ibClient.ReqContractDetails(symbol, SecurityType.FUT);
        }

        public static void RequestIndexContract(IbClient ibClient, string symbol)
        {
            ibClient.ReqContractDetails(symbol, SecurityType.IND);
        }

        public static void RequestOptionsOnFuturesContract(IbClient ibClient, string symbol)
        {
            ibClient.ReqContractDetails(symbol, SecurityType.FOP);
        }

        public static void RequestCommodityContract(IbClient ibClient, string symbol, string currency)
        {
            ibClient.ReqContractDetails(1020, new Contract
            {
                Symbol = symbol,
                SecType = nameof(SecurityType.CMDTY),
                Exchange = "SMART",
                Currency = currency
            });
        }
    }
}