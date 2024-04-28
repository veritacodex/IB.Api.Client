/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System.Runtime.InteropServices;

namespace IBApi
{
	/**
     * @class HistoricalTickBidAsk
     * @brief The historical tick's description. Used when requesting historical tick data with whatToShow = BID_ASK
     * @sa EClient, EWrapper
     */
    [ComVisible(true)]
    public class HistoricalTickBidAsk
    {
        /**
         * @brief The UNIX timestamp of the historical tick 
         */
        public long Time 
        {
            [return:MarshalAs(UnmanagedType.I8)]
            get;
            [param:MarshalAs(UnmanagedType.I8)]
            set; 
        }
		
		/**
         * @brief Tick attribs of historical bid/ask tick
         */
        public TickAttribBidAsk TickAttribBidAsk { get; set; }
		
		/**
         * @brief The bid price of the historical tick
         */
        public double PriceBid { get; set; }
		
		/**
         * @brief The ask price of the historical tick 
         */
        public double PriceAsk { get; set; }
		
		/**
         * @brief The bid size of the historical tick 
         */
        public decimal SizeBid
        {
            [return: MarshalAs(UnmanagedType.I8)]
            get;
            [param: MarshalAs(UnmanagedType.I8)]
            set;
        }
		
		/**
         * @brief The ask size of the historical tick 
         */
        public decimal SizeAsk
        {
            [return: MarshalAs(UnmanagedType.I8)]
            get;
            [param: MarshalAs(UnmanagedType.I8)]
            set;
        }
    }
}
