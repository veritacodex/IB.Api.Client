/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System.Collections.Generic;

namespace IBApi
{
    class EOrderDecoder
    {
        private readonly EDecoder eDecoder;
        private readonly Contract contract;
        private readonly Order order;
        private readonly OrderState orderState;
        private readonly int msgVersion;
        private readonly int serverVersion;

        public EOrderDecoder(EDecoder eDecoder, Contract contract, Order order, OrderState orderState, int msgVersion, int serverVersion)
        {
            this.eDecoder = eDecoder;
            this.contract = contract;
            this.order = order;
            this.orderState = orderState;
            this.msgVersion = msgVersion;
            this.serverVersion = serverVersion;
        }

        public void ReadOrderId()
        {
            order.OrderId = eDecoder.ReadInt();
        }

        public void ReadAction() 
        {
            order.Action = eDecoder.ReadString();
        }

        public void ReadContractFields()
        {
            if (msgVersion >= 17)
            {
                contract.ConId = eDecoder.ReadInt();
            }
            contract.Symbol = eDecoder.ReadString();
            contract.SecType = eDecoder.ReadString();
            contract.LastTradeDateOrContractMonth = eDecoder.ReadString();
            contract.Strike = eDecoder.ReadDouble();
            contract.Right = eDecoder.ReadString();
            if (msgVersion >= 32)
            {
                contract.Multiplier = eDecoder.ReadString();
            }
            contract.Exchange = eDecoder.ReadString();
            contract.Currency = eDecoder.ReadString();
            if (msgVersion >= 2)
            {
                contract.LocalSymbol = eDecoder.ReadString();
            }
            if (msgVersion >= 32)
            {
                contract.TradingClass = eDecoder.ReadString();
            }
        }

        public void ReadTotalQuantity()
        {
            order.TotalQuantity = eDecoder.ReadDecimal();
        }

        public void ReadOrderType()
        {
            order.OrderType = eDecoder.ReadString();
        }

        public void ReadLmtPrice() 
        {
            if (msgVersion < 29)
            {
                order.LmtPrice = eDecoder.ReadDouble();
            }
            else
            {
                order.LmtPrice = eDecoder.ReadDoubleMax();
            }
        }

        public void ReadAuxPrice() 
        {
            if (msgVersion < 30)
            {
                order.AuxPrice = eDecoder.ReadDouble();
            }
            else
            {
                order.AuxPrice = eDecoder.ReadDoubleMax();
            }
        }

        public void ReadTIF() 
        {
            order.Tif = eDecoder.ReadString();
        }

        public void ReadOcaGroup() 
        {
            order.OcaGroup = eDecoder.ReadString();
        }

        public void ReadAccount() 
        {
            order.Account = eDecoder.ReadString();
        }

        public void ReadOpenClose() 
        {
            order.OpenClose = eDecoder.ReadString();
        }

        public void ReadOrigin() 
        {
            order.Origin = eDecoder.ReadInt();
        }

        public void ReadOrderRef() 
        {
            order.OrderRef = eDecoder.ReadString();
        }

        public void ReadClientId() 
        {
            if (msgVersion >= 3)
            {
                order.ClientId = eDecoder.ReadInt();
            }
        }

        public void ReadPermId() 
        {
            if (msgVersion >= 4)
            {
                order.PermId = eDecoder.ReadInt();
            }
        }

        public void ReadOutsideRth() 
        {
            if (msgVersion >= 4)
            {
                if (msgVersion < 18)
                {
                    // will never happen
                    /* order.ignoreRth = */
                    eDecoder.ReadBoolFromInt();
                }
                else
                {
                    order.OutsideRth = eDecoder.ReadBoolFromInt();
                }
            }
        }

        public void ReadHidden() 
        {
            if (msgVersion >= 4)
            {
                order.Hidden = eDecoder.ReadInt() == 1;
            }
        }

        public void ReadDiscretionaryAmount() 
        {
            if (msgVersion >= 4)
            {
                order.DiscretionaryAmt = eDecoder.ReadDouble();
            }
        }

        public void ReadGoodAfterTime() 
        {
            if (msgVersion >= 5)
            {
                order.GoodAfterTime = eDecoder.ReadString();
            }
        }

        public void SkipSharesAllocation()
        {
            if (msgVersion >= 6)
            {
                // skip deprecated sharesAllocation field
                eDecoder.ReadString();
            }
        }


        public void ReadFAParams() 
        {
            if (msgVersion >= 7)
            {
                order.FaGroup = eDecoder.ReadString();
                order.FaMethod = eDecoder.ReadString();
                order.FaPercentage = eDecoder.ReadString();
                order.FaProfile = eDecoder.ReadString();
            }
        }

        public void ReadModelCode() 
        {
            if (serverVersion >= MinServerVer.MODELS_SUPPORT)
            {
                order.ModelCode = eDecoder.ReadString();
            }
        }

        public void ReadGoodTillDate() 
        {
            if (msgVersion >= 8)
            {
                order.GoodTillDate = eDecoder.ReadString();
            }
        }

        public void ReadRule80A() 
        {
            if (msgVersion >= 9)
            {
                order.Rule80A = eDecoder.ReadString();
            }
        }

        public void ReadPercentOffset() 
        {
            if (msgVersion >= 9)
            {
                order.PercentOffset = eDecoder.ReadDoubleMax();
            }
        }

        public void ReadSettlingFirm() 
        {
            if (msgVersion >= 9)
            {
                order.SettlingFirm = eDecoder.ReadString();
            }
        }

        public void ReadShortSaleParams() 
        {
            if (msgVersion >= 9)
            {
                order.ShortSaleSlot = eDecoder.ReadInt();
                order.DesignatedLocation = eDecoder.ReadString();
                if (serverVersion == 51)
                {
                    eDecoder.ReadInt(); // exemptCode
                }
                else if (msgVersion >= 23)
                {
                    order.ExemptCode = eDecoder.ReadInt();
                }
            }
        }

        public void ReadAuctionStrategy() 
        {
            if (msgVersion >= 9)
            {
                order.AuctionStrategy = eDecoder.ReadInt();
            }
        }

        public void ReadBoxOrderParams() 
        {
            if (msgVersion >= 9)
            {
                order.StartingPrice = eDecoder.ReadDoubleMax();
                order.StockRefPrice = eDecoder.ReadDoubleMax();
                order.Delta = eDecoder.ReadDoubleMax();
            }
        }

        public void ReadPegToStkOrVolOrderParams() 
        {
            if (msgVersion >= 9)
            {
                order.StockRangeLower = eDecoder.ReadDoubleMax();
                order.StockRangeUpper = eDecoder.ReadDoubleMax();
            }
        }

        public void ReadDisplaySize() 
        {
            if (msgVersion >= 9)
            {
                order.DisplaySize = eDecoder.ReadIntMax();
            }
        }

        public void ReadOldStyleOutsideRth() 
        {
            if (msgVersion >= 9)
            {
                if (msgVersion < 18)
                {
                    // will never happen
                    /* order.rthOnly = */
                    eDecoder.ReadBoolFromInt();
                }
            }
        }

        public void ReadBlockOrder() 
        {
            if (msgVersion >= 9)
            {
                order.BlockOrder = eDecoder.ReadBoolFromInt();
            }
        }

        public void ReadSweepToFill() 
        {
            if (msgVersion >= 9)
            {
                order.SweepToFill = eDecoder.ReadBoolFromInt();
            }
        }

        public void ReadAllOrNone() 
        {
            if (msgVersion >= 9)
            {
                order.AllOrNone = eDecoder.ReadBoolFromInt();
            }
        }

        public void ReadMinQty() 
        {
            if (msgVersion >= 9)
            {
                order.MinQty = eDecoder.ReadIntMax();
            }
        }

        public void ReadOcaType() 
        {
            if (msgVersion >= 9)
            {
                order.OcaType = eDecoder.ReadInt();
            }
        }

        public void SkipETradeOnly() 
        {
            if (msgVersion >= 9)
            {
                eDecoder.ReadBoolFromInt();
            }
        }

        public void SkipFirmQuoteOnly() 
        {
            if (msgVersion >= 9)
            {
                eDecoder.ReadBoolFromInt();
            }
        }

        public void SkipNbboPriceCap() 
        {
            if (msgVersion >= 9)
            {
                eDecoder.ReadDoubleMax();
            }
        }

        public void ReadParentId() 
        {
            if (msgVersion >= 10)
            {
                order.ParentId = eDecoder.ReadInt();
            }
        }

        public void ReadTriggerMethod() 
        {
            if (msgVersion >= 10)
            {
                order.TriggerMethod = eDecoder.ReadInt();
            }
        }

        public void ReadVolOrderParams(bool readOpenOrderAttribs) 
        {
            if (msgVersion >= 11)
            {
                order.Volatility = eDecoder.ReadDoubleMax();
                order.VolatilityType = eDecoder.ReadInt();
                if (msgVersion == 11)
                {
                    int receivedInt = eDecoder.ReadInt();
                    order.DeltaNeutralOrderType = ((receivedInt == 0) ? "NONE" : "MKT");
                }
                else
                { // msgVersion 12 and up
                    order.DeltaNeutralOrderType = eDecoder.ReadString();
                    order.DeltaNeutralAuxPrice = eDecoder.ReadDoubleMax();

                    if (msgVersion >= 27 && !Util.StringIsEmpty(order.DeltaNeutralOrderType))
                    {
                        order.DeltaNeutralConId = eDecoder.ReadInt();
                        if (readOpenOrderAttribs)
                        {
                            order.DeltaNeutralSettlingFirm = eDecoder.ReadString();
                            order.DeltaNeutralClearingAccount = eDecoder.ReadString();
                            order.DeltaNeutralClearingIntent = eDecoder.ReadString();
                        }
                    }

                    if (msgVersion >= 31 && !Util.StringIsEmpty(order.DeltaNeutralOrderType))
                    {
                        if (readOpenOrderAttribs)
                        {
                            order.DeltaNeutralOpenClose = eDecoder.ReadString();
                        }
                        order.DeltaNeutralShortSale = eDecoder.ReadBoolFromInt();
                        order.DeltaNeutralShortSaleSlot = eDecoder.ReadInt();
                        order.DeltaNeutralDesignatedLocation = eDecoder.ReadString();
                    }
                }
                order.ContinuousUpdate = eDecoder.ReadInt();
                if (serverVersion == 26)
                {
                    order.StockRangeLower = eDecoder.ReadDouble();
                    order.StockRangeUpper = eDecoder.ReadDouble();
                }
                order.ReferencePriceType = eDecoder.ReadInt();
            }
        }

        public void ReadTrailParams() 
        {
            if (msgVersion >= 13)
            {
                order.TrailStopPrice = eDecoder.ReadDoubleMax();
            }
            if (msgVersion >= 30)
            {
                order.TrailingPercent = eDecoder.ReadDoubleMax();
            }
        }

        public void ReadBasisPoints() 
        {
            if (msgVersion >= 14)
            {
                order.BasisPoints = eDecoder.ReadDoubleMax();
                order.BasisPointsType = eDecoder.ReadIntMax();
            }
        }
   
        public void ReadComboLegs() 
        {
            if (msgVersion >= 14)
            {
                contract.ComboLegsDescription = eDecoder.ReadString();
            }

            if (msgVersion >= 29)
            {
                int comboLegsCount = eDecoder.ReadInt();
                if (comboLegsCount > 0)
                {
                    contract.ComboLegs = new List<ComboLeg>(comboLegsCount);
                    for (int i = 0; i < comboLegsCount; ++i)
                    {
                        int conId = eDecoder.ReadInt();
                        int ratio = eDecoder.ReadInt();
                        string action = eDecoder.ReadString();
                        string exchange = eDecoder.ReadString();
                        int openClose = eDecoder.ReadInt();
                        int shortSaleSlot = eDecoder.ReadInt();
                        string designatedLocation = eDecoder.ReadString();
                        int exemptCode = eDecoder.ReadInt();

                        ComboLeg comboLeg = new ComboLeg(conId, ratio, action, exchange, openClose,
                                shortSaleSlot, designatedLocation, exemptCode);
                        contract.ComboLegs.Add(comboLeg);
                    }
                }

                int orderComboLegsCount = eDecoder.ReadInt();
                if (orderComboLegsCount > 0)
                {
                    order.OrderComboLegs = new List<OrderComboLeg>(orderComboLegsCount);
                    for (int i = 0; i < orderComboLegsCount; ++i)
                    {
                        double price = eDecoder.ReadDoubleMax();

                        OrderComboLeg orderComboLeg = new OrderComboLeg(price);
                        order.OrderComboLegs.Add(orderComboLeg);
                    }
                }
            }
        }

        public void ReadSmartComboRoutingParams() 
        {
            if (msgVersion >= 26)
            {
                int smartComboRoutingParamsCount = eDecoder.ReadInt();
                if (smartComboRoutingParamsCount > 0)
                {
                    order.SmartComboRoutingParams = new List<TagValue>(smartComboRoutingParamsCount);
                    for (int i = 0; i < smartComboRoutingParamsCount; ++i)
                    {
                        TagValue tagValue = new TagValue
                        {
                            Tag = eDecoder.ReadString(),
                            Value = eDecoder.ReadString()
                        };
                        order.SmartComboRoutingParams.Add(tagValue);
                    }
                }
            }
        }

        public void ReadScaleOrderParams() 
        {
            if (msgVersion >= 15)
            {
                if (msgVersion >= 20)
                {
                    order.ScaleInitLevelSize = eDecoder.ReadIntMax();
                    order.ScaleSubsLevelSize = eDecoder.ReadIntMax();
                }
                else
                {
                    /* int notSuppScaleNumComponents = */
                    eDecoder.ReadIntMax();
                    order.ScaleInitLevelSize = eDecoder.ReadIntMax();
                }
                order.ScalePriceIncrement = eDecoder.ReadDoubleMax();
            }

            if (msgVersion >= 28 && order.ScalePriceIncrement > 0.0 && order.ScalePriceIncrement != double.MaxValue)
            {
                order.ScalePriceAdjustValue = eDecoder.ReadDoubleMax();
                order.ScalePriceAdjustInterval = eDecoder.ReadIntMax();
                order.ScaleProfitOffset = eDecoder.ReadDoubleMax();
                order.ScaleAutoReset = eDecoder.ReadBoolFromInt();
                order.ScaleInitPosition = eDecoder.ReadIntMax();
                order.ScaleInitFillQty = eDecoder.ReadIntMax();
                order.ScaleRandomPercent = eDecoder.ReadBoolFromInt();
            }
        }

        public void ReadHedgeParams() 
        {
            if (msgVersion >= 24)
            {
                order.HedgeType = eDecoder.ReadString();
                if (!Util.StringIsEmpty(order.HedgeType))
                {
                    order.HedgeParam = eDecoder.ReadString();
                }
            }
        }

        public void ReadOptOutSmartRouting() 
        {
            if (msgVersion >= 25)
            {
                order.OptOutSmartRouting = eDecoder.ReadBoolFromInt();
            }
        }

        public void ReadClearingParams() 
        {
            if (msgVersion >= 19)
            {
                order.ClearingAccount = eDecoder.ReadString();
                order.ClearingIntent = eDecoder.ReadString();
            }
        }

        public void ReadNotHeld() 
        {
            if (msgVersion >= 22)
            {
                order.NotHeld = eDecoder.ReadBoolFromInt();
            }
        }

        public void ReadDeltaNeutral() 
        {
            if (msgVersion >= 20)
            {
                if (eDecoder.ReadBoolFromInt())
                {
                    DeltaNeutralContract deltaNeutralContract = new DeltaNeutralContract
                    {
                        ConId = eDecoder.ReadInt(),
                        Delta = eDecoder.ReadDouble(),
                        Price = eDecoder.ReadDouble()
                    };
                    contract.DeltaNeutralContract = deltaNeutralContract;
                }
            }
        }

        public void ReadAlgoParams() 
        {
            if (msgVersion >= 21)
            {
                order.AlgoStrategy = eDecoder.ReadString();
                if (!Util.StringIsEmpty(order.AlgoStrategy))
                {
                    int algoParamsCount = eDecoder.ReadInt();
                    if (algoParamsCount > 0)
                    {
                        order.AlgoParams = new List<TagValue>(algoParamsCount);
                        for (int i = 0; i < algoParamsCount; ++i)
                        {
                            TagValue tagValue = new TagValue
                            {
                                Tag = eDecoder.ReadString(),
                                Value = eDecoder.ReadString()
                            };
                            order.AlgoParams.Add(tagValue);
                        }
                    }
                }
            }
        }

        public void ReadSolicited() 
        {
            if (msgVersion >= 33)
            {
                order.Solicited = eDecoder.ReadBoolFromInt();
            }
        }

        public void ReadWhatIfInfoAndCommission() 
        {
            if (msgVersion >= 16)
            {
                order.WhatIf = eDecoder.ReadBoolFromInt();
                ReadOrderStatus();
                if (serverVersion >= MinServerVer.WHAT_IF_EXT_FIELDS)
                {
                    orderState.InitMarginBefore = eDecoder.ReadString();
                    orderState.MaintMarginBefore = eDecoder.ReadString();
                    orderState.EquityWithLoanBefore = eDecoder.ReadString();
                    orderState.InitMarginChange = eDecoder.ReadString();
                    orderState.MaintMarginChange = eDecoder.ReadString();
                    orderState.EquityWithLoanChange = eDecoder.ReadString();
                }
                orderState.InitMarginAfter = eDecoder.ReadString();
                orderState.MaintMarginAfter = eDecoder.ReadString();
                orderState.EquityWithLoanAfter = eDecoder.ReadString();
                orderState.Commission = eDecoder.ReadDoubleMax();
                orderState.MinCommission = eDecoder.ReadDoubleMax();
                orderState.MaxCommission = eDecoder.ReadDoubleMax();
                orderState.CommissionCurrency = eDecoder.ReadString();
                orderState.WarningText = eDecoder.ReadString();
            }

        }

        public void ReadOrderStatus() 
        {
            orderState.Status = eDecoder.ReadString();
        }

        public void ReadVolRandomizeFlags() 
        {
            if (msgVersion >= 34)
            {
                order.RandomizeSize = eDecoder.ReadBoolFromInt();
                order.RandomizePrice = eDecoder.ReadBoolFromInt();
            }
        }

        public void ReadPegToBenchParams() 
        {
            if (serverVersion >= MinServerVer.PEGGED_TO_BENCHMARK)
            {
                if (order.OrderType == "PEG BENCH")
                {
                    order.ReferenceContractId = eDecoder.ReadInt();
                    order.IsPeggedChangeAmountDecrease = eDecoder.ReadBoolFromInt();
                    order.PeggedChangeAmount = eDecoder.ReadDoubleMax();
                    order.ReferenceChangeAmount = eDecoder.ReadDoubleMax();
                    order.ReferenceExchange = eDecoder.ReadString();
                }
            }
        }

        public void ReadConditions() 
        {
            if (serverVersion >= MinServerVer.PEGGED_TO_BENCHMARK)
            {
                int nConditions = eDecoder.ReadInt();

                if (nConditions > 0)
                {
                    for (int i = 0; i < nConditions; i++)
                    {
                        OrderConditionType orderConditionType = (OrderConditionType)eDecoder.ReadInt();
                        OrderCondition condition = OrderCondition.Create(orderConditionType);

                        condition.Deserialize(eDecoder);
                        order.Conditions.Add(condition);
                    }

                    order.ConditionsIgnoreRth = eDecoder.ReadBoolFromInt();
                    order.ConditionsCancelOrder = eDecoder.ReadBoolFromInt();
                }
            }

        }

        public void ReadAdjustedOrderParams() 
        {
            if (serverVersion >= MinServerVer.PEGGED_TO_BENCHMARK)
            {
                order.AdjustedOrderType = eDecoder.ReadString();
                order.TriggerPrice = eDecoder.ReadDoubleMax();
                ReadStopPriceAndLmtPriceOffset();
                order.AdjustedStopPrice = eDecoder.ReadDoubleMax();
                order.AdjustedStopLimitPrice = eDecoder.ReadDoubleMax();
                order.AdjustedTrailingAmount = eDecoder.ReadDoubleMax();
                order.AdjustableTrailingUnit = eDecoder.ReadInt();
            }
        }

        public void ReadStopPriceAndLmtPriceOffset() 
        {
            order.TrailStopPrice = eDecoder.ReadDoubleMax();
            order.LmtPriceOffset = eDecoder.ReadDoubleMax();
        }

        public void ReadSoftDollarTier() 
        {
            if (serverVersion >= MinServerVer.SOFT_DOLLAR_TIER)
            {
                order.Tier = new SoftDollarTier(eDecoder.ReadString(), eDecoder.ReadString(), eDecoder.ReadString());
            }
        }

        public void ReadCashQty() 
        {
            if (serverVersion >= MinServerVer.CASH_QTY)
            {
                order.CashQty = eDecoder.ReadDoubleMax();
            }
        }

        public void ReadDontUseAutoPriceForHedge() 
        {
            if (serverVersion >= MinServerVer.AUTO_PRICE_FOR_HEDGE)
            {
                order.DontUseAutoPriceForHedge = eDecoder.ReadBoolFromInt();
            }
        }

        public void ReadIsOmsContainer() 
        {
            if (serverVersion >= MinServerVer.ORDER_CONTAINER)
            {
                order.IsOmsContainer = eDecoder.ReadBoolFromInt();
            }
        }

        public void ReadDiscretionaryUpToLimitPrice() 
        {
            if (serverVersion >= MinServerVer.D_PEG_ORDERS)
            {
                order.DiscretionaryUpToLimitPrice = eDecoder.ReadBoolFromInt();
            }
        }

        public void ReadAutoCancelDate() 
        {
            order.AutoCancelDate = eDecoder.ReadString();
        }

        public void ReadFilledQuantity() 
        {
            order.FilledQuantity = eDecoder.ReadDecimal();
        }

        public void ReadRefFuturesConId() 
        {
            order.RefFuturesConId = eDecoder.ReadInt();
        }

        public void ReadAutoCancelParent() 
        {
            ReadAutoCancelParent(Constants.MinVersion);
        }

        public void ReadAutoCancelParent(int minVersionAutoCancelParent)
        {
            if (serverVersion >= minVersionAutoCancelParent)
            {
                order.AutoCancelParent = eDecoder.ReadBoolFromInt();
            }
        }

        public void ReadShareholder() 
        {
            order.Shareholder = eDecoder.ReadString();
        }

        public void ReadImbalanceOnly() 
        {
            order.ImbalanceOnly = eDecoder.ReadBoolFromInt();
        }

        public void ReadRouteMarketableToBbo() 
        {
            order.RouteMarketableToBbo = eDecoder.ReadBoolFromInt();
        }

        public void ReadParentPermId() 
        {
            order.ParentPermId = eDecoder.ReadLong();
        }

        public void ReadCompletedTime() 
        {
            orderState.CompletedTime = eDecoder.ReadString();
        }

        public void ReadCompletedStatus() 
        {
            orderState.CompletedStatus = eDecoder.ReadString();
        }
        
        public void ReadUsePriceMgmtAlgo()
        {
            if (serverVersion >= MinServerVer.PRICE_MGMT_ALGO)
            {
                order.UsePriceMgmtAlgo = eDecoder.ReadBoolFromInt();
            }
        }

        public void ReadDuration()
        {
            if (serverVersion >= MinServerVer.DURATION)
            {
                order.Duration = eDecoder.ReadIntMax();
            }
        }

        public void ReadPostToAts()
        {
            if (serverVersion >= MinServerVer.POST_TO_ATS)
            {
                order.PostToAts = eDecoder.ReadIntMax();
            }
        }

        public void ReadPegBestPegMidOrderAttributes()
        {
            if (serverVersion >= MinServerVer.PEGBEST_PEGMID_OFFSETS)
            {
                order.MinTradeQty = eDecoder.ReadIntMax();
                order.MinCompeteSize = eDecoder.ReadIntMax();
                order.CompeteAgainstBestOffset = eDecoder.ReadDoubleMax();
                order.MidOffsetAtWhole = eDecoder.ReadDoubleMax();
                order.MidOffsetAtHalf = eDecoder.ReadDoubleMax();
            }
        }
    }
}
