/* Copyright (C) 2024 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System.Collections.Generic;

namespace IBApi
{
    /**
     * @class OrderCancel
     */
    public class OrderCancel
    {
        public static string EMPTY_STR = "";

        /**
         * @brief This is a regulartory attribute that applies to all US Commodity (Futures) Exchanges, provided to allow client to comply with CFTC Tag 50 Rules
         */
        public string ExtOperator { get; set; }

        /**
         * @brief Used by brokers and advisors when manually entering, modifying or cancelling orders at the direction of a client.
         * <i>Only used when allocating orders to specific groups or accounts. Excluding "All" group.</i>
         */
        public string ManualOrderCancelTime { get; set; }

        /**
         * @brief External User Id
         */
        public string ExternalUserId { get; set; }

        /**
         * @brief Manual Order Indicator
         */
        public int ManualOrderIndicator { get; set; }

        public OrderCancel()
        {
            ManualOrderCancelTime = EMPTY_STR;
            ExtOperator = EMPTY_STR;
            ExternalUserId = EMPTY_STR;
            ManualOrderIndicator = int.MaxValue;
        }

        public override bool Equals(object p_other)
        {
            if (this == p_other)
                return true;

            if (!(p_other is OrderCancel l_theOther))
                return false;

            if (ManualOrderIndicator != l_theOther.ManualOrderIndicator)
            {
                return false;
            }

            if (
                Util.StringCompare(ManualOrderCancelTime, l_theOther.ManualOrderCancelTime) != 0 ||
                Util.StringCompare(ExtOperator, l_theOther.ExtOperator) != 0 ||
                Util.StringCompare(ExternalUserId, l_theOther.ExternalUserId) != 0)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = 1040337091;
            hashCode *= -1521134295 + EqualityComparer<string>.Default.GetHashCode(ManualOrderCancelTime);
            hashCode *= -1521134295 + EqualityComparer<string>.Default.GetHashCode(ExtOperator);
            hashCode *= -1521134295 + EqualityComparer<string>.Default.GetHashCode(ExternalUserId);
            hashCode *= -1521134295 + ManualOrderIndicator.GetHashCode();

            return hashCode;
        }
    }
}
