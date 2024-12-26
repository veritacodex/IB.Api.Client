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
            _ = reqId;
            _ = contract;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.currentTime(long time)
        {
            _ = time;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.deltaNeutralValidation(int reqId, DeltaNeutralContract deltaNeutralContract)
        {
            _ = reqId;
            _ = deltaNeutralContract;
            
            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.displayGroupList(int reqId, string groups)
        {
            _ = reqId;
            _ = groups;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.displayGroupUpdated(int reqId, string contractInfo)
        {
            _ = reqId;
            _ = contractInfo;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.familyCodes(FamilyCode[] familyCodes)
        {
            _ = familyCodes;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.fundamentalData(int reqId, string data)
        {
            _ = reqId;
            _ = data;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.headTimestamp(int reqId, string headTimestamp)
        {
            _ = reqId;
            _ = headTimestamp;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.histogramData(int reqId, HistogramEntry[] data)
        {
            _ = reqId;
            _ = data;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.mktDepthExchanges(DepthMktDataDescription[] depthMktDataDescriptions)
        {
            _= depthMktDataDescriptions;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.pnl(int reqId, double dailyPnL, double unrealizedPnL, double realizedPnL)
        {
            _ = reqId;
            _ = dailyPnL;
            _ = unrealizedPnL;
            _ = realizedPnL;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.pnlSingle(int reqId, decimal pos, double dailyPnL, double unrealizedPnL, double realizedPnL, double value)
        {
            _ = reqId; 
            _ = pos;
            _ = dailyPnL;
            _ = unrealizedPnL;
            _ = realizedPnL;
            _ = value;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.receiveFA(int faDataType, string faXmlData)
        {
            _ = faDataType;
            _ = faXmlData;
            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.rerouteMktDataReq(int reqId, int conId, string exchange)
        {
            _ = reqId;
            _ = conId;
            _ = exchange;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.rerouteMktDepthReq(int reqId, int conId, string exchange)
        {
            _ = reqId;
            _ = conId;
            _ = exchange;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.smartComponents(int reqId, Dictionary<int, KeyValuePair<string, char>> theMap)
        {
            _ = reqId;
            _ = theMap;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.softDollarTiers(int reqId, SoftDollarTier[] tiers)
        {
            _ = reqId;
            _ = tiers;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.symbolSamples(int reqId, ContractDescription[] contractDescriptions)
        {
            _ = reqId;
            _ = contractDescriptions;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.verifyAndAuthCompleted(bool isSuccessful, string errorText)
        {
            _ = isSuccessful;
            _ = errorText;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.verifyAndAuthMessageAPI(string apiData, string xyzChallenge)
        {
            _ = apiData;
            _ = xyzChallenge;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.verifyCompleted(bool isSuccessful, string errorText)
        {
            _ = isSuccessful;
            _ = errorText;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.verifyMessageAPI(string apiData)
        {
            _ = apiData;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.accountSummaryEnd(int reqId)
        {
            _ = reqId;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.accountUpdateMulti(int requestId, string account, string modelCode, string key, string value, string currency)
        {
            _= requestId;
            _ = account;
            _ = modelCode;
            _ = key;
            _ = value;
            _ = currency;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.accountSummary(int reqId, string account, string tag, string value, string currency)
        {
            _ = reqId;
            _ = account;
            _ = tag;
            _ = value;
            _ = currency;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.accountUpdateMultiEnd(int requestId)
        {
            _ = requestId;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.tickByTickAllLast(int reqId, int tickType, long time, double price, decimal size, TickAttribLast tickAttribLast, string exchange, string specialConditions)
        {
            _ = reqId;
            _ = tickType;
            _ = time;
            _ = price;
            _ = size;
            _ = tickAttribLast;
            _ = exchange;
            _ = specialConditions;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.tickByTickMidPoint(int reqId, long time, double midPoint)
        {
            _ = reqId;
            _ = time;
            _ = midPoint;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
        {
            _ = tickerId;
            _ = tickType;
            _ = basisPoints;
            _ = formattedBasisPoints;
            _ = impliedFuture;
            _ = holdDays;
            _ = futureLastTradeDate;
            _ = dividendImpact;
            _ = dividendsToLastTradeDate;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.tickSnapshotEnd(int tickerId)
        {
            _ = tickerId;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, decimal size, bool isSmartDepth)
        {
            _ = tickerId;
            _ = position;
            _ = marketMaker;
            _ = operation;
            _ = side;
            _ = price;
            _ = size;
            _ = isSmartDepth;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.position(string account, Contract contract, decimal pos, double avgCost)
        {
            _ = account;
            _ = contract;
            _ = pos;
            _ = avgCost;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.positionEnd()
        {
            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.positionMulti(int requestId, string account, string modelCode, Contract contract, decimal pos, double avgCost)
        {
            _ = requestId;
            _ = account;
            _ = modelCode;
            _ = contract;
            _ = pos;
            _ = avgCost;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.positionMultiEnd(int requestId)
        {
            _ = requestId;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.orderBound(long orderId, int apiClientId, int apiOrderId)
        {
            _ = orderId;
            _ = apiClientId;
            _ = apiOrderId;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.historicalNews(int requestId, string time, string providerCode, string articleId, string headline)
        {
            _ = requestId;
            _ = time;
            _ = providerCode;
            _ = articleId;
            _ = headline;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.historicalNewsEnd(int requestId, bool hasMore)
        {
            _ = requestId;
            _ = hasMore;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.newsArticle(int requestId, int articleType, string articleText)
        {
            _ = requestId;
            _ = articleType;
            _ = articleText;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.replaceFAEnd(int reqId, string text)
        {
            _ = reqId;
            _ = text;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.wshMetaData(int reqId, string dataJson)
        {
            _ = reqId;
            _ = dataJson;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.wshEventData(int reqId, string dataJson)
        {
            _ = reqId;
            _ = dataJson;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.historicalSchedule(int reqId, string startDateTime, string endDateTime, string timeZone, HistoricalSession[] sessions)
        {
            _ = reqId;
            _ = startDateTime;
            _ = endDateTime;
            _ = timeZone;
            _ = sessions;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.userInfo(int reqId, string whiteBrandingId)
        {
            _ = reqId;
            _ = whiteBrandingId;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.completedOrder(Contract contract, Order order, OrderState orderState)
        {
            _ = contract;
            _ = order;
            _ = orderState;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.completedOrdersEnd()
        {
            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
        {
            _ = reqId;
            _ = rank;
            _ = contractDetails;
            _ = distance;
            _ = benchmark;
            _ = projection;
            _ = legsStr;

            Notify("Not implemented");
            throw new NotImplementedException();
        }
        void IEWrapper.scannerDataEnd(int reqId)
        {
            _ = reqId;
            Notify("Not implemented");
            throw new NotImplementedException();
        }
    }
}
