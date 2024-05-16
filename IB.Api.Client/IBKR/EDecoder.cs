/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace IBApi
{
    class EDecoder(int serverVersion, IEWrapper callback, IEClientMsgSink sink = null) : IDecoder
    {
        private readonly IEClientMsgSink eClientMsgSink = sink;
        private readonly IEWrapper eWrapper = callback;
        private int serverVersion = serverVersion;
        private BinaryReader dataReader;
        private int nDecodedLen;

        public int ParseAndProcessMsg(byte[] buf)
        {
            dataReader?.Dispose();

            dataReader = new BinaryReader(new MemoryStream(buf));
            nDecodedLen = 0;

            if (serverVersion == 0)
            {
                ProcessConnectAck();

                return nDecodedLen;
            }

            return ProcessIncomingMessage(ReadInt()) ? nDecodedLen : -1;
        }

        private void ProcessConnectAck()
        {
            serverVersion = ReadInt();

            if (serverVersion == -1)
            {
                var srv = ReadString();

                serverVersion = 0;

                eClientMsgSink?.Redirect(srv);

                return;
            }

            var serverTime = "";

            if (serverVersion >= 20)
            {
                serverTime = ReadString();
            }

            eClientMsgSink?.ServerVersion(serverVersion, serverTime);

            eWrapper.ConnectAck();
        }

        private bool ProcessIncomingMessage(int incomingMessage)
        {
            if (incomingMessage == IncomingMessage.NotValid)
                return false;

            switch (incomingMessage)
            {
                case IncomingMessage.TickPrice:
                    TickPriceEvent();
                    break;

                case IncomingMessage.TickSize:
                    TickSizeEvent();
                    break;

                case IncomingMessage.Tickstring:
                    TickStringEvent();
                    break;

                case IncomingMessage.TickGeneric:
                    TickGenericEvent();
                    break;

                case IncomingMessage.TickEFP:
                    TickEFPEvent();
                    break;

                case IncomingMessage.TickSnapshotEnd:
                    TickSnapshotEndEvent();
                    break;

                case IncomingMessage.Error:
                    ErrorEvent();
                    break;

                case IncomingMessage.CurrentTime:
                    CurrentTimeEvent();
                    break;

                case IncomingMessage.ManagedAccounts:
                    ManagedAccountsEvent();
                    break;

                case IncomingMessage.NextValidId:
                    NextValidIdEvent();
                    break;

                case IncomingMessage.DeltaNeutralValidation:
                    DeltaNeutralValidationEvent();
                    break;

                case IncomingMessage.TickOptionComputation:
                    TickOptionComputationEvent();
                    break;

                case IncomingMessage.AccountSummary:
                    AccountSummaryEvent();
                    break;

                case IncomingMessage.AccountSummaryEnd:
                    AccountSummaryEndEvent();
                    break;

                case IncomingMessage.AccountValue:
                    AccountValueEvent();
                    break;

                case IncomingMessage.PortfolioValue:
                    PortfolioValueEvent();
                    break;

                case IncomingMessage.AccountUpdateTime:
                    AccountUpdateTimeEvent();
                    break;

                case IncomingMessage.AccountDownloadEnd:
                    AccountDownloadEndEvent();
                    break;

                case IncomingMessage.OrderStatus:
                    OrderStatusEvent();
                    break;

                case IncomingMessage.OpenOrder:
                    OpenOrderEvent();
                    break;

                case IncomingMessage.OpenOrderEnd:
                    OpenOrderEndEvent();
                    break;

                case IncomingMessage.ContractData:
                    ContractDataEvent();
                    break;

                case IncomingMessage.ContractDataEnd:
                    ContractDataEndEvent();
                    break;

                case IncomingMessage.ExecutionData:
                    ExecutionDataEvent();
                    break;

                case IncomingMessage.ExecutionDataEnd:
                    ExecutionDataEndEvent();
                    break;

                case IncomingMessage.CommissionsReport:
                    CommissionReportEvent();
                    break;

                case IncomingMessage.FundamentalData:
                    FundamentalDataEvent();
                    break;

                case IncomingMessage.HistoricalData:
                    HistoricalDataEvent();
                    break;

                case IncomingMessage.MarketDataType:
                    MarketDataTypeEvent();
                    break;

                case IncomingMessage.MarketDepth:
                    MarketDepthEvent();
                    break;

                case IncomingMessage.MarketDepthL2:
                    MarketDepthL2Event();
                    break;

                case IncomingMessage.NewsBulletins:
                    NewsBulletinsEvent();
                    break;

                case IncomingMessage.Position:
                    PositionEvent();
                    break;

                case IncomingMessage.PositionEnd:
                    PositionEndEvent();
                    break;

                case IncomingMessage.RealTimeBars:
                    RealTimeBarsEvent();
                    break;

                case IncomingMessage.ScannerParameters:
                    ScannerParametersEvent();
                    break;

                case IncomingMessage.ScannerData:
                    ScannerDataEvent();
                    break;

                case IncomingMessage.ReceiveFA:
                    ReceiveFAEvent();
                    break;

                case IncomingMessage.BondContractData:
                    BondContractDetailsEvent();
                    break;

                case IncomingMessage.VerifyMessageApi:
                    VerifyMessageApiEvent();
                    break;

                case IncomingMessage.VerifyCompleted:
                    VerifyCompletedEvent();
                    break;

                case IncomingMessage.DisplayGroupList:
                    DisplayGroupListEvent();
                    break;

                case IncomingMessage.DisplayGroupUpdated:
                    DisplayGroupUpdatedEvent();
                    break;

                case IncomingMessage.VerifyAndAuthMessageApi:
                    VerifyAndAuthMessageApiEvent();
                    break;

                case IncomingMessage.VerifyAndAuthCompleted:
                    VerifyAndAuthCompletedEvent();
                    break;

                case IncomingMessage.PositionMulti:
                    PositionMultiEvent();
                    break;

                case IncomingMessage.PositionMultiEnd:
                    PositionMultiEndEvent();
                    break;

                case IncomingMessage.AccountUpdateMulti:
                    AccountUpdateMultiEvent();
                    break;

                case IncomingMessage.AccountUpdateMultiEnd:
                    AccountUpdateMultiEndEvent();
                    break;

                case IncomingMessage.SecurityDefinitionOptionParameter:
                    SecurityDefinitionOptionParameterEvent();
                    break;

                case IncomingMessage.SecurityDefinitionOptionParameterEnd:
                    SecurityDefinitionOptionParameterEndEvent();
                    break;

                case IncomingMessage.SoftDollarTier:
                    SoftDollarTierEvent();
                    break;

                case IncomingMessage.FamilyCodes:
                    FamilyCodesEvent();
                    break;

                case IncomingMessage.SymbolSamples:
                    SymbolSamplesEvent();
                    break;

                case IncomingMessage.MktDepthExchanges:
                    MktDepthExchangesEvent();
                    break;

                case IncomingMessage.TickNews:
                    TickNewsEvent();
                    break;

                case IncomingMessage.TickReqParams:
                    TickReqParamsEvent();
                    break;

                case IncomingMessage.SmartComponents:
                    SmartComponentsEvent();
                    break;

                case IncomingMessage.NewsProviders:
                    NewsProvidersEvent();
                    break;

                case IncomingMessage.NewsArticle:
                    NewsArticleEvent();
                    break;

                case IncomingMessage.HistoricalNews:
                    HistoricalNewsEvent();
                    break;

                case IncomingMessage.HistoricalNewsEnd:
                    HistoricalNewsEndEvent();
                    break;

                case IncomingMessage.HeadTimestamp:
                    HeadTimestampEvent();
                    break;

                case IncomingMessage.HistogramData:
                    HistogramDataEvent();
                    break;

                case IncomingMessage.HistoricalDataUpdate:
                    HistoricalDataUpdateEvent();
                    break;

                case IncomingMessage.RerouteMktDataReq:
                    RerouteMktDataReqEvent();
                    break;

                case IncomingMessage.RerouteMktDepthReq:
                    RerouteMktDepthReqEvent();
                    break;

                case IncomingMessage.MarketRule:
                    MarketRuleEvent();
                    break;

                case IncomingMessage.PnL:
                    PnLEvent();
                    break;

                case IncomingMessage.PnLSingle:
                    PnLSingleEvent();
                    break;

                case IncomingMessage.HistoricalTick:
                    HistoricalTickEvent();
                    break;

                case IncomingMessage.HistoricalTickBidAsk:
                    HistoricalTickBidAskEvent();
                    break;

                case IncomingMessage.HistoricalTickLast:
                    HistoricalTickLastEvent();
                    break;

                case IncomingMessage.TickByTick:
                    TickByTickEvent();
                    break;

                case IncomingMessage.OrderBound:
                    OrderBoundEvent();
                    break;

                case IncomingMessage.CompletedOrder:
                    CompletedOrderEvent();
                    break;

                case IncomingMessage.CompletedOrdersEnd:
                    CompletedOrdersEndEvent();
                    break;

                case IncomingMessage.ReplaceFAEnd:
                    ReplaceFAEndEvent();
                    break;

                case IncomingMessage.WshMetaData:
                    ProcessWshMetaData();
                    break;

                case IncomingMessage.WshEventData:
                    ProcessWshEventData();
                    break;

                case IncomingMessage.HistoricalSchedule:
                    ProcessHistoricalScheduleEvent();
                    break;

                case IncomingMessage.UserInfo:
                    ProcessUserInfoEvent();
                    break;

                default:
                    eWrapper.Error(IncomingMessage.NotValid, EClientErrors.UNKNOWN_ID.Code, EClientErrors.UNKNOWN_ID.Message, "");
                    return false;
            }

            return true;
        }

        private void CompletedOrderEvent()
        {
            Contract contract = new Contract();
            Order order = new Order();
            OrderState orderState = new OrderState();
            EOrderDecoder eOrderDecoder = new EOrderDecoder(this, contract, order, orderState, int.MaxValue, serverVersion);

            // read contract fields
            eOrderDecoder.ReadContractFields();

            // read order fields
            eOrderDecoder.ReadAction();
            eOrderDecoder.ReadTotalQuantity();
            eOrderDecoder.ReadOrderType();
            eOrderDecoder.ReadLmtPrice();
            eOrderDecoder.ReadAuxPrice();
            eOrderDecoder.ReadTIF();
            eOrderDecoder.ReadOcaGroup();
            eOrderDecoder.ReadAccount();
            eOrderDecoder.ReadOpenClose();
            eOrderDecoder.ReadOrigin();
            eOrderDecoder.ReadOrderRef();
            eOrderDecoder.ReadPermId();
            eOrderDecoder.ReadOutsideRth();
            eOrderDecoder.ReadHidden();
            eOrderDecoder.ReadDiscretionaryAmount();
            eOrderDecoder.ReadGoodAfterTime();
            eOrderDecoder.ReadFAParams();
            eOrderDecoder.ReadModelCode();
            eOrderDecoder.ReadGoodTillDate();
            eOrderDecoder.ReadRule80A();
            eOrderDecoder.ReadPercentOffset();
            eOrderDecoder.ReadSettlingFirm();
            eOrderDecoder.ReadShortSaleParams();
            eOrderDecoder.ReadBoxOrderParams();
            eOrderDecoder.ReadPegToStkOrVolOrderParams();
            eOrderDecoder.ReadDisplaySize();
            eOrderDecoder.ReadSweepToFill();
            eOrderDecoder.ReadAllOrNone();
            eOrderDecoder.ReadMinQty();
            eOrderDecoder.ReadOcaType();
            eOrderDecoder.ReadTriggerMethod();
            eOrderDecoder.ReadVolOrderParams(false);
            eOrderDecoder.ReadTrailParams();
            eOrderDecoder.ReadComboLegs();
            eOrderDecoder.ReadSmartComboRoutingParams();
            eOrderDecoder.ReadScaleOrderParams();
            eOrderDecoder.ReadHedgeParams();
            eOrderDecoder.ReadClearingParams();
            eOrderDecoder.ReadNotHeld();
            eOrderDecoder.ReadDeltaNeutral();
            eOrderDecoder.ReadAlgoParams();
            eOrderDecoder.ReadSolicited();
            eOrderDecoder.ReadOrderStatus();
            eOrderDecoder.ReadVolRandomizeFlags();
            eOrderDecoder.ReadPegToBenchParams();
            eOrderDecoder.ReadConditions();
            eOrderDecoder.ReadStopPriceAndLmtPriceOffset();
            eOrderDecoder.ReadCashQty();
            eOrderDecoder.ReadDontUseAutoPriceForHedge();
            eOrderDecoder.ReadIsOmsContainer();
            eOrderDecoder.ReadAutoCancelDate();
            eOrderDecoder.ReadFilledQuantity();
            eOrderDecoder.ReadRefFuturesConId();
            eOrderDecoder.ReadAutoCancelParent();
            eOrderDecoder.ReadShareholder();
            eOrderDecoder.ReadImbalanceOnly();
            eOrderDecoder.ReadRouteMarketableToBbo();
            eOrderDecoder.ReadParentPermId();
            eOrderDecoder.ReadCompletedTime();
            eOrderDecoder.ReadCompletedStatus();
            eOrderDecoder.ReadPegBestPegMidOrderAttributes();

            eWrapper.CompletedOrder(contract, order, orderState);
        }

        private void CompletedOrdersEndEvent()
        {
            eWrapper.CompletedOrdersEnd();
        }

        private void OrderBoundEvent()
        {
            long orderId = ReadLong();
            int apiClientId = ReadInt();
            int apiOrderId = ReadInt();

            eWrapper.OrderBound(orderId, apiClientId, apiOrderId);
        }

        private void TickByTickEvent()
        {
            int reqId = ReadInt();
            int tickType = ReadInt();
            long time = ReadLong();
            BitMask mask;

            switch (tickType)
            {
                case 0: // None
                    break;
                case 1: // Last
                case 2: // AllLast
                    double price = ReadDouble();
                    decimal size = ReadDecimal();
                    mask = new BitMask(ReadInt());
                    TickAttribLast tickAttribLast = new TickAttribLast
                    {
                        PastLimit = mask[0],
                        Unreported = mask[1]
                    };
                    string exchange = ReadString();
                    string specialConditions = ReadString();
                    eWrapper.TickByTickAllLast(reqId, tickType, time, price, size, tickAttribLast, exchange, specialConditions);
                    break;
                case 3: // BidAsk
                    double bidPrice = ReadDouble();
                    double askPrice = ReadDouble();
                    decimal bidSize = ReadDecimal();
                    decimal askSize = ReadDecimal();
                    mask = new BitMask(ReadInt());
                    TickAttribBidAsk tickAttribBidAsk = new TickAttribBidAsk
                    {
                        BidPastLow = mask[0],
                        AskPastHigh = mask[1]
                    };
                    eWrapper.TickByTickBidAsk(reqId, time, bidPrice, askPrice, bidSize, askSize, tickAttribBidAsk);
                    break;
                case 4: // MidPoint
                    double midPoint = ReadDouble();
                    eWrapper.TickByTickMidPoint(reqId, time, midPoint);
                    break;
            }
        }

        private void HistoricalTickLastEvent()
        {
            int reqId = ReadInt();
            int nTicks = ReadInt();
            HistoricalTickLast[] ticks = new HistoricalTickLast[nTicks];

            for (int i = 0; i < nTicks; i++)
            {
                var time = ReadLong();
                BitMask mask = new BitMask(ReadInt());
                TickAttribLast tickAttribLast = new TickAttribLast
                {
                    PastLimit = mask[0],
                    Unreported = mask[1]
                };
                var price = ReadDouble();
                var size = ReadDecimal();
                var exchange = ReadString();
                var specialConditions = ReadString();

                ticks[i] = new HistoricalTickLast(time, tickAttribLast, price, size, exchange, specialConditions);
            }

            bool done = ReadBoolFromInt();

            eWrapper.HistoricalTicksLast(reqId, ticks, done);
        }

        private void HistoricalTickBidAskEvent()
        {
            int reqId = ReadInt();
            int nTicks = ReadInt();
            HistoricalTickBidAsk[] ticks = new HistoricalTickBidAsk[nTicks];

            for (int i = 0; i < nTicks; i++)
            {
                var time = ReadLong();
                BitMask mask = new BitMask(ReadInt());
                TickAttribBidAsk tickAttribBidAsk = new TickAttribBidAsk
                {
                    AskPastHigh = mask[0],
                    BidPastLow = mask[1]
                };
                var priceBid = ReadDouble();
                var priceAsk = ReadDouble();
                var sizeBid = ReadDecimal();
                var sizeAsk = ReadDecimal();

                ticks[i] = new HistoricalTickBidAsk
                {
                    Time = time,
                    PriceAsk = priceAsk,
                    PriceBid = priceBid,
                    SizeAsk = sizeAsk,
                    SizeBid = sizeBid,
                    TickAttribBidAsk = tickAttribBidAsk
                };
            }

            bool done = ReadBoolFromInt();

            eWrapper.HistoricalTicksBidAsk(reqId, ticks, done);
        }

        private void HistoricalTickEvent()
        {
            int reqId = ReadInt();
            int nTicks = ReadInt();
            HistoricalTick[] ticks = new HistoricalTick[nTicks];

            for (int i = 0; i < nTicks; i++)
            {
                var time = ReadLong();
                ReadInt();// for consistency
                var price = ReadDouble();
                var size = ReadDecimal();

                ticks[i] = new HistoricalTick(time, price, size);
            }

            bool done = ReadBoolFromInt();

            eWrapper.HistoricalTicks(reqId, ticks, done);
        }

        private void MarketRuleEvent()
        {
            int marketRuleId = ReadInt();
            PriceIncrement[] priceIncrements = new PriceIncrement[0];
            int nPriceIncrements = ReadInt();

            if (nPriceIncrements > 0)
            {
                Array.Resize(ref priceIncrements, nPriceIncrements);

                for (int i = 0; i < nPriceIncrements; ++i)
                {
                    priceIncrements[i] = new PriceIncrement(ReadDouble(), ReadDouble());
                }
            }

            eWrapper.MarketRule(marketRuleId, priceIncrements);
        }

        private void RerouteMktDepthReqEvent()
        {
            var reqId = ReadInt();
            var conId = ReadInt();
            string exchange = ReadString();

            eWrapper.RerouteMktDepthReq(reqId, conId, exchange);
        }

        private void RerouteMktDataReqEvent()
        {
            var reqId = ReadInt();
            var conId = ReadInt();
            string exchange = ReadString();

            eWrapper.RerouteMktDataReq(reqId, conId, exchange);
        }

        private void HistoricalDataUpdateEvent()
        {
            int requestId = ReadInt();
            int barCount = ReadInt();
            string date = ReadString();
            double open = ReadDouble();
            double close = ReadDouble();
            double high = ReadDouble();
            double low = ReadDouble();
            decimal WAP = ReadDecimal();
            decimal volume = ReadDecimal();

            var bar = new Bar
            {
                Time = date,
                Open = open,
                High = high,
                Low = low,
                Close = close,
                Volume = volume,
                Count = barCount,
                WAP = WAP
            };
            eWrapper.HistoricalDataUpdate(requestId, bar);
        }


        private void PnLSingleEvent()
        {
            int reqId = ReadInt();
            decimal pos = ReadDecimal();
            double dailyPnL = ReadDouble();
            double unrealizedPnL = double.MaxValue;
            double realizedPnL = double.MaxValue;

            if (serverVersion >= MinServerVer.UNREALIZED_PNL)
            {
                unrealizedPnL = ReadDouble();
            }

            if (serverVersion >= MinServerVer.REALIZED_PNL)
            {
                realizedPnL = ReadDouble();
            }

            double value = ReadDouble();

            eWrapper.PnlSingle(reqId, pos, dailyPnL, unrealizedPnL, realizedPnL, value);
        }

        private void PnLEvent()
        {
            int reqId = ReadInt();
            double dailyPnL = ReadDouble();
            double unrealizedPnL = double.MaxValue;
            double realizedPnL = double.MaxValue;

            if (serverVersion >= MinServerVer.UNREALIZED_PNL)
            {
                unrealizedPnL = ReadDouble();
            }

            if (serverVersion >= MinServerVer.REALIZED_PNL)
            {
                realizedPnL = ReadDouble();
            }

            eWrapper.Pnl(reqId, dailyPnL, unrealizedPnL, realizedPnL);
        }

        private void HistogramDataEvent()
        {
            var reqId = ReadInt();
            var n = ReadInt();
            var data = new HistogramEntry[n];

            for (int i = 0; i < n; i++)
            {
                data[i].Price = ReadDouble();
                data[i].Size = ReadDecimal();
            }

            eWrapper.HistogramData(reqId, data);
        }

        private void HeadTimestampEvent()
        {
            int reqId = ReadInt();
            string headTimestamp = ReadString();

            eWrapper.HeadTimestamp(reqId, headTimestamp);
        }

        private void HistoricalNewsEvent()
        {
            int requestId = ReadInt();
            string time = ReadString();
            string providerCode = ReadString();
            string articleId = ReadString();
            string headline = ReadString();

            eWrapper.HistoricalNews(requestId, time, providerCode, articleId, headline);
        }

        private void HistoricalNewsEndEvent()
        {
            int requestId = ReadInt();
            bool hasMore = ReadBoolFromInt();

            eWrapper.HistoricalNewsEnd(requestId, hasMore);
        }

        private void NewsArticleEvent()
        {
            int requestId = ReadInt();
            int articleType = ReadInt();
            string articleText = ReadString();

            eWrapper.NewsArticle(requestId, articleType, articleText);
        }

        private void NewsProvidersEvent()
        {
            NewsProvider[] newsProviders = new NewsProvider[0];
            int nNewsProviders = ReadInt();

            if (nNewsProviders > 0)
            {
                Array.Resize(ref newsProviders, nNewsProviders);

                for (int i = 0; i < nNewsProviders; ++i)
                {
                    newsProviders[i] = new NewsProvider(ReadString(), ReadString());
                }
            }

            eWrapper.NewsProviders(newsProviders);
        }

        private void SmartComponentsEvent()
        {
            int reqId = ReadInt();
            int n = ReadInt();
            var theMap = new Dictionary<int, KeyValuePair<string, char>>();

            for (int i = 0; i < n; i++)
            {
                int bitNumber = ReadInt();
                string exchange = ReadString();
                char exchangeLetter = ReadChar();

                theMap.Add(bitNumber, new KeyValuePair<string, char>(exchange, exchangeLetter));
            }

            eWrapper.SmartComponents(reqId, theMap);
        }

        private void TickReqParamsEvent()
        {
            int tickerId = ReadInt();
            double minTick = ReadDouble();
            string bboExchange = ReadString();
            int snapshotPermissions = ReadInt();

            eWrapper.TickReqParams(tickerId, minTick, bboExchange, snapshotPermissions);
        }

        private void TickNewsEvent()
        {
            int tickerId = ReadInt();
            long timeStamp = ReadLong();
            string providerCode = ReadString();
            string articleId = ReadString();
            string headline = ReadString();
            string extraData = ReadString();

            eWrapper.TickNews(tickerId, timeStamp, providerCode, articleId, headline, extraData);
        }

        private void SymbolSamplesEvent()
        {
            int reqId = ReadInt();
            ContractDescription[] contractDescriptions = new ContractDescription[0];
            int nContractDescriptions = ReadInt();

            if (nContractDescriptions > 0)
            {
                Array.Resize(ref contractDescriptions, nContractDescriptions);

                for (int i = 0; i < nContractDescriptions; ++i)
                {
                    // read contract fields
                    Contract contract = new Contract
                    {
                        ConId = ReadInt(),
                        Symbol = ReadString(),
                        SecType = ReadString(),
                        PrimaryExch = ReadString(),
                        Currency = ReadString()
                    };

                    // read derivative sec types list
                    string[] derivativeSecTypes = new string[0];
                    int nDerivativeSecTypes = ReadInt();
                    if (nDerivativeSecTypes > 0)
                    {
                        Array.Resize(ref derivativeSecTypes, nDerivativeSecTypes);
                        for (int j = 0; j < nDerivativeSecTypes; ++j)
                        {
                            derivativeSecTypes[j] = ReadString();
                        }
                    }
                    if (serverVersion >= MinServerVer.MIN_SERVER_VER_BOND_ISSUERID)
                    {
                        contract.Description = ReadString();
                        contract.IssuerId = ReadString();
                    }

                    ContractDescription contractDescription = new ContractDescription(contract, derivativeSecTypes);
                    contractDescriptions[i] = contractDescription;
                }
            }

            eWrapper.SymbolSamples(reqId, contractDescriptions);
        }

        private void FamilyCodesEvent()
        {
            FamilyCode[] familyCodes = new FamilyCode[0];
            int nFamilyCodes = ReadInt();

            if (nFamilyCodes > 0)
            {
                Array.Resize(ref familyCodes, nFamilyCodes);

                for (int i = 0; i < nFamilyCodes; ++i)
                {
                    familyCodes[i] = new FamilyCode(ReadString(), ReadString());
                }
            }

            eWrapper.FamilyCodes(familyCodes);
        }

        private void MktDepthExchangesEvent()
        {
            DepthMktDataDescription[] depthMktDataDescriptions = new DepthMktDataDescription[0];
            int nDescriptions = ReadInt();

            if (nDescriptions > 0)
            {
                Array.Resize(ref depthMktDataDescriptions, nDescriptions);

                for (int i = 0; i < nDescriptions; i++)
                {
                    if (serverVersion >= MinServerVer.SERVICE_DATA_TYPE)
                    {
                        depthMktDataDescriptions[i] = new DepthMktDataDescription(ReadString(), ReadString(), ReadString(), ReadString(), ReadIntMax());
                    }
                    else
                    {
                        depthMktDataDescriptions[i] = new DepthMktDataDescription(ReadString(), ReadString(), "", ReadBoolFromInt() ? "Deep2" : "Deep", int.MaxValue);
                    }
                }
            }

            eWrapper.MktDepthExchanges(depthMktDataDescriptions);
        }

        private void SoftDollarTierEvent()
        {
            int reqId = ReadInt();
            int nTiers = ReadInt();
            SoftDollarTier[] tiers = new SoftDollarTier[nTiers];

            for (int i = 0; i < nTiers; i++)
            {
                tiers[i] = new SoftDollarTier(ReadString(), ReadString(), ReadString());
            }

            eWrapper.SoftDollarTiers(reqId, tiers);
        }

        private void SecurityDefinitionOptionParameterEndEvent()
        {
            int reqId = ReadInt();

            eWrapper.SecurityDefinitionOptionParameterEnd(reqId);
        }

        private void SecurityDefinitionOptionParameterEvent()
        {
            int reqId = ReadInt();
            string exchange = ReadString();
            int underlyingConId = ReadInt();
            string tradingClass = ReadString();
            string multiplier = ReadString();
            int expirationsSize = ReadInt();
            HashSet<string> expirations = [];
            HashSet<double> strikes = [];

            for (int i = 0; i < expirationsSize; i++)
            {
                expirations.Add(ReadString());
            }

            int strikesSize = ReadInt();

            for (int i = 0; i < strikesSize; i++)
            {
                strikes.Add(ReadDouble());
            }

            eWrapper.SecurityDefinitionOptionParameter(reqId, exchange, underlyingConId, tradingClass, multiplier, expirations, strikes);
        }

        private void DisplayGroupUpdatedEvent()
        {
            _ = ReadInt();
            int reqId = ReadInt();
            string contractInfo = ReadString();

            eWrapper.DisplayGroupUpdated(reqId, contractInfo);
        }

        private void DisplayGroupListEvent()
        {
            _ = ReadInt();
            int reqId = ReadInt();
            string groups = ReadString();

            eWrapper.DisplayGroupList(reqId, groups);
        }

        private void VerifyCompletedEvent()
        {
            _ = ReadInt();
            bool isSuccessful = string.Compare(ReadString(), "true", true) == 0;
            string errorText = ReadString();

            eWrapper.VerifyCompleted(isSuccessful, errorText);
        }

        private void VerifyMessageApiEvent()
        {
            _ = ReadInt();
            string apiData = ReadString();

            eWrapper.VerifyMessageAPI(apiData);
        }

        private void VerifyAndAuthCompletedEvent()
        {
            _ = ReadInt();
            bool isSuccessful = string.Compare(ReadString(), "true", true) == 0;
            string errorText = ReadString();

            eWrapper.VerifyAndAuthCompleted(isSuccessful, errorText);
        }

        private void VerifyAndAuthMessageApiEvent()
        {
            _ = ReadInt();
            string apiData = ReadString();
            string xyzChallenge = ReadString();

            eWrapper.VerifyAndAuthMessageAPI(apiData, xyzChallenge);
        }

        private void TickPriceEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            int tickType = ReadInt();
            double price = ReadDouble();
            decimal size = 0;

            if (msgVersion >= 2)
                size = ReadDecimal();

            TickAttrib attr = new TickAttrib();

            if (msgVersion >= 3)
            {
                int attrMask = ReadInt();

                attr.CanAutoExecute = attrMask == 1;

                if (serverVersion >= MinServerVer.PAST_LIMIT)
                {
                    BitMask mask = new BitMask(attrMask);

                    attr.CanAutoExecute = mask[0];
                    attr.PastLimit = mask[1];

                    if (serverVersion >= MinServerVer.PRE_OPEN_BID_ASK)
                    {
                        attr.PreOpen = mask[2];
                    }
                }
            }


            eWrapper.TickPrice(requestId, tickType, price, attr);

            if (msgVersion >= 2)
            {
                int sizeTickType = -1;//not a tick
                switch (tickType)
                {
                    case TickType.BID:
                        sizeTickType = TickType.BID_SIZE;
                        break;
                    case TickType.ASK:
                        sizeTickType = TickType.ASK_SIZE;
                        break;
                    case TickType.LAST:
                        sizeTickType = TickType.LAST_SIZE;
                        break;
                    case TickType.DELAYED_BID:
                        sizeTickType = TickType.DELAYED_BID_SIZE;
                        break;
                    case TickType.DELAYED_ASK:
                        sizeTickType = TickType.DELAYED_ASK_SIZE;
                        break;
                    case TickType.DELAYED_LAST:
                        sizeTickType = TickType.DELAYED_LAST_SIZE;
                        break;
                }
                if (sizeTickType != -1)
                {
                    eWrapper.TickSize(requestId, sizeTickType, size);
                }
            }
        }

        private void TickSizeEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            int tickType = ReadInt();
            decimal size = ReadDecimal();
            eWrapper.TickSize(requestId, tickType, size);
        }

        private void TickStringEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            int tickType = ReadInt();
            string value = ReadString();
            eWrapper.TickString(requestId, tickType, value);
        }

        private void TickGenericEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            int tickType = ReadInt();
            double value = ReadDouble();
            eWrapper.TickGeneric(requestId, tickType, value);
        }

        private void TickEFPEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            int tickType = ReadInt();
            double basisPoints = ReadDouble();
            string formattedBasisPoints = ReadString();
            double impliedFuturesPrice = ReadDouble();
            int holdDays = ReadInt();
            string futureLastTradeDate = ReadString();
            double dividendImpact = ReadDouble();
            double dividendsToLastTradeDate = ReadDouble();
            eWrapper.TickEFP(requestId, tickType, basisPoints, formattedBasisPoints, impliedFuturesPrice, holdDays, futureLastTradeDate, dividendImpact, dividendsToLastTradeDate);
        }

        private void TickSnapshotEndEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            eWrapper.TickSnapshotEnd(requestId);
        }

        private void ErrorEvent()
        {
            int msgVersion = ReadInt();
            if (msgVersion < 2)
            {
                string msg = ReadString();
                eWrapper.Error(msg);
            }
            else
            {
                int id = ReadInt();
                int errorCode = ReadInt();
                string errorMsg = serverVersion >= MinServerVer.ENCODE_MSG_ASCII7 ? Regex.Unescape(ReadString()) : ReadString();
                string advancedOrderRejectJson = "";
                if (serverVersion >= MinServerVer.ADVANCED_ORDER_REJECT)
                {
                    string tempStr = ReadString();
                    if (!Util.StringIsEmpty(tempStr))
                    {
                        advancedOrderRejectJson = Regex.Unescape(tempStr);
                    }
                }
                eWrapper.Error(id, errorCode, errorMsg, advancedOrderRejectJson);
            }
        }

        private void CurrentTimeEvent()
        {
            _ = ReadInt();//version
            long time = ReadLong();
            eWrapper.CurrentTime(time);
        }

        private void ManagedAccountsEvent()
        {
            _ = ReadInt();
            string accountsList = ReadString();
            eWrapper.ManagedAccounts(accountsList);
        }

        private void NextValidIdEvent()
        {
            _ = ReadInt();
            int orderId = ReadInt();
            eWrapper.NextValidId(orderId);
        }

        private void DeltaNeutralValidationEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            DeltaNeutralContract deltaNeutralContract = new DeltaNeutralContract
            {
                ConId = ReadInt(),
                Delta = ReadDouble(),
                Price = ReadDouble()
            };
            eWrapper.DeltaNeutralValidation(requestId, deltaNeutralContract);
        }

        private void TickOptionComputationEvent()
        {
            int msgVersion = serverVersion >= MinServerVer.PRICE_BASED_VOLATILITY ? int.MaxValue : ReadInt();

            int requestId = ReadInt();
            int tickType = ReadInt();
            int tickAttrib = int.MaxValue;
            if (serverVersion >= MinServerVer.PRICE_BASED_VOLATILITY)
            {
                tickAttrib = ReadInt();
            }
            double impliedVolatility = ReadDouble();
            if (impliedVolatility.Equals(-1))
            { // -1 is the "not yet computed" indicator
                impliedVolatility = double.MaxValue;
            }
            double delta = ReadDouble();
            if (delta.Equals(-2))
            { // -2 is the "not yet computed" indicator
                delta = double.MaxValue;
            }
            double optPrice = double.MaxValue;
            double pvDividend = double.MaxValue;
            double gamma = double.MaxValue;
            double vega = double.MaxValue;
            double theta = double.MaxValue;
            double undPrice = double.MaxValue;
            if (msgVersion >= 6 || tickType == TickType.MODEL_OPTION || tickType == TickType.DELAYED_MODEL_OPTION)
            {
                optPrice = ReadDouble();
                if (optPrice.Equals(-1))
                { // -1 is the "not yet computed" indicator
                    optPrice = double.MaxValue;
                }
                pvDividend = ReadDouble();
                if (pvDividend.Equals(-1))
                { // -1 is the "not yet computed" indicator
                    pvDividend = double.MaxValue;
                }
            }
            if (msgVersion >= 6)
            {
                gamma = ReadDouble();
                if (gamma.Equals(-2))
                { // -2 is the "not yet computed" indicator
                    gamma = double.MaxValue;
                }
                vega = ReadDouble();
                if (vega.Equals(-2))
                { // -2 is the "not yet computed" indicator
                    vega = double.MaxValue;
                }
                theta = ReadDouble();
                if (theta.Equals(-2))
                { // -2 is the "not yet computed" indicator
                    theta = double.MaxValue;
                }
                undPrice = ReadDouble();
                if (undPrice.Equals(-1))
                { // -1 is the "not yet computed" indicator
                    undPrice = double.MaxValue;
                }
            }

            eWrapper.TickOptionComputation(requestId, tickType, tickAttrib, impliedVolatility, delta, optPrice, pvDividend, gamma, vega, theta, undPrice);
        }

        private void AccountSummaryEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            string account = ReadString();
            string tag = ReadString();
            string value = ReadString();
            string currency = ReadString();
            eWrapper.AccountSummary(requestId, account, tag, value, currency);
        }

        private void AccountSummaryEndEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            eWrapper.AccountSummaryEnd(requestId);
        }

        private void AccountValueEvent()
        {
            int msgVersion = ReadInt();
            string key = ReadString();
            string value = ReadString();
            string currency = ReadString();
            string accountName = null;
            if (msgVersion >= 2)
                accountName = ReadString();
            eWrapper.UpdateAccountValue(key, value, currency, accountName);
        }

        private void BondContractDetailsEvent()
        {
            int msgVersion = 6;
            if (serverVersion < MinServerVer.SIZE_RULES)
            {
                msgVersion = ReadInt();
            }
            int requestId = -1;
            if (msgVersion >= 3)
            {
                requestId = ReadInt();
            }

            ContractDetails contract = new ContractDetails();

            contract.Contract.Symbol = ReadString();
            contract.Contract.SecType = ReadString();
            contract.Cusip = ReadString();
            contract.Coupon = ReadDouble();
            ReadLastTradeDate(contract, true);
            contract.IssueDate = ReadString();
            contract.Ratings = ReadString();
            contract.BondType = ReadString();
            contract.CouponType = ReadString();
            contract.Convertible = ReadBoolFromInt();
            contract.Callable = ReadBoolFromInt();
            contract.Putable = ReadBoolFromInt();
            contract.DescAppend = ReadString();
            contract.Contract.Exchange = ReadString();
            contract.Contract.Currency = ReadString();
            contract.MarketName = ReadString();
            contract.Contract.TradingClass = ReadString();
            contract.Contract.ConId = ReadInt();
            contract.MinTick = ReadDouble();
            if (serverVersion >= MinServerVer.MD_SIZE_MULTIPLIER && serverVersion < MinServerVer.SIZE_RULES)
            {
                ReadInt(); // MdSizeMultiplier - not used anymore
            }
            contract.OrderTypes = ReadString();
            contract.ValidExchanges = ReadString();
            if (msgVersion >= 2)
            {
                contract.NextOptionDate = ReadString();
                contract.NextOptionType = ReadString();
                contract.NextOptionPartial = ReadBoolFromInt();
                contract.Notes = ReadString();
            }
            if (msgVersion >= 4)
            {
                contract.LongName = ReadString();
            }
            if (msgVersion >= 6)
            {
                contract.EvRule = ReadString();
                contract.EvMultiplier = ReadDouble();
            }
            if (msgVersion >= 5)
            {
                int secIdListCount = ReadInt();
                if (secIdListCount > 0)
                {
                    contract.SecIdList = [];
                    for (int i = 0; i < secIdListCount; ++i)
                    {
                        TagValue tagValue = new TagValue
                        {
                            Tag = ReadString(),
                            Value = ReadString()
                        };
                        contract.SecIdList.Add(tagValue);
                    }
                }
            }
            if (serverVersion >= MinServerVer.AGG_GROUP)
            {
                contract.AggGroup = ReadInt();
            }
            if (serverVersion >= MinServerVer.MARKET_RULES)
            {
                contract.MarketRuleIds = ReadString();
            }
            if (serverVersion >= MinServerVer.SIZE_RULES)
            {
                contract.MinSize = ReadDecimal();
                contract.SizeIncrement = ReadDecimal();
                contract.SuggestedSizeIncrement = ReadDecimal();
            }

            eWrapper.BondContractDetails(requestId, contract);
        }

        private void PortfolioValueEvent()
        {
            int msgVersion = ReadInt();
            Contract contract = new Contract();
            if (msgVersion >= 6)
                contract.ConId = ReadInt();
            contract.Symbol = ReadString();
            contract.SecType = ReadString();
            contract.LastTradeDateOrContractMonth = ReadString();
            contract.Strike = ReadDouble();
            contract.Right = ReadString();
            if (msgVersion >= 7)
            {
                contract.Multiplier = ReadString();
                contract.PrimaryExch = ReadString();
            }
            contract.Currency = ReadString();
            if (msgVersion >= 2)
            {
                contract.LocalSymbol = ReadString();
            }
            if (msgVersion >= 8)
            {
                contract.TradingClass = ReadString();
            }

            decimal position = ReadDecimal();
            double marketPrice = ReadDouble();
            double marketValue = ReadDouble();
            double averageCost = 0.0;
            double unrealizedPNL = 0.0;
            double realizedPNL = 0.0;
            if (msgVersion >= 3)
            {
                averageCost = ReadDouble();
                unrealizedPNL = ReadDouble();
                realizedPNL = ReadDouble();
            }

            string accountName = null;
            if (msgVersion >= 4)
            {
                accountName = ReadString();
            }

            if (msgVersion == 6 && serverVersion == 39)
            {
                contract.PrimaryExch = ReadString();
            }

            eWrapper.UpdatePortfolio(contract, position, marketPrice, marketValue,
                            averageCost, unrealizedPNL, realizedPNL, accountName);
        }

        private void AccountUpdateTimeEvent()
        {
            _ = ReadInt();
            string timestamp = ReadString();
            eWrapper.UpdateAccountTime(timestamp);
        }

        private void AccountDownloadEndEvent()
        {
            _ = ReadInt();
            string account = ReadString();
            eWrapper.AccountDownloadEnd(account);
        }

        private void OrderStatusEvent()
        {
            int msgVersion = serverVersion >= MinServerVer.MARKET_CAP_PRICE ? int.MaxValue : ReadInt();
            int id = ReadInt();
            string status = ReadString();
            decimal filled = ReadDecimal();
            decimal remaining = ReadDecimal();
            double avgFillPrice = ReadDouble();

            int permId = 0;
            if (msgVersion >= 2)
            {
                permId = ReadInt();
            }

            int parentId = 0;
            if (msgVersion >= 3)
            {
                parentId = ReadInt();
            }

            double lastFillPrice = 0;
            if (msgVersion >= 4)
            {
                lastFillPrice = ReadDouble();
            }

            int clientId = 0;
            if (msgVersion >= 5)
            {
                clientId = ReadInt();
            }

            string whyHeld = null;
            if (msgVersion >= 6)
            {
                whyHeld = ReadString();
            }

            double mktCapPrice = double.MaxValue;

            if (serverVersion >= MinServerVer.MARKET_CAP_PRICE)
            {
                mktCapPrice = ReadDouble();
            }

            eWrapper.OrderStatus(id, status, filled, remaining, avgFillPrice, permId, parentId, lastFillPrice, clientId, whyHeld, mktCapPrice);
        }

        private void OpenOrderEvent()
        {
            int msgVersion = serverVersion < MinServerVer.ORDER_CONTAINER ? ReadInt() : serverVersion;

            Contract contract = new Contract();
            Order order = new Order();
            OrderState orderState = new OrderState();
            EOrderDecoder eOrderDecoder = new EOrderDecoder(this, contract, order, orderState, msgVersion, serverVersion);

            // read order id
            eOrderDecoder.ReadOrderId();

            // read contract fields
            eOrderDecoder.ReadContractFields();

            // read order fields
            eOrderDecoder.ReadAction();
            eOrderDecoder.ReadTotalQuantity();
            eOrderDecoder.ReadOrderType();
            eOrderDecoder.ReadLmtPrice();
            eOrderDecoder.ReadAuxPrice();
            eOrderDecoder.ReadTIF();
            eOrderDecoder.ReadOcaGroup();
            eOrderDecoder.ReadAccount();
            eOrderDecoder.ReadOpenClose();
            eOrderDecoder.ReadOrigin();
            eOrderDecoder.ReadOrderRef();
            eOrderDecoder.ReadClientId();
            eOrderDecoder.ReadPermId();
            eOrderDecoder.ReadOutsideRth();
            eOrderDecoder.ReadHidden();
            eOrderDecoder.ReadDiscretionaryAmount();
            eOrderDecoder.ReadGoodAfterTime();
            eOrderDecoder.SkipSharesAllocation();
            eOrderDecoder.ReadFAParams();
            eOrderDecoder.ReadModelCode();
            eOrderDecoder.ReadGoodTillDate();
            eOrderDecoder.ReadRule80A();
            eOrderDecoder.ReadPercentOffset();
            eOrderDecoder.ReadSettlingFirm();
            eOrderDecoder.ReadShortSaleParams();
            eOrderDecoder.ReadAuctionStrategy();
            eOrderDecoder.ReadBoxOrderParams();
            eOrderDecoder.ReadPegToStkOrVolOrderParams();
            eOrderDecoder.ReadDisplaySize();
            eOrderDecoder.ReadOldStyleOutsideRth();
            eOrderDecoder.ReadBlockOrder();
            eOrderDecoder.ReadSweepToFill();
            eOrderDecoder.ReadAllOrNone();
            eOrderDecoder.ReadMinQty();
            eOrderDecoder.ReadOcaType();
            eOrderDecoder.SkipETradeOnly();
            eOrderDecoder.SkipFirmQuoteOnly();
            eOrderDecoder.SkipNbboPriceCap();
            eOrderDecoder.ReadParentId();
            eOrderDecoder.ReadTriggerMethod();
            eOrderDecoder.ReadVolOrderParams(true);
            eOrderDecoder.ReadTrailParams();
            eOrderDecoder.ReadBasisPoints();
            eOrderDecoder.ReadComboLegs();
            eOrderDecoder.ReadSmartComboRoutingParams();
            eOrderDecoder.ReadScaleOrderParams();
            eOrderDecoder.ReadHedgeParams();
            eOrderDecoder.ReadOptOutSmartRouting();
            eOrderDecoder.ReadClearingParams();
            eOrderDecoder.ReadNotHeld();
            eOrderDecoder.ReadDeltaNeutral();
            eOrderDecoder.ReadAlgoParams();
            eOrderDecoder.ReadSolicited();
            eOrderDecoder.ReadWhatIfInfoAndCommission();
            eOrderDecoder.ReadVolRandomizeFlags();
            eOrderDecoder.ReadPegToBenchParams();
            eOrderDecoder.ReadConditions();
            eOrderDecoder.ReadAdjustedOrderParams();
            eOrderDecoder.ReadSoftDollarTier();
            eOrderDecoder.ReadCashQty();
            eOrderDecoder.ReadDontUseAutoPriceForHedge();
            eOrderDecoder.ReadIsOmsContainer();
            eOrderDecoder.ReadDiscretionaryUpToLimitPrice();
            eOrderDecoder.ReadUsePriceMgmtAlgo();
            eOrderDecoder.ReadDuration();
            eOrderDecoder.ReadPostToAts();
            eOrderDecoder.ReadAutoCancelParent(MinServerVer.AUTO_CANCEL_PARENT);
            eOrderDecoder.ReadPegBestPegMidOrderAttributes();

            eWrapper.OpenOrder(order.OrderId, contract, order, orderState);
        }

        private void OpenOrderEndEvent()
        {
            _ = ReadInt();
            eWrapper.OpenOrderEnd();
        }

        private void ContractDataEvent()
        {
            int msgVersion = 8;
            if (serverVersion < MinServerVer.SIZE_RULES)
            {
                msgVersion = ReadInt();
            }
            int requestId = -1;
            if (msgVersion >= 3)
                requestId = ReadInt();
            ContractDetails contract = new ContractDetails();
            contract.Contract.Symbol = ReadString();
            contract.Contract.SecType = ReadString();
            ReadLastTradeDate(contract, false);
            contract.Contract.Strike = ReadDouble();
            contract.Contract.Right = ReadString();
            contract.Contract.Exchange = ReadString();
            contract.Contract.Currency = ReadString();
            contract.Contract.LocalSymbol = ReadString();
            contract.MarketName = ReadString();
            contract.Contract.TradingClass = ReadString();
            contract.Contract.ConId = ReadInt();
            contract.MinTick = ReadDouble();
            if (serverVersion >= MinServerVer.MD_SIZE_MULTIPLIER && serverVersion < MinServerVer.SIZE_RULES)
            {
                ReadInt(); // MdSizeMultiplier - not used anymore
            }
            contract.Contract.Multiplier = ReadString();
            contract.OrderTypes = ReadString();
            contract.ValidExchanges = ReadString();
            if (msgVersion >= 2)
            {
                contract.PriceMagnifier = ReadInt();
            }
            if (msgVersion >= 4)
            {
                contract.UnderConId = ReadInt();
            }
            if (msgVersion >= 5)
            {
                contract.LongName = serverVersion >= MinServerVer.ENCODE_MSG_ASCII7 ? Regex.Unescape(ReadString()) : ReadString();
                contract.Contract.PrimaryExch = ReadString();
            }
            if (msgVersion >= 6)
            {
                contract.ContractMonth = ReadString();
                contract.Industry = ReadString();
                contract.Category = ReadString();
                contract.Subcategory = ReadString();
                contract.TimeZoneId = ReadString();
                contract.TradingHours = ReadString();
                contract.LiquidHours = ReadString();
            }
            if (msgVersion >= 8)
            {
                contract.EvRule = ReadString();
                contract.EvMultiplier = ReadDouble();
            }
            if (msgVersion >= 7)
            {
                int secIdListCount = ReadInt();
                if (secIdListCount > 0)
                {
                    contract.SecIdList = new List<TagValue>(secIdListCount);
                    for (int i = 0; i < secIdListCount; ++i)
                    {
                        TagValue tagValue = new TagValue
                        {
                            Tag = ReadString(),
                            Value = ReadString()
                        };
                        contract.SecIdList.Add(tagValue);
                    }
                }
            }
            if (serverVersion >= MinServerVer.AGG_GROUP)
            {
                contract.AggGroup = ReadInt();
            }
            if (serverVersion >= MinServerVer.UNDERLYING_INFO)
            {
                contract.UnderSymbol = ReadString();
                contract.UnderSecType = ReadString();
            }
            if (serverVersion >= MinServerVer.MARKET_RULES)
            {
                contract.MarketRuleIds = ReadString();
            }
            if (serverVersion >= MinServerVer.REAL_EXPIRATION_DATE)
            {
                contract.RealExpirationDate = ReadString();
            }
            if (serverVersion >= MinServerVer.STOCK_TYPE)
            {
                contract.StockType = ReadString();
            }
            if (serverVersion >= MinServerVer.FRACTIONAL_SIZE_SUPPORT && serverVersion < MinServerVer.SIZE_RULES)
            {
                ReadDecimal(); // SizeMinTick - not used anymore
            }
            if (serverVersion >= MinServerVer.SIZE_RULES)
            {
                contract.MinSize = ReadDecimal();
                contract.SizeIncrement = ReadDecimal();
                contract.SuggestedSizeIncrement = ReadDecimal();
            }

            eWrapper.ContractDetails(requestId, contract);
        }


        private void ContractDataEndEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            eWrapper.ContractDetailsEnd(requestId);
        }

        private void ExecutionDataEvent()
        {
            int msgVersion = serverVersion;

            if (serverVersion < MinServerVer.LAST_LIQUIDITY)
            {
                msgVersion = ReadInt();
            }

            int requestId = -1;
            if (msgVersion >= 7)
                requestId = ReadInt();
            int orderId = ReadInt();
            Contract contract = new Contract();
            if (msgVersion >= 5)
            {
                contract.ConId = ReadInt();
            }
            contract.Symbol = ReadString();
            contract.SecType = ReadString();
            contract.LastTradeDateOrContractMonth = ReadString();
            contract.Strike = ReadDouble();
            contract.Right = ReadString();
            if (msgVersion >= 9)
            {
                contract.Multiplier = ReadString();
            }
            contract.Exchange = ReadString();
            contract.Currency = ReadString();
            contract.LocalSymbol = ReadString();
            if (msgVersion >= 10)
            {
                contract.TradingClass = ReadString();
            }

            Execution exec = new Execution
            {
                OrderId = orderId,
                ExecId = ReadString(),
                Time = ReadString(),
                AcctNumber = ReadString(),
                Exchange = ReadString(),
                Side = ReadString(),
                Shares = ReadDecimal(),
                Price = ReadDouble()
            };
            if (msgVersion >= 2)
            {
                exec.PermId = ReadInt();
            }
            if (msgVersion >= 3)
            {
                exec.ClientId = ReadInt();
            }
            if (msgVersion >= 4)
            {
                exec.Liquidation = ReadInt();
            }
            if (msgVersion >= 6)
            {
                exec.CumQty = ReadDecimal();
                exec.AvgPrice = ReadDouble();
            }
            if (msgVersion >= 8)
            {
                exec.OrderRef = ReadString();
            }
            if (msgVersion >= 9)
            {
                exec.EvRule = ReadString();
                exec.EvMultiplier = ReadDouble();
            }
            if (serverVersion >= MinServerVer.MODELS_SUPPORT)
            {
                exec.ModelCode = ReadString();
            }

            if (serverVersion >= MinServerVer.LAST_LIQUIDITY)
            {
                exec.LastLiquidity = new Liquidity(ReadInt());
            }

            eWrapper.ExecDetails(requestId, contract, exec);
        }

        private void ExecutionDataEndEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            eWrapper.ExecDetailsEnd(requestId);
        }

        private void CommissionReportEvent()
        {
            _ = ReadInt();
            CommissionReport commissionReport = new CommissionReport
            {
                ExecId = ReadString(),
                Commission = ReadDouble(),
                Currency = ReadString(),
                RealizedPNL = ReadDouble(),
                Yield = ReadDouble(),
                YieldRedemptionDate = ReadInt()
            };
            eWrapper.CommissionReport(commissionReport);
        }

        private void FundamentalDataEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            string fundamentalData = ReadString();
            eWrapper.FundamentalData(requestId, fundamentalData);
        }

        private void HistoricalDataEvent()
        {
            int msgVersion = int.MaxValue;

            if (serverVersion < MinServerVer.SYNT_REALTIME_BARS)
            {
                msgVersion = ReadInt();
            }

            int requestId = ReadInt();
            string startDateStr = "";
            string endDateStr = "";

            if (msgVersion >= 2)
            {
                startDateStr = ReadString();
                endDateStr = ReadString();
            }

            int itemCount = ReadInt();

            for (int ctr = 0; ctr < itemCount; ctr++)
            {
                string date = ReadString();
                double open = ReadDouble();
                double high = ReadDouble();
                double low = ReadDouble();
                double close = ReadDouble();
                decimal volume = ReadDecimal();
                decimal WAP = ReadDecimal();

                if (serverVersion < MinServerVer.SYNT_REALTIME_BARS)
                {
                    /*string hasGaps = */
                    ReadString();
                }

                int barCount = -1;

                if (msgVersion >= 3)
                {
                    barCount = ReadInt();
                }

                var bar = new Bar
                {
                    Time = date,
                    Open = open,
                    High = high,
                    Low = low,
                    Close = close,
                    Volume = volume,
                    Count = barCount,
                    WAP = WAP
                };
                eWrapper.HistoricalData(requestId, bar);
            }

            // send end of dataset marker.
            eWrapper.HistoricalDataEnd(requestId, startDateStr, endDateStr);
        }

        private void MarketDataTypeEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            int marketDataType = ReadInt();
            eWrapper.MarketDataType(requestId, marketDataType);
        }

        private void MarketDepthEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            int position = ReadInt();
            int operation = ReadInt();
            int side = ReadInt();
            double price = ReadDouble();
            decimal size = ReadDecimal();
            eWrapper.UpdateMktDepth(requestId, position, operation, side, price, size);
        }

        private void MarketDepthL2Event()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            int position = ReadInt();
            string marketMaker = ReadString();
            int operation = ReadInt();
            int side = ReadInt();
            double price = ReadDouble();
            decimal size = ReadDecimal();

            bool isSmartDepth = false;
            if (serverVersion >= MinServerVer.SMART_DEPTH)
            {
                isSmartDepth = ReadBoolFromInt();
            }

            eWrapper.UpdateMktDepthL2(requestId, position, marketMaker, operation, side, price, size, isSmartDepth);
        }

        private void NewsBulletinsEvent()
        {
            _ = ReadInt();
            int newsMsgId = ReadInt();
            int newsMsgType = ReadInt();
            string newsMessage = ReadString();
            string originatingExch = ReadString();
            eWrapper.UpdateNewsBulletin(newsMsgId, newsMsgType, newsMessage, originatingExch);
        }

        private void PositionEvent()
        {
            int msgVersion = ReadInt();
            string account = ReadString();
            Contract contract = new Contract
            {
                ConId = ReadInt(),
                Symbol = ReadString(),
                SecType = ReadString(),
                LastTradeDateOrContractMonth = ReadString(),
                Strike = ReadDouble(),
                Right = ReadString(),
                Multiplier = ReadString(),
                Exchange = ReadString(),
                Currency = ReadString(),
                LocalSymbol = ReadString()
            };
            if (msgVersion >= 2)
            {
                contract.TradingClass = ReadString();
            }

            decimal pos = ReadDecimal();
            double avgCost = 0;
            if (msgVersion >= 3)
                avgCost = ReadDouble();
            eWrapper.Position(account, contract, pos, avgCost);
        }

        private void PositionEndEvent()
        {
            _ = ReadInt();
            eWrapper.PositionEnd();
        }

        private void RealTimeBarsEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            long time = ReadLong();
            double open = ReadDouble();
            double high = ReadDouble();
            double low = ReadDouble();
            double close = ReadDouble();
            decimal volume = ReadDecimal();
            decimal wap = ReadDecimal();
            int count = ReadInt();
            eWrapper.RealtimeBar(requestId, time, open, high, low, close, volume, wap, count);
        }

        private void ScannerParametersEvent()
        {
            _ = ReadInt();
            string xml = ReadString();
            eWrapper.ScannerParameters(xml);
        }

        private void ScannerDataEvent()
        {
            int msgVersion = ReadInt();
            int requestId = ReadInt();
            int numberOfElements = ReadInt();
            for (int i = 0; i < numberOfElements; i++)
            {
                int rank = ReadInt();
                ContractDetails conDet = new ContractDetails();
                if (msgVersion >= 3)
                    conDet.Contract.ConId = ReadInt();
                conDet.Contract.Symbol = ReadString();
                conDet.Contract.SecType = ReadString();
                conDet.Contract.LastTradeDateOrContractMonth = ReadString();
                conDet.Contract.Strike = ReadDouble();
                conDet.Contract.Right = ReadString();
                conDet.Contract.Exchange = ReadString();
                conDet.Contract.Currency = ReadString();
                conDet.Contract.LocalSymbol = ReadString();
                conDet.MarketName = ReadString();
                conDet.Contract.TradingClass = ReadString();
                string distance = ReadString();
                string benchmark = ReadString();
                string projection = ReadString();
                string legsStr = null;
                if (msgVersion >= 2)
                {
                    legsStr = ReadString();
                }
                eWrapper.ScannerData(requestId, rank, conDet, distance,
                    benchmark, projection, legsStr);
            }
            eWrapper.ScannerDataEnd(requestId);
        }

        private void ReceiveFAEvent()
        {
            _ = ReadInt();
            int faDataType = ReadInt();
            string faData = ReadString();
            eWrapper.ReceiveFA(faDataType, faData);
        }

        private void PositionMultiEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            string account = ReadString();
            Contract contract = new Contract
            {
                ConId = ReadInt(),
                Symbol = ReadString(),
                SecType = ReadString(),
                LastTradeDateOrContractMonth = ReadString(),
                Strike = ReadDouble(),
                Right = ReadString(),
                Multiplier = ReadString(),
                Exchange = ReadString(),
                Currency = ReadString(),
                LocalSymbol = ReadString(),
                TradingClass = ReadString()
            };
            decimal pos = ReadDecimal();
            double avgCost = ReadDouble();
            string modelCode = ReadString();
            eWrapper.PositionMulti(requestId, account, modelCode, contract, pos, avgCost);
        }

        private void PositionMultiEndEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            eWrapper.PositionMultiEnd(requestId);
        }

        private void AccountUpdateMultiEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            string account = ReadString();
            string modelCode = ReadString();
            string key = ReadString();
            string value = ReadString();
            string currency = ReadString();
            eWrapper.AccountUpdateMulti(requestId, account, modelCode, key, value, currency);
        }

        private void AccountUpdateMultiEndEvent()
        {
            _ = ReadInt();
            int requestId = ReadInt();
            eWrapper.AccountUpdateMultiEnd(requestId);
        }

        private void ReplaceFAEndEvent()
        {
            int reqId = ReadInt();
            string text = ReadString();
            eWrapper.ReplaceFAEnd(reqId, text);
        }

        private void ProcessWshMetaData()
        {
            int reqId = ReadInt();
            string dataJson = ReadString();

            eWrapper.WshMetaData(reqId, dataJson);
        }

        private void ProcessWshEventData()
        {
            int reqId = ReadInt();
            string dataJson = ReadString();
            eWrapper.WshEventData(reqId, dataJson);
        }

        private void ProcessHistoricalScheduleEvent()
        {
            int reqId = ReadInt();
            string startDateTime = ReadString();
            string endDateTime = ReadString();
            string timeZone = ReadString();

            int sessionsCount = ReadInt();
            HistoricalSession[] sessions = new HistoricalSession[sessionsCount];

            for (int i = 0; i < sessionsCount; i++)
            {
                var sessionStartDateTime = ReadString();
                var sessionEndDateTime = ReadString();
                var sessionRefDate = ReadString();

                sessions[i] = new HistoricalSession(sessionStartDateTime, sessionEndDateTime, sessionRefDate);
            }

            eWrapper.HistoricalSchedule(reqId, startDateTime, endDateTime, timeZone, sessions);
        }

        private void ProcessUserInfoEvent()
        {
            int reqId = ReadInt();
            string whiteBrandingId = ReadString();

            eWrapper.UserInfo(reqId, whiteBrandingId);
        }

        public double ReadDouble()
        {
            string doubleAsstring = ReadString();
            if (string.IsNullOrEmpty(doubleAsstring) ||
                doubleAsstring == "0")
            {
                return 0;
            }
            else return double.Parse(doubleAsstring, System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        public double ReadDoubleMax()
        {
            string str = ReadString();
            return string.IsNullOrEmpty(str) ? double.MaxValue : str == Constants.INFINITY_STR ? double.PositiveInfinity : double.Parse(str, System.Globalization.NumberFormatInfo.InvariantInfo);
        }

        public decimal ReadDecimal()
        {
            string str = ReadString();
            return Util.StringToDecimal(str);
        }

        public long ReadLong()
        {
            string longAsstring = ReadString();
            if (string.IsNullOrEmpty(longAsstring) ||
                longAsstring == "0")
            {
                return 0;
            }
            else return long.Parse(longAsstring);
        }

        public int ReadInt()
        {
            string intAsstring = ReadString();
            if (string.IsNullOrEmpty(intAsstring) ||
                intAsstring == "0")
            {
                return 0;
            }
            else return int.Parse(intAsstring);
        }

        public int ReadIntMax()
        {
            string str = ReadString();
            return string.IsNullOrEmpty(str) ? int.MaxValue : int.Parse(str);
        }

        public bool ReadBoolFromInt()
        {
            string str = ReadString();
            return !string.IsNullOrEmpty(str) && (int.Parse(str) != 0);
        }

        public char ReadChar()
        {
            string str = ReadString();
            return str == null ? '\0' : str[0];
        }

        public string ReadString()
        {
            byte b = dataReader.ReadByte();

            nDecodedLen++;

            if (b == 0)
            {
                return null;
            }
            else
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append((char)b);
                while (true)
                {
                    b = dataReader.ReadByte();
                    if (b == 0)
                    {
                        break;
                    }
                    else
                    {
                        strBuilder.Append((char)b);
                    }
                }

                nDecodedLen += strBuilder.Length;

                return strBuilder.ToString();
            }
        }

        private void ReadLastTradeDate(ContractDetails contract, bool isBond)
        {
            string lastTradeDateOrContractMonth = ReadString();
            if (lastTradeDateOrContractMonth != null)
            {
                string[] splitted = lastTradeDateOrContractMonth.Contains("-") ? Regex.Split(lastTradeDateOrContractMonth, "-") : Regex.Split(lastTradeDateOrContractMonth, "\\s+");
                if (splitted.Length > 0)
                {
                    if (isBond)
                    {
                        contract.Maturity = splitted[0];
                    }
                    else
                    {
                        contract.Contract.LastTradeDateOrContractMonth = splitted[0];
                    }
                }
                if (splitted.Length > 1)
                {
                    contract.LastTradeTime = splitted[1];
                }
                if (isBond && splitted.Length > 2)
                {
                    contract.TimeZoneId = splitted[2];
                }
            }
        }
    }
}
