using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using IB.Api.Client.Implementation.Model;
using IBApi;

namespace IB.Api.Client.Helper
{
    public static class ContractHelper
    {
        public static List<TradingHours> GetTradingHours(ContractDetails contractDetails)
        {
            const string dateFormat = "yyyyMMdd:HHmm";
            var output = new List<TradingHours>();
            var tradingHours = contractDetails.TradingHours.Split(';');
            foreach (var item in tradingHours)
            {
                var tradingHoursItem = item.Trim();
                if (tradingHoursItem.Length > 0 && ValidTradingHoursItem(tradingHoursItem))
                {
                    var hours = tradingHoursItem.Split('-');
                    var tradingHour = new TradingHours
                    {
                        Symbol = contractDetails.Contract.Symbol,
                        Start = DateTime.ParseExact(hours[0].Trim(), dateFormat, CultureInfo.InvariantCulture).ToLocalTime(),
                        End = DateTime.ParseExact(hours[1].Trim(), dateFormat, CultureInfo.InvariantCulture).ToLocalTime(),
                        LastTradeDate = contractDetails.Contract.LastTradeDateOrContractMonth != null ? DateTime.ParseExact(contractDetails.Contract.LastTradeDateOrContractMonth.Trim(), "yyyyMMdd", CultureInfo.InvariantCulture).ToShortDateString() : ""
                    };
                    output.Add(tradingHour);
                }
            }
            return output.OrderBy(x => x.Start).ToList();
        }

        public static void GetMarginRequirements(IBClient ibClient, Contract contract)
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
        /// An example of EUR/USD would be symbol=EUR and currency=USD
        /// </summary>
        /// <param name="ibClient"></param>
        /// <param name="symbol"></param>
        /// <param name="currency"></param>
        public static void RequestForexContract(IBClient ibClient, string symbol, string currency)
        {
            ibClient.GetContractDetails(new Contract
            {
                Symbol = symbol,
                SecType = nameof(SecurityType.CASH),
                Exchange = "IDEALPRO",
                Currency = currency
            });
        }

        public static void RequestStockContract(IBClient ibClient, string symbol)
        {
            ibClient.GetContractDetails(symbol, SecurityType.STK);
        }

        public static void RequestFuturesContract(IBClient ibClient, string symbol)
        {
            ibClient.GetContractDetails(symbol, SecurityType.FUT);
        }

        public static void RequestIndexContract(IBClient ibClient, string symbol)
        {
            ibClient.GetContractDetails(symbol, SecurityType.IND);
        }

        public static void RequestOptionsOnFuturesContract(IBClient ibClient, string symbol)
        {
            ibClient.GetContractDetails(symbol, SecurityType.FOP);
        }

        public static void RequestOptionsContract(IBClient ibClient, string symbol)
        {
            ibClient.GetContractDetails(symbol, SecurityType.OPT);
        }

        public static void RequestComodityContract(IBClient ibClient, string symbol, string currency)
        {
            ibClient.GetContractDetails(new Contract
            {
                Symbol = symbol,
                SecType = nameof(SecurityType.CMDTY),
                Exchange = "SMART",
                Currency = currency
            });
        }
    }
}
