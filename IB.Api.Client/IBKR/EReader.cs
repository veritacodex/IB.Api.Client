/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */
using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace IBApi
{
    /**
    * @brief Captures incoming messages to the API client and places them into a queue.
    */
    public class EReader
    {
        readonly EClientSocket eClientSocket;
        readonly IEReaderSignal eReaderSignal;
        readonly Queue<EMessage> msgQueue = new Queue<EMessage>();
        readonly EDecoder processMsgsDecoder;
        const int defaultInBufSize = ushort.MaxValue / 8;

        bool UseV100Plus
        {
            get
            {
                return eClientSocket.UseV100Plus;
            }
        }


        static readonly IEWrapper defaultWrapper = new DefaultEWrapper();

        public EReader(EClientSocket clientSocket, IEReaderSignal signal)
        {
            eClientSocket = clientSocket;
            eReaderSignal = signal;
            processMsgsDecoder = new EDecoder(eClientSocket.ServerVersion, eClientSocket.Wrapper, eClientSocket);
        }

        public void Start()
        {
            new Thread(() =>
            {
                try
                {
                    while (eClientSocket.IsConnected())
                    {
                        if (!eClientSocket.IsDataAvailable())
                        {
                            Thread.Sleep(1);
                            continue;
                        }

                        if (!PutMessageToQueue())
                            break;
                    }
                }
                catch (Exception ex)
                {
                    eClientSocket.Wrapper.Error(ex);
                    eClientSocket.EDisconnect();
                }

                eReaderSignal.IssueSignal();
            }) { IsBackground = true }.Start();
        }

        EMessage GetMsg()
        {
            lock (msgQueue)
                return msgQueue.Count == 0 ? null : msgQueue.Dequeue();
        }

        public void ProcessMsgs()
        {
            EMessage msg = GetMsg();

            while (msg != null && processMsgsDecoder.ParseAndProcessMsg(msg.GetBuf()) > 0)
                msg = GetMsg();
        }

        public bool PutMessageToQueue()
        {
            try
            {
                EMessage msg = ReadSingleMessage();

                if (msg == null)
                    return false;

                lock (msgQueue)
                    msgQueue.Enqueue(msg);

                eReaderSignal.IssueSignal();

                return true;
            }
            catch (Exception ex)
            {
                if (eClientSocket.IsConnected())
                    eClientSocket.Wrapper.Error(ex);

                return false;
            }
        }

        readonly List<byte> inBuf = new List<byte>(defaultInBufSize);

        private EMessage ReadSingleMessage()
        {
            int msgSize;
            if (UseV100Plus)
            {
                msgSize = eClientSocket.ReadInt();

                if (msgSize > Constants.MaxMsgSize)
                {
                    throw new EClientException(EClientErrors.BAD_LENGTH);
                }

                return new EMessage(eClientSocket.ReadByteArray(msgSize));
            }

            if (inBuf.Count == 0)
                AppendInBuf();

            while (true)
                try
                {
                    msgSize = new EDecoder(eClientSocket.ServerVersion, defaultWrapper).ParseAndProcessMsg(inBuf.ToArray());
                    break;
                }
                catch (EndOfStreamException)
                {
                    if (inBuf.Count >= inBuf.Capacity * 3/4)
                        inBuf.Capacity *= 2;

                    AppendInBuf();
                }

            var msgBuf = new byte[msgSize];

            inBuf.CopyTo(0, msgBuf, 0, msgSize);
            inBuf.RemoveRange(0, msgSize);

            if (inBuf.Count < defaultInBufSize && inBuf.Capacity > defaultInBufSize)
                inBuf.Capacity = defaultInBufSize;

            return new EMessage(msgBuf);
        }

        private void AppendInBuf()
        {
            inBuf.AddRange(eClientSocket.ReadAtLeastNBytes(inBuf.Capacity - inBuf.Count));
        }
    }
}
