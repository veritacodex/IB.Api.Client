﻿/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System;

namespace IBApi
{
	public abstract class ContractCondition : OperatorCondition
    {
        public int ConId { get; set; }
        public string Exchange { get; set; }

        const string delimiter = " of ";

        public Func<int, string, string> ContractResolver { get; set; }

        protected ContractCondition()
        {
            ContractResolver = (conid, exch) => conid + "(" + exch + ")";
        }

        public override string ToString()
        {
            return Type + delimiter + ContractResolver(ConId, Exchange) + base.ToString();
        }

        public override bool Equals(object obj)
        {
            var other = obj as ContractCondition;

            if (other == null)
                return false;

            return base.Equals(obj)
                && ConId == other.ConId
                && Exchange.Equals(other.Exchange);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + ConId.GetHashCode() + Exchange.GetHashCode();
        }

        protected override bool TryParse(string cond)
        {
            try
            {
                if (cond[..cond.IndexOf(delimiter)] != Type.ToString())
                    return false;

                cond = cond[(cond.IndexOf(delimiter) + delimiter.Length)..];
                int conid;

                if (!int.TryParse(cond.AsSpan(0, cond.IndexOf('(')), out conid))
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
