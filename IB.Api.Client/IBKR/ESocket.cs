/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */
using System;
using System.IO;

namespace IBApi
{
    class ESocket(Stream socketStream) : IETransport, IDisposable
    {
        readonly BinaryWriter tcpWriter = new BinaryWriter(socketStream);
        readonly object tcpWriterLock = new object();

        public void Send(EMessage msg)
        {
            lock (tcpWriterLock)
            {
                tcpWriter.Write(msg.GetBuf());
            }
        }

        public void Dispose()
        {
            tcpWriter.Dispose();
        }
    }
}
