﻿/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System.Threading;

namespace IBApi
{
    public class EReaderMonitorSignal : IEReaderSignal
    {
        readonly object cs = new();
        bool open;

        public void IssueSignal()
        {
            lock (cs)
            {
                open = true;

                Monitor.PulseAll(cs);
            }
        }

        public void WaitForSignal()
        {
            lock (cs)
            {
                while (!open)
                    Monitor.Wait(cs);

                open = false;
            }
        }
    }
}
