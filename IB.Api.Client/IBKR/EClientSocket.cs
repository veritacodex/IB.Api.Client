/* Copyright (C) 2024 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IB.Api.Client.IBKR
{
    /**
     * @class EClientSocket
     * @brief TWS/Gateway client class
     * This client class contains all the available methods to communicate with IB. Up to 32 clients can be connected to a single instance of the TWS/Gateway simultaneously. From herein, the TWS/Gateway will be referred to as the Host.
     */
    public class EClientSocket : EClient, EClientMsgSink
    {
        private int port;
        private TcpClient tcpClient;

        public EClientSocket(IEWrapper wrapper, EReaderSignal eReaderSignal) :
            base(wrapper) => this.eReaderSignal = eReaderSignal;

        void EClientMsgSink.serverVersion(int version, string time)
        {
            base.serverVersion = version;

            if (!useV100Plus)
            {
                if (!CheckServerVersion(MinServerVer.MIN_VERSION, ""))
                {
                    ReportUpdateTWS("");
                    return;
                }
            }
            else if (serverVersion < Constants.MinVersion || serverVersion > Constants.MaxVersion)
            {
                wrapper.error(clientId, EClientErrors.UNSUPPORTED_VERSION.Code, EClientErrors.UNSUPPORTED_VERSION.Message, "");
                return;
            }

            if (serverVersion >= 3)
            {
                if (serverVersion < MinServerVer.LINKING)
                {
                    var buf = new List<byte>();

                    buf.AddRange(Encoding.UTF8.GetBytes(clientId.ToString()));
                    buf.Add(Constants.EOL);
                    socketTransport.Send(new EMessage(buf.ToArray()));
                }
            }

            ServerTime = time;
            isConnected = true;

            if (!AsyncEConnect)
                startApi();
        }

        /**
         * @brief Establishes a connection to the designated Host. This earlier version of eConnect does not have extraAuth parameter.
         */
        public void eConnect(string host, int lport, int lclientId) => eConnect(host, lport, lclientId, false);

        protected virtual Stream createClientStream(string host, int lport)
        {
            tcpClient = new TcpClient(host, lport);
            tcpClient.NoDelay = true;

            return tcpClient.GetStream();
        }

        

        /**
         * @brief Establishes a connection to the designated Host.
         * After establishing a connection successfully, the Host will provide the next valid order id, server's current time, managed accounts and open orders among others depending on the Host version.
         * @param host the Host's IP address. Leave blank for localhost.
         * @param port the Host's port. 7496 by default for the TWS, 4001 by default on the Gateway.
         * @param clientId Every API client program requires a unique id which can be any integer. Note that up to 32 clients can be connected simultaneously to a single Host.
         * @sa EWrapper, EWrapper::nextValidId, EWrapper::currentTime
         */
        private void eConnect(string host, int lport, int lclientId, bool lextraAuth)
        {
            try
            {
                validateInvalidSymbols(host);
            }
            catch (EClientException e)
            {
                wrapper.error(IncomingMessage.NotValid, e.Err.Code, e.Err.Message + e.Text, "");
                return;
            }

            if (isConnected)
            {
                wrapper.error(IncomingMessage.NotValid, EClientErrors.AlreadyConnected.Code, EClientErrors.AlreadyConnected.Message, "");
                return;
            }
            try
            {
                tcpStream = createClientStream(host, lport);
                this.port = lport;
                socketTransport = new ESocket(tcpStream);

                this.clientId = lclientId;
                this.extraAuth = lextraAuth;

                sendConnectRequest();

                if (!AsyncEConnect)
                {
                    var eReader = new EReader(this, eReaderSignal);

                    while (serverVersion == 0 && eReader.putMessageToQueue())
                    {
                        eReaderSignal.waitForSignal();
                        eReader.processMsgs();
                    }
                }
            }
            catch (ArgumentNullException ane)
            {
                wrapper.error(ane);
            }
            catch (SocketException se)
            {
                wrapper.error(se);
            }
            catch (EClientException e)
            {
                var cmp = e.Err;

                wrapper.error(-1, cmp.Code, cmp.Message, "");
            }
            catch (Exception e)
            {
                wrapper.error(e);
            }
        }

        private readonly EReaderSignal eReaderSignal;
        private int redirectCount;

        protected override uint prepareBuffer(BinaryWriter paramsList)
        {
            var rval = (uint)paramsList.BaseStream.Position;

            if (useV100Plus)
                paramsList.Write(0);

            return rval;
        }

        protected override void CloseAndSend(BinaryWriter request, uint lengthPos)
        {
            if (useV100Plus)
            {
                request.Seek((int)lengthPos, SeekOrigin.Begin);
                request.Write(IPAddress.HostToNetworkOrder((int)(request.BaseStream.Length - lengthPos - sizeof(int))));
            }

            request.Seek(0, SeekOrigin.Begin);

            var buf = new MemoryStream();

            request.BaseStream.CopyTo(buf);
            socketTransport.Send(new EMessage(buf.ToArray()));
        }

        /**
        * @brief Redirects connection to different host.
        */
        public void redirect(string host)
        {
            if (!allowRedirect)
            {
                wrapper.error(clientId, EClientErrors.CONNECT_FAIL.Code, EClientErrors.CONNECT_FAIL.Message, "");
                return;
            }

            var srv = host.Split(':');

            if (srv.Length > 1 && !int.TryParse(srv[1], out port)) throw new EClientException(EClientErrors.BAD_MESSAGE);

            ++redirectCount;

            if (redirectCount > Constants.REDIRECT_COUNT_MAX)
            {
                eDisconnect();
                wrapper.error(clientId, EClientErrors.CONNECT_FAIL.Code, "Redirect count exceeded", "");
                return;
            }

            eDisconnect(false);
            eConnect(srv[0], port, clientId, extraAuth);
        }

        public override void eDisconnect(bool resetState = true)
        {
            if (resetState)
            {
                redirectCount = 0;
            }
            tcpClient = null;
            base.eDisconnect(resetState);
        }

        /**
         * @brief Determines the status of the tcpClient with SelectMode.SelectRead.
         * @param timeout The time to wait for a response, in microseconds.
         * @returns true if any of the following conditions occur before the timeout expires, otherwise, false.
         * @sa EWrapper::connectionClosed
         */
        internal bool Poll(int timeout)
        {
            return tcpClient.Client.Poll(timeout, SelectMode.SelectRead);
        }
    }
}
