/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */
using System;
using System.Collections.Generic;

namespace IBApi
{
    public class DefaultEWrapper : IEWrapper
    {
		//
		// Note to updaters:
		//
		//
		// Please ensure that implementations of new EWrapper methods are declared
		// as virtual, since the only purpose for this class to be public is so that
		// API clients that only wish to consume a subset of the EWrapper interface
		// can create a class that inherits from it and then override just the methods
		// needed (ie Adapter pattern), rather than implementing EWrapper directly.
		//

        public virtual void Error(Exception e)
        {
        }

        public virtual void Error(string str)
        {
        }

        public virtual void Error(int id, int errorCode, string errorMsg, string advancedOrderRejectJson)
        {
        }

        public virtual void CurrentTime(long time)
        {
        }

        public virtual void TickPrice(int tickerId, int field, double price, TickAttrib attribs)
        {
        }

        public virtual void TickSize(int tickerId, int field, decimal size)
        {
        }

        public virtual void TickString(int tickerId, int field, string value)
        {
        }

        public virtual void TickGeneric(int tickerId, int field, double value)
        {
        }

        public virtual void TickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
        {
        }

        public virtual void DeltaNeutralValidation(int reqId, DeltaNeutralContract deltaNeutralContract)
        {
        }

        public virtual void TickOptionComputation(int tickerId, int field, int tickAttrib, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
        {
        }

        public virtual void TickSnapshotEnd(int tickerId)
        {
        }

        public virtual void NextValidId(int orderId)
        {
        }

        public virtual void ManagedAccounts(string accountsList)
        {
        }

        public virtual void ConnectionClosed()
        {
        }

        public virtual void AccountSummary(int reqId, string account, string tag, string value, string currency)
        {
        }

        public virtual void AccountSummaryEnd(int reqId)
        {
        }

        public virtual void BondContractDetails(int reqId, ContractDetails contract)
        {
        }

        public virtual void UpdateAccountValue(string key, string value, string currency, string accountName)
        {
        }

        public virtual void UpdatePortfolio(Contract contract, decimal position, double marketPrice, double marketValue, double averageCost, double unrealizedPNL, double realizedPNL, string accountName)
        {
        }

        public virtual void UpdateAccountTime(string timestamp)
        {
        }

        public virtual void AccountDownloadEnd(string account)
        {
        }

        public virtual void OrderStatus(int orderId, string status, decimal filled, decimal remaining, double avgFillPrice, int permId, int parentId, double lastFillPrice, int clientId, string whyHeld, double mktCapPrice)
        {
        }

        public virtual void OpenOrder(int orderId, Contract contract, Order order, OrderState orderState)
        {
        }

        public virtual void OpenOrderEnd()
        {
        }

        public virtual void ContractDetails(int reqId, ContractDetails contractDetails)
        {
        }

        public virtual void ContractDetailsEnd(int reqId)
        {
        }

        public virtual void ExecDetails(int reqId, Contract contract, Execution execution)
        {
        }

        public virtual void ExecDetailsEnd(int reqId)
        {
        }

        public virtual void CommissionReport(CommissionReport commissionReport)
        {
        }

        public virtual void FundamentalData(int reqId, string data)
        {
        }

        public virtual void HistoricalData(int reqId, Bar bar)
        {
        }

        public virtual void HistoricalDataEnd(int reqId, string start, string end)
        {
        }

        public virtual void MarketDataType(int reqId, int marketDataType)
        {
        }

        public virtual void UpdateMktDepth(int tickerId, int position, int operation, int side, double price, decimal size)
        {
        }

        public virtual void UpdateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, decimal size, bool isSmartDepth)
        {
        }

        public virtual void UpdateNewsBulletin(int msgId, int msgType, string message, string origExchange)
        {
        }

        public virtual void Position(string account, Contract contract, decimal pos, double avgCost)
        {
        }

        public virtual void PositionEnd()
        {
        }

        public virtual void RealtimeBar(int reqId, long time, double open, double high, double low, double close, decimal volume, decimal WAP, int count)
        {
        }

        public virtual void ScannerParameters(string xml)
        {
        }

        public virtual void ScannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
        {
        }

        public virtual void ScannerDataEnd(int reqId)
        {
        }

        public virtual void ReceiveFA(int faDataType, string faXmlData)
        {
        }

        public virtual void VerifyMessageAPI(string apiData)
        {
        }

        public virtual void VerifyCompleted(bool isSuccessful, string errorText)
        {
        }

        public virtual void VerifyAndAuthMessageAPI(string apiData, string xyzChallenge)
        {
        }

        public virtual void VerifyAndAuthCompleted(bool isSuccessful, string errorText)
        {
        }

        public virtual void DisplayGroupList(int reqId, string groups)
        {
        }

        public virtual void DisplayGroupUpdated(int reqId, string contractInfo)
        {
        }

        public virtual void ConnectAck()
        {
        }

        public virtual void PositionMulti(int requestId, string account, string modelCode, Contract contract, decimal pos, double avgCost)
        {
        }

        public virtual void PositionMultiEnd(int requestId)
        {
        }

        public virtual void AccountUpdateMulti(int requestId, string account, string modelCode, string key, string value, string currency)
        {
        }

        public virtual void AccountUpdateMultiEnd(int requestId)
        {
        }


        public virtual void SecurityDefinitionOptionParameter(int reqId, string exchange, int underlyingConId, string tradingClass, string multiplier, HashSet<string> expirations, HashSet<double> strikes)
        {
        }

        public virtual void SecurityDefinitionOptionParameterEnd(int reqId)
        {
        }

        public virtual void SoftDollarTiers(int reqId, SoftDollarTier[] tiers)
        {
        }

        public virtual void FamilyCodes(FamilyCode[] familyCodes)
        {
        }

        public virtual void SymbolSamples(int reqId, ContractDescription[] contractDescriptions)
        {
        }

        public virtual void MktDepthExchanges(DepthMktDataDescription[] descriptions)
        {
        }

        public virtual void TickNews(int tickerId, long timeStamp, string providerCode, string articleId, string headline, string extraData)
        {
        }

        public virtual void SmartComponents(int reqId, Dictionary<int, KeyValuePair<string, char>> theMap)
        {
        }

        public virtual void TickReqParams(int tickerId, double minTick, string bboExchange, int snapshotPermissions)
        {
        }

        public virtual void NewsProviders(NewsProvider[] newsProviders)
        {
        }

        public virtual void NewsArticle(int requestId, int articleType, string articleText)
        {
        }

        public virtual void HistoricalNews(int requestId, string time, string providerCode, string articleId, string headline)
        {
        }

        public virtual void HistoricalNewsEnd(int requestId, bool hasMore)
        {
        }

        public virtual void HeadTimestamp(int reqId, string headTimestamp)
        {
        }


        public virtual void HistogramData(int reqId, HistogramEntry[] data)
        {
        }

        public virtual void HistoricalDataUpdate(int reqId, Bar bar)
        {
        }

        public virtual void RerouteMktDataReq(int reqId, int conId, string exchange)
        {
        }

        public virtual void RerouteMktDepthReq(int reqId, int conId, string exchange)
        {
        }

        public virtual void MarketRule(int marketRuleId, PriceIncrement[] priceIncrements)
        {
        }


        public virtual void Pnl(int reqId, double dailyPnL, double unrealizedPnL, double realizedPnL)
        {

        }

        public virtual void PnlSingle(int reqId, decimal pos, double dailyPnL, double realizedPnL, double value, double unrealizedPnL)
        {

        }

        public virtual void HistoricalTicks(int reqId, HistoricalTick[] ticks, bool done)
        {
        }

        public virtual void HistoricalTicksBidAsk(int reqId, HistoricalTickBidAsk[] ticks, bool done)
        {
        }

        public virtual void HistoricalTicksLast(int reqId, HistoricalTickLast[] ticks, bool done)
        {
        }

        public virtual void TickByTickAllLast(int reqId, int tickType, long time, double price, decimal size, TickAttribLast tickAttribLast, string exchange, string specialConditions)
        {
        }

        public virtual void TickByTickBidAsk(int reqId, long time, double bidPrice, double askPrice, decimal bidSize, decimal askSize, TickAttribBidAsk tickAttribBidAsk)
        {
        }

        public virtual void TickByTickMidPoint(int reqId, long time, double midPoint)
        {
        }

        public virtual void OrderBound(long orderId, int apiClientId, int apiOrderId)
        {
        }

        public virtual void CompletedOrder(Contract contract, Order order, OrderState orderState)
        {
        }

        public virtual void CompletedOrdersEnd()
        {
        }

        public virtual void ReplaceFAEnd(int reqId, string text)
        {
        }

        public virtual void WshMetaData(int reqId, string dataJson)
        {
        }

        public virtual void WshEventData(int reqId, string dataJson)
        {
        }

        public void HistoricalSchedule(int reqId, string startDateTime, string endDateTime, string timeZone, HistoricalSession[] sessions)
        {
        }

        public void UserInfo(int reqId, string whiteBrandingId)
        {
        }
    }
}
