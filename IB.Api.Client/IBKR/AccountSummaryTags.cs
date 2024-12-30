/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

namespace IB.Api.Client.IBKR
{
    /**
     * @class AccountSummaryTags
     * @brief class containing all existing values being reported by EClientSocket::reqAccountSummary
     */
    public class AccountSummaryTags
    {
        private const string AccountType = "AccountType";
        private const string NetLiquidation = "NetLiquidation";
        private const string TotalCashValue = "TotalCashValue";
        private const string SettledCash = "SettledCash";
        private const string AccruedCash = "AccruedCash";
        private const string BuyingPower = "BuyingPower";
        private const string EquityWithLoanValue = "EquityWithLoanValue";
        private const string PreviousDayEquityWithLoanValue = "PreviousDayEquityWithLoanValue";
        private const string GrossPositionValue = "GrossPositionValue";
        private const string ReqTEquity = "ReqTEquity";
        private const string ReqTMargin = "ReqTMargin";
        private const string SMA = "SMA";
        private const string InitMarginReq = "InitMarginReq";
        private const string MaintMarginReq = "MaintMarginReq";
        private const string AvailableFunds = "AvailableFunds";
        private const string ExcessLiquidity = "ExcessLiquidity";
        private const string Cushion = "Cushion";
        private const string FullInitMarginReq = "FullInitMarginReq";
        private const string FullMaintMarginReq = "FullMaintMarginReq";
        private const string FullAvailableFunds = "FullAvailableFunds";
        private const string FullExcessLiquidity = "FullExcessLiquidity";
        private const string LookAheadNextChange = "LookAheadNextChange";
        private const string LookAheadInitMarginReq = "LookAheadInitMarginReq";
        private const string LookAheadMaintMarginReq = "LookAheadMaintMarginReq";
        private const string LookAheadAvailableFunds = "LookAheadAvailableFunds";
        private const string LookAheadExcessLiquidity = "LookAheadExcessLiquidity";
        private const string HighestSeverity = "HighestSeverity";
        private const string DayTradesRemaining = "DayTradesRemaining";
        private const string Leverage = "Leverage";

        public static string GetAllTags() => $"{AccountType},{NetLiquidation},{TotalCashValue},{SettledCash},{AccruedCash},{BuyingPower},{EquityWithLoanValue},{PreviousDayEquityWithLoanValue},{GrossPositionValue},{ReqTEquity},{ReqTMargin},{SMA},{InitMarginReq},{MaintMarginReq},{AvailableFunds},{ExcessLiquidity},{Cushion},{FullInitMarginReq},{FullMaintMarginReq},{FullAvailableFunds},{FullExcessLiquidity},{LookAheadNextChange},{LookAheadInitMarginReq},{LookAheadMaintMarginReq},{LookAheadAvailableFunds},{LookAheadExcessLiquidity},{HighestSeverity},{DayTradesRemaining},{Leverage}";
    }
}