using System;
using System.Collections.Generic;
using IBApi;

namespace IB.Api.Client
{
    //Not implemented
    public partial class IBClient
    {
        void IEWrapper.BondContractDetails(int reqId, ContractDetails contract)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.CurrentTime(long time)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.DeltaNeutralValidation(int reqId, DeltaNeutralContract deltaNeutralContract)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.DisplayGroupList(int reqId, string groups)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.DisplayGroupUpdated(int reqId, string contractInfo)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.FamilyCodes(FamilyCode[] familyCodes)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.FundamentalData(int reqId, string data)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.HeadTimestamp(int reqId, string headTimestamp)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.HistogramData(int reqId, HistogramEntry[] data)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.MktDepthExchanges(DepthMktDataDescription[] depthMktDataDescriptions)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.Pnl(int reqId, double dailyPnL, double unrealizedPnL, double realizedPnL)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.PnlSingle(int reqId, decimal pos, double dailyPnL, double unrealizedPnL, double realizedPnL, double value)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.ReceiveFA(int faDataType, string faXmlData)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.RerouteMktDataReq(int reqId, int conId, string exchange)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.RerouteMktDepthReq(int reqId, int conId, string exchange)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.SmartComponents(int reqId, Dictionary<int, KeyValuePair<string, char>> theMap)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.SoftDollarTiers(int reqId, SoftDollarTier[] tiers)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.SymbolSamples(int reqId, ContractDescription[] contractDescriptions)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.VerifyAndAuthCompleted(bool isSuccessful, string errorText)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.VerifyAndAuthMessageAPI(string apiData, string xyzChallenge)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.VerifyCompleted(bool isSuccessful, string errorText)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.VerifyMessageAPI(string apiData)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.AccountSummaryEnd(int reqId)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.AccountUpdateMulti(int requestId, string account, string modelCode, string key, string value, string currency)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.AccountSummary(int reqId, string account, string tag, string value, string currency)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.AccountUpdateMultiEnd(int requestId)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.TickByTickAllLast(int reqId, int tickType, long time, double price, decimal size, TickAttribLast tickAttribLast, string exchange, string specialConditions)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.TickByTickMidPoint(int reqId, long time, double midPoint)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.TickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.TickSnapshotEnd(int tickerId)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.UpdateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, decimal size, bool isSmartDepth)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.Position(string account, Contract contract, decimal pos, double avgCost)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.PositionEnd()
        {
            throw new NotImplementedException();
        }
        void IEWrapper.PositionMulti(int requestId, string account, string modelCode, Contract contract, decimal pos, double avgCost)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.PositionMultiEnd(int requestId)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.OrderBound(long orderId, int apiClientId, int apiOrderId)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.HistoricalNews(int requestId, string time, string providerCode, string articleId, string headline)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.HistoricalNewsEnd(int requestId, bool hasMore)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.NewsArticle(int requestId, int articleType, string articleText)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.ReplaceFAEnd(int reqId, string text)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.WshMetaData(int reqId, string dataJson)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.WshEventData(int reqId, string dataJson)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.HistoricalSchedule(int reqId, string startDateTime, string endDateTime, string timeZone, HistoricalSession[] sessions)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.UserInfo(int reqId, string whiteBrandingId)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.CompletedOrder(Contract contract, Order order, OrderState orderState)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.CompletedOrdersEnd()
        {
            throw new NotImplementedException();
        }
        void IEWrapper.ScannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
        {
            throw new NotImplementedException();
        }
        void IEWrapper.ScannerDataEnd(int reqId)
        {
            throw new NotImplementedException();
        }
    }
}
