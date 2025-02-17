﻿/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

namespace IB.Api.Client.IBKR
{
    /**
     * @brief Time condition used in conditional orders to submit or cancel orders at specified time. 
     */
    public class TimeCondition : OperatorCondition
    {
        private const string header = "time";

        protected override string Value
        {
            get => Time;
            set => Time = value;
        }

        public override string ToString() => header + base.ToString();

        /**
         * @brief Time field used in conditional order logic. Valid format: YYYYMMDD HH:MM:SS
         */

        public string Time { get; set; }

        protected override bool TryParse(string cond)
        {
            if (!cond.StartsWith(header))
                return false;

            cond = cond.Replace(header, "");
            return base.TryParse(cond);
        }
    }
}
