using System;
using System.Collections.Generic;
using IBApi;

namespace IB.Api.Client.Implementation
{
    //Not implemented
    public partial class IBClient
    {
        void EWrapper.bondContractDetails(int reqId, ContractDetails contract)
        {
            throw new NotImplementedException();
        }
        void EWrapper.currentTime(long time)
        {
            throw new NotImplementedException();
        }
        void EWrapper.deltaNeutralValidation(int reqId, DeltaNeutralContract deltaNeutralContract)
        {
            throw new NotImplementedException();
        }
        void EWrapper.displayGroupList(int reqId, string groups)
        {
            throw new NotImplementedException();
        }
        void EWrapper.displayGroupUpdated(int reqId, string contractInfo)
        {
            throw new NotImplementedException();
        }
        void EWrapper.familyCodes(FamilyCode[] familyCodes)
        {
            throw new NotImplementedException();
        }
        void EWrapper.fundamentalData(int reqId, string data)
        {
            throw new NotImplementedException();
        }
        void EWrapper.headTimestamp(int reqId, string headTimestamp)
        {
            throw new NotImplementedException();
        }
        void EWrapper.histogramData(int reqId, HistogramEntry[] data)
        {
            throw new NotImplementedException();
        }
        void EWrapper.mktDepthExchanges(DepthMktDataDescription[] depthMktDataDescriptions)
        {
            throw new NotImplementedException();
        }
        void EWrapper.pnl(int reqId, double dailyPnL, double unrealizedPnL, double realizedPnL)
        {
            throw new NotImplementedException();
        }
        void EWrapper.pnlSingle(int reqId, decimal pos, double dailyPnL, double unrealizedPnL, double realizedPnL, double value)
        {
            throw new NotImplementedException();
        }
        void EWrapper.receiveFA(int faDataType, string faXmlData)
        {
            throw new NotImplementedException();
        }
        void EWrapper.rerouteMktDataReq(int reqId, int conId, string exchange)
        {
            throw new NotImplementedException();
        }
        void EWrapper.rerouteMktDepthReq(int reqId, int conId, string exchange)
        {
            throw new NotImplementedException();
        }
        void EWrapper.smartComponents(int reqId, Dictionary<int, KeyValuePair<string, char>> theMap)
        {
            throw new NotImplementedException();
        }
        void EWrapper.softDollarTiers(int reqId, SoftDollarTier[] tiers)
        {
            throw new NotImplementedException();
        }
        void EWrapper.symbolSamples(int reqId, ContractDescription[] contractDescriptions)
        {
            throw new NotImplementedException();
        }
        void EWrapper.verifyAndAuthCompleted(bool isSuccessful, string errorText)
        {
            throw new NotImplementedException();
        }
        void EWrapper.verifyAndAuthMessageAPI(string apiData, string xyzChallenge)
        {
            throw new NotImplementedException();
        }
        void EWrapper.verifyCompleted(bool isSuccessful, string errorText)
        {
            throw new NotImplementedException();
        }
        void EWrapper.verifyMessageAPI(string apiData)
        {
            throw new NotImplementedException();
        }
        void EWrapper.accountSummaryEnd(int reqId)
        {
            throw new NotImplementedException();
        }
        void EWrapper.accountUpdateMulti(int requestId, string account, string modelCode, string key, string value, string currency)
        {
            throw new NotImplementedException();
        }
        void EWrapper.accountSummary(int reqId, string account, string tag, string value, string currency)
        {
            throw new NotImplementedException();
        }
        void EWrapper.accountUpdateMultiEnd(int requestId)
        {
            throw new NotImplementedException();
        }
        void EWrapper.tickByTickAllLast(int reqId, int tickType, long time, double price, decimal size, TickAttribLast tickAttribLast, string exchange, string specialConditions)
        {
            throw new NotImplementedException();
        }
        void EWrapper.tickByTickMidPoint(int reqId, long time, double midPoint)
        {
            throw new NotImplementedException();
        }
        void EWrapper.tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
        {
            throw new NotImplementedException();
        }
        void EWrapper.tickSnapshotEnd(int tickerId)
        {
            throw new NotImplementedException();
        }
        void EWrapper.updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, decimal size, bool isSmartDepth)
        {
            throw new NotImplementedException();
        }
        void EWrapper.position(string account, Contract contract, decimal pos, double avgCost)
        {
            throw new NotImplementedException();
        }
        void EWrapper.positionEnd()
        {
            throw new NotImplementedException();
        }
        void EWrapper.positionMulti(int requestId, string account, string modelCode, Contract contract, decimal pos, double avgCost)
        {
            throw new NotImplementedException();
        }
        void EWrapper.positionMultiEnd(int requestId)
        {
            throw new NotImplementedException();
        }
        void EWrapper.orderBound(long orderId, int apiClientId, int apiOrderId)
        {
            throw new NotImplementedException();
        }
        void EWrapper.historicalNews(int requestId, string time, string providerCode, string articleId, string headline)
        {
            throw new NotImplementedException();
        }
        void EWrapper.historicalNewsEnd(int requestId, bool hasMore)
        {
            throw new NotImplementedException();
        }
        void EWrapper.newsArticle(int requestId, int articleType, string articleText)
        {
            throw new NotImplementedException();
        }
        void EWrapper.replaceFAEnd(int reqId, string text)
        {
            throw new NotImplementedException();
        }
        void EWrapper.wshMetaData(int reqId, string dataJson)
        {
            throw new NotImplementedException();
        }
        void EWrapper.wshEventData(int reqId, string dataJson)
        {
            throw new NotImplementedException();
        }
        void EWrapper.historicalSchedule(int reqId, string startDateTime, string endDateTime, string timeZone, HistoricalSession[] sessions)
        {
            throw new NotImplementedException();
        }
        void EWrapper.userInfo(int reqId, string whiteBrandingId)
        {
            throw new NotImplementedException();
        }
        void EWrapper.completedOrder(Contract contract, Order order, OrderState orderState)
        {
            throw new NotImplementedException();
        }
        void EWrapper.completedOrdersEnd()
        {
            throw new NotImplementedException();
        }
        void EWrapper.scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
        {
            throw new NotImplementedException();
        }
        void EWrapper.scannerDataEnd(int reqId)
        {
            throw new NotImplementedException();
        }
    }
}
