/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System;

namespace IB.Api.Client.IBKR
{
    public abstract class ContractCondition : OperatorCondition
    {
        private int ConId { get; set; }
        private string Exchange { get; set; }

        private const string DELIMITER = " of ";

        private Func<int, string, string> ContractResolver { get; set; }

        protected ContractCondition() => ContractResolver = (conid, exch) => $"{conid}({exch})";

        public override string ToString() => Type + DELIMITER + ContractResolver(ConId, Exchange) + base.ToString();

        public override bool Equals(object obj)
        {
            if (obj is not ContractCondition other)
                return false;

            return base.Equals(obj)
                && ConId == other.ConId
                && Exchange.Equals(other.Exchange, StringComparison.Ordinal);
        }

        public override int GetHashCode() => base.GetHashCode() + ConId.GetHashCode() + Exchange.GetHashCode();

        protected override bool TryParse(string cond)
        {
            try
            {
                if (cond[..cond.IndexOf(DELIMITER, StringComparison.Ordinal)] != Type.ToString())
                    return false;

                cond = cond[(cond.IndexOf(DELIMITER, StringComparison.Ordinal) + DELIMITER.Length)..];

                if (!int.TryParse(cond.AsSpan(0, cond.IndexOf('(')), out var conid))
                    return false;

                ConId = conid;
                cond = cond[(cond.IndexOf('(') + 1)..];
                Exchange = cond[..cond.IndexOf(')')];
                cond = cond[(cond.IndexOf(')') + 1)..];

                return base.TryParse(cond);
            }
            catch
            {
                return false;
            }
        }

        public override void Deserialize(IDecoder inStream)
        {
            base.Deserialize(inStream);

            ConId = inStream.ReadInt();
            Exchange = inStream.ReadString();
        }

        public override void Serialize(System.IO.BinaryWriter outStream)
        {
            base.Serialize(outStream);
            outStream.AddParameter(ConId);
            outStream.AddParameter(Exchange);
        }
    }
}
