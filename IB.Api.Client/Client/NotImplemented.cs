using System;
using System.Collections.Generic;
using IBApi;

namespace IB.Api.Client
{
    //Not implemented
    public partial class IBClient
    {
        void IEWrapper.bondContractDetails(int reqId, ContractDetails contract)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.currentTime(long time)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.deltaNeutralValidation(int reqId, DeltaNeutralContract deltaNeutralContract)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.displayGroupList(int reqId, string groups)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.displayGroupUpdated(int reqId, string contractInfo)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.familyCodes(FamilyCode[] familyCodes)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.fundamentalData(int reqId, string data)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.headTimestamp(int reqId, string headTimestamp)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.histogramData(int reqId, HistogramEntry[] data)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.mktDepthExchanges(DepthMktDataDescription[] depthMktDataDescriptions)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.pnl(int reqId, double dailyPnL, double unrealizedPnL, double realizedPnL)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.pnlSingle(int reqId, decimal pos, double dailyPnL, double unrealizedPnL, double realizedPnL, double value)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.receiveFA(int faDataType, string faXmlData)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.rerouteMktDataReq(int reqId, int conId, string exchange)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.rerouteMktDepthReq(int reqId, int conId, string exchange)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.smartComponents(int reqId, Dictionary<int, KeyValuePair<string, char>> theMap)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.softDollarTiers(int reqId, SoftDollarTier[] tiers)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.symbolSamples(int reqId, ContractDescription[] contractDescriptions)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.verifyAndAuthCompleted(bool isSuccessful, string errorText)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.verifyAndAuthMessageAPI(string apiData, string xyzChallenge)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.verifyCompleted(bool isSuccessful, string errorText)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.verifyMessageAPI(string apiData)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.accountSummaryEnd(int reqId)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.accountUpdateMulti(int requestId, string account, string modelCode, string key, string value, string currency)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.accountSummary(int reqId, string account, string tag, string value, string currency)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.accountUpdateMultiEnd(int requestId)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.tickByTickAllLast(int reqId, int tickType, long time, double price, decimal size, TickAttribLast tickAttribLast, string exchange, string specialConditions)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.tickByTickMidPoint(int reqId, long time, double midPoint)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.tickSnapshotEnd(int tickerId)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, decimal size, bool isSmartDepth)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.position(string account, Contract contract, decimal pos, double avgCost)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.positionEnd()
        {
            throw new NotImplementedException();
        }
        void IEWrapper.positionMulti(int requestId, string account, string modelCode, Contract contract, decimal pos, double avgCost)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.positionMultiEnd(int requestId)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.orderBound(long orderId, int apiClientId, int apiOrderId)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.historicalNews(int requestId, string time, string providerCode, string articleId, string headline)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.historicalNewsEnd(int requestId, bool hasMore)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.newsArticle(int requestId, int articleType, string articleText)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.replaceFAEnd(int reqId, string text)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.wshMetaData(int reqId, string dataJson)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.wshEventData(int reqId, string dataJson)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.historicalSchedule(int reqId, string startDateTime, string endDateTime, string timeZone, HistoricalSession[] sessions)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.userInfo(int reqId, string whiteBrandingId)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.completedOrder(Contract contract, Order order, OrderState orderState)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.completedOrdersEnd()
        {
            throw new NotImplementedException();
        }
        void IEWrapper.scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.scannerDataEnd(int reqId)
        {
            throw new NotImplementedException();
        }
    }
}
