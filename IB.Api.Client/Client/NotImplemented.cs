using System;
using System.Collections.Generic;
using IBApi;

namespace IB.Api.Client
{
    //Not implemented
    public partial class IBClient
    {
        public void BondContractDetails(int reqId, ContractDetails contract)
        {
            throw new NotImplementedException();
        }
        public void CurrentTime(long time)
        {
            throw new NotImplementedException();
        }
        public void DeltaNeutralValidation(int reqId, DeltaNeutralContract deltaNeutralContract)
        {
            throw new NotImplementedException();
        }
        public void DisplayGroupList(int reqId, string groups)
        {
            throw new NotImplementedException();
        }
        public void DisplayGroupUpdated(int reqId, string contractInfo)
        {
            throw new NotImplementedException();
        }
        public void FamilyCodes(FamilyCode[] familyCodes)
        {
            throw new NotImplementedException();
        }
        public void FundamentalData(int reqId, string data)
        {
            throw new NotImplementedException();
        }
        public void HeadTimestamp(int reqId, string headTimestamp)
        {
            throw new NotImplementedException();
        }
        public void HistogramData(int reqId, HistogramEntry[] data)
        {
            throw new NotImplementedException();
        }
        public void MarketRule(int marketRuleId, PriceIncrement[] priceIncrements)
        {
            throw new NotImplementedException();
        }
        public void MktDepthExchanges(DepthMktDataDescription[] depthMktDataDescriptions)
        {
            throw new NotImplementedException();
        }
        public void Pnl(int reqId, double dailyPnL, double unrealizedPnL, double realizedPnL)
        {
            throw new NotImplementedException();
        }
        public void PnlSingle(int reqId, decimal pos, double dailyPnL, double unrealizedPnL, double realizedPnL, double value)
        {
            throw new NotImplementedException();
        }
        public void ReceiveFA(int faDataType, string faXmlData)
        {
            throw new NotImplementedException();
        }
        public void RerouteMktDataReq(int reqId, int conId, string exchange)
        {
            throw new NotImplementedException();
        }
        public void RerouteMktDepthReq(int reqId, int conId, string exchange)
        {
            throw new NotImplementedException();
        }
        public void ScannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
        {
            throw new NotImplementedException();
        }
        public void ScannerDataEnd(int reqId)
        {
            throw new NotImplementedException();
        }
        public void ScannerParameters(string xml)
        {
            throw new NotImplementedException();
        }
        public void SmartComponents(int reqId, Dictionary<int, KeyValuePair<string, char>> theMap)
        {
            throw new NotImplementedException();
        }
        public void SoftDollarTiers(int reqId, SoftDollarTier[] tiers)
        {
            throw new NotImplementedException();
        }
        public void SymbolSamples(int reqId, ContractDescription[] contractDescriptions)
        {
            throw new NotImplementedException();
        }
        public void VerifyAndAuthCompleted(bool isSuccessful, string errorText)
        {
            throw new NotImplementedException();
        }
        public void VerifyAndAuthMessageAPI(string apiData, string xyzChallenge)
        {
            throw new NotImplementedException();
        }
        public void VerifyCompleted(bool isSuccessful, string errorText)
        {
            throw new NotImplementedException();
        }
        public void VerifyMessageAPI(string apiData)
        {
            throw new NotImplementedException();
        }
        public void AccountSummaryEnd(int reqId)
        {
            throw new NotImplementedException();
        }
        public void AccountUpdateMulti(int requestId, string account, string modelCode, string key, string value, string currency)
        {
            throw new NotImplementedException();
        }
        public void AccountSummary(int reqId, string account, string tag, string value, string currency)
        {
            throw new NotImplementedException();
        }
        public void AccountUpdateMultiEnd(int requestId)
        {
            throw new NotImplementedException();
        }
        public void TickByTickAllLast(int reqId, int tickType, long time, double price, decimal size, TickAttribLast tickAttribLast, string exchange, string specialConditions)
        {
            throw new NotImplementedException();
        }
        public void TickByTickMidPoint(int reqId, long time, double midPoint)
        {
            throw new NotImplementedException();
        }
        public void TickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
        {
            throw new NotImplementedException();
        }


        public void TickSnapshotEnd(int tickerId)
        {
            throw new NotImplementedException();
        }
        public void UpdateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, decimal size, bool isSmartDepth)
        {
            throw new NotImplementedException();
        }
        public void Position(string account, Contract contract, decimal pos, double avgCost)
        {
            throw new NotImplementedException();
        }
        public void PositionEnd()
        {
            throw new NotImplementedException();
        }
        public void PositionMulti(int requestId, string account, string modelCode, Contract contract, decimal pos, double avgCost)
        {
            throw new NotImplementedException();
        }
        public void PositionMultiEnd(int requestId)
        {
            throw new NotImplementedException();
        }
        public void OrderBound(long orderId, int apiClientId, int apiOrderId)
        {
            throw new NotImplementedException();
        }
        public void HistoricalNews(int requestId, string time, string providerCode, string articleId, string headline)
        {
            throw new NotImplementedException();
        }
        public void HistoricalNewsEnd(int requestId, bool hasMore)
        {
            throw new NotImplementedException();
        }
        public void NewsArticle(int requestId, int articleType, string articleText)
        {
            throw new NotImplementedException();
        }
        public void ReplaceFAEnd(int reqId, string text)
        {
            throw new NotImplementedException();
        }
        public void WshMetaData(int reqId, string dataJson)
        {
            throw new NotImplementedException();
        }
        public void WshEventData(int reqId, string dataJson)
        {
            throw new NotImplementedException();
        }
        public void HistoricalSchedule(int reqId, string startDateTime, string endDateTime, string timeZone, HistoricalSession[] sessions)
        {
            throw new NotImplementedException();
        }
        public void UserInfo(int reqId, string whiteBrandingId)
        {
            throw new NotImplementedException();
        }        
    }
}
