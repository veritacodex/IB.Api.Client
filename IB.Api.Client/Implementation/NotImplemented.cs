using System;
using System.Collections.Generic;
using IB.Api.Client.IBKR;

namespace IB.Api.Client.Implementation
{
    //Not implemented
    public partial class IbClient
    {
        private static void DiscardImplementation(params object[] inputs)
        {
            _ = inputs;
        }
        void IEWrapper.bondContractDetails(int reqId, ContractDetails contract)
        {
            DiscardImplementation(reqId, contract);
            throw new NotImplementedException();
        }
        void IEWrapper.currentTime(long time)
        {
            DiscardImplementation(time);
            throw new NotImplementedException();
        }
        void IEWrapper.deltaNeutralValidation(int reqId, DeltaNeutralContract deltaNeutralContract)
        {
            DiscardImplementation(reqId, deltaNeutralContract);
            throw new NotImplementedException();
        }
        void IEWrapper.displayGroupList(int reqId, string groups)
        {
            DiscardImplementation(reqId, groups);
            throw new NotImplementedException();
        }
        void IEWrapper.displayGroupUpdated(int reqId, string contractInfo)
        {
            DiscardImplementation(reqId, contractInfo);
            throw new NotImplementedException();
        }
        void IEWrapper.familyCodes(FamilyCode[] familyCodes)
        {
            DiscardImplementation(familyCodes);
            throw new NotImplementedException();
        }
        void IEWrapper.fundamentalData(int reqId, string data)
        {
            DiscardImplementation(reqId, data);
            throw new NotImplementedException();
        }
        void IEWrapper.headTimestamp(int reqId, string headTimestamp)
        {
            DiscardImplementation(reqId, headTimestamp);
            throw new NotImplementedException();
        }
        void IEWrapper.histogramData(int reqId, HistogramEntry[] data)
        {
            DiscardImplementation(reqId, data);
            throw new NotImplementedException();
        }
        void IEWrapper.mktDepthExchanges(DepthMktDataDescription[] depthMktDataDescriptions)
        {
            DiscardImplementation(depthMktDataDescriptions);
            throw new NotImplementedException();
        }
        void IEWrapper.pnl(int reqId, double dailyPnL, double unrealizedPnL, double realizedPnL)
        {
            DiscardImplementation(reqId, dailyPnL, unrealizedPnL, realizedPnL);
            throw new NotImplementedException();
        }
        void IEWrapper.pnlSingle(int reqId, decimal pos, double dailyPnL, double unrealizedPnL, double realizedPnL, double value)
        {
            DiscardImplementation(reqId, pos, dailyPnL, unrealizedPnL, realizedPnL, value);
            throw new NotImplementedException();
        }
        void IEWrapper.receiveFa(int faDataType, string faXmlData)
        {
            DiscardImplementation(faDataType, faXmlData);
            throw new NotImplementedException();
        }
        void IEWrapper.rerouteMktDataReq(int reqId, int conId, string exchange)
        {
            DiscardImplementation(reqId, conId, exchange);
            throw new NotImplementedException();
        }
        void IEWrapper.rerouteMktDepthReq(int reqId, int conId, string exchange)
        {
            DiscardImplementation(conId, reqId, exchange);
            throw new NotImplementedException();
        }
        void IEWrapper.smartComponents(int reqId, Dictionary<int, KeyValuePair<string, char>> theMap)
        {
            DiscardImplementation(reqId, theMap);
            throw new NotImplementedException();
        }
        void IEWrapper.softDollarTiers(int reqId, SoftDollarTier[] tiers)
        {
            DiscardImplementation(reqId, tiers);
            throw new NotImplementedException();
        }
        void IEWrapper.symbolSamples(int reqId, ContractDescription[] contractDescriptions)
        {
            DiscardImplementation(reqId, contractDescriptions);
            throw new NotImplementedException();
        }
        void IEWrapper.verifyAndAuthCompleted(bool isSuccessful, string errorText)
        {
            DiscardImplementation(isSuccessful, errorText);
            throw new NotImplementedException();
        }
        void IEWrapper.verifyAndAuthMessageApi(string apiData, string xyzChallenge)
        {
            DiscardImplementation(apiData, xyzChallenge);
            throw new NotImplementedException();
        }
        void IEWrapper.verifyCompleted(bool isSuccessful, string errorText)
        {
            DiscardImplementation(errorText, isSuccessful);
            throw new NotImplementedException();
        }
        void IEWrapper.verifyMessageApi(string apiData)
        {
            DiscardImplementation(apiData);
            throw new NotImplementedException();
        }
        void IEWrapper.accountSummaryEnd(int reqId)
        {
            DiscardImplementation(reqId);
            throw new NotImplementedException();
        }
        void IEWrapper.accountUpdateMulti(int requestId, string account, string modelCode, string key, string value, string currency)
        {
            DiscardImplementation(requestId, account, modelCode, key, value, currency);
            throw new NotImplementedException();
        }
        void IEWrapper.accountSummary(int reqId, string account, string tag, string value, string currency)
        {
            DiscardImplementation(reqId, account, tag, value, currency);
            throw new NotImplementedException();
        }
        void IEWrapper.accountUpdateMultiEnd(int requestId)
        {
            DiscardImplementation(requestId);
            throw new NotImplementedException();
        }
        void IEWrapper.tickByTickAllLast(int reqId, int tickType, long time, double price, decimal size, TickAttribLast tickAttribLast, string exchange, string specialConditions)
        {
            DiscardImplementation(reqId, tickType, time, price, size, tickAttribLast, exchange, specialConditions);
            throw new NotImplementedException();
        }
        void IEWrapper.tickByTickMidPoint(int reqId, long time, double midPoint)
        {
            DiscardImplementation(reqId, time, midPoint);
            throw new NotImplementedException();
        }
        void IEWrapper.tickEfp(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
        {
            DiscardImplementation(tickerId, tickType, basisPoints, formattedBasisPoints, impliedFuture, holdDays, futureLastTradeDate, dividendImpact, dividendsToLastTradeDate);
            throw new NotImplementedException();
        }
        void IEWrapper.tickSnapshotEnd(int tickerId)
        {
            DiscardImplementation(tickerId);
            throw new NotImplementedException();
        }
        void IEWrapper.updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, decimal size, bool isSmartDepth)
        {
            DiscardImplementation(tickerId, position, marketMaker, operation, side, price, size, isSmartDepth);
            throw new NotImplementedException();
        }
        void IEWrapper.position(string account, Contract contract, decimal pos, double avgCost)
        {
            DiscardImplementation(account, contract, pos, avgCost);
            throw new NotImplementedException();
        }
        void IEWrapper.positionEnd()
        {
            DiscardImplementation();
            throw new NotImplementedException();
        }
        void IEWrapper.positionMulti(int requestId, string account, string modelCode, Contract contract, decimal pos, double avgCost)
        {
            DiscardImplementation(requestId, account, modelCode, contract, pos, avgCost);
            throw new NotImplementedException();
        }
        void IEWrapper.positionMultiEnd(int requestId)
        {
            DiscardImplementation(requestId, "");
            throw new NotImplementedException();
        }
        void IEWrapper.orderBound(long orderId, int apiClientId, int apiOrderId)
        {
            DiscardImplementation(orderId, apiClientId, apiOrderId);
            throw new NotImplementedException();
        }
        void IEWrapper.historicalNews(int requestId, string time, string providerCode, string articleId, string headline)
        {
            DiscardImplementation(requestId, time, providerCode, articleId, headline);
            throw new NotImplementedException();
        }
        void IEWrapper.historicalNewsEnd(int requestId, bool hasMore)
        {
            DiscardImplementation(requestId, hasMore);
            throw new NotImplementedException();
        }
        void IEWrapper.newsArticle(int requestId, int articleType, string articleText)
        {
            DiscardImplementation(requestId, articleType, articleText);
            throw new NotImplementedException();
        }
        void IEWrapper.replaceFaEnd(int reqId, string text)
        {
            DiscardImplementation(reqId, text);
            throw new NotImplementedException();
        }
        void IEWrapper.wshMetaData(int reqId, string dataJson)
        {
            DiscardImplementation(reqId, dataJson);
            throw new NotImplementedException();
        }
        void IEWrapper.wshEventData(int reqId, string dataJson)
        {
            DiscardImplementation(dataJson, reqId);
            throw new NotImplementedException();
        }
        void IEWrapper.historicalSchedule(int reqId, string startDateTime, string endDateTime, string timeZone, HistoricalSession[] sessions)
        {
            DiscardImplementation(reqId, startDateTime, endDateTime, timeZone, sessions);
            throw new NotImplementedException();
        }
        void IEWrapper.userInfo(int reqId, string whiteBrandingId)
        {
            DiscardImplementation(reqId, whiteBrandingId);
            throw new NotImplementedException();
        }
        void IEWrapper.scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
        {
            DiscardImplementation(reqId, rank, contractDetails, distance, benchmark, projection, legsStr);
            throw new NotImplementedException();
        }
        void IEWrapper.scannerDataEnd(int reqId)
        {
            DiscardImplementation(reqId, "", "");
            throw new NotImplementedException();
        }
    }
}
