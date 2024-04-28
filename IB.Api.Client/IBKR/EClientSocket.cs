/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IBApi
{
    /**
     * @class EClientSocket
     * @brief TWS/Gateway client class
     * This client class contains all the available methods to communicate with IB. Up to 32 clients can be connected to a single instance of the TWS/Gateway simultaneously. From herein, the TWS/Gateway will be referred to as the Host.
     */
    public class EClientSocket : EClient,  IEClientMsgSink
    {
        private int port;

        public EClientSocket(IEWrapper wrapper, IEReaderSignal eReaderSignal):
            base(wrapper)
        {
            this.eReaderSignal = eReaderSignal;
        }

        void IEClientMsgSink.ServerVersion(int version, string time)
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
                wrapper.Error(clientId, EClientErrors.UNSUPPORTED_VERSION.Code, EClientErrors.UNSUPPORTED_VERSION.Message, "");
                return;
            }

            if (serverVersion >= 3)
            {
                if (serverVersion < MinServerVer.LINKING)
                {
                    List<byte> buf = new List<byte>();

                    buf.AddRange(Encoding.UTF8.GetBytes(clientId.ToString()));
                    buf.Add(Constants.EOL);
                    socketTransport.Send(new EMessage(buf.ToArray()));
                }
            }

            ServerTime = time;
            isConnected = true;

            if (!AsyncEConnect)
                StartApi();
        }

        /**
        * Creates socket connection to TWS/IBG. This earlier version of eConnect does not have extraAuth parameter.
        */
        public void EConnect(string host, int port, int clientId)
        {
            EConnect(host, port, clientId, false);
        }

        protected virtual Stream CreateClientStream(string host, int port)
        {
            return new TcpClient(host, port).GetStream();
        }

        /**
        * @brief Creates socket connection to TWS/IBG.
        */
        public void EConnect(string host, int port, int clientId, bool extraAuth)
        {
            if (isConnected)
            {
                wrapper.Error(IncomingMessage.NotValid, EClientErrors.AlreadyConnected.Code, EClientErrors.AlreadyConnected.Message, "");
                return;
            }
            try
            {
                tcpStream = CreateClientStream(host, port);
                this.port = port;
                socketTransport = new ESocket(tcpStream);

                this.clientId = clientId;
                this.extraAuth = extraAuth;

                SendConnectRequest();

                if (!AsyncEConnect)
                {
                    var eReader = new EReader(this, eReaderSignal);

                    while (serverVersion == 0 && eReader.PutMessageToQueue())
                    {
                        eReaderSignal.WaitForSignal();
                        eReader.ProcessMsgs();
                    }
                }
            }
            catch (ArgumentNullException ane)
            {
                wrapper.Error(ane);
            }
            catch (SocketException se)
            {
                wrapper.Error(se);
            }
            catch (EClientException e)
            {
                var cmp = (e as EClientException).Err;

                wrapper.Error(-1, cmp.Code, cmp.Message, "");
            }
            catch (Exception e)
            {
                wrapper.Error(e);
            }
        }

        private readonly IEReaderSignal eReaderSignal;
        private int redirectCount;

        protected override uint PrepareBuffer(BinaryWriter paramsList)
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
        public void Redirect(string host)
        {
            if (!allowRedirect)
            {
                wrapper.Error(clientId, EClientErrors.CONNECT_FAIL.Code, EClientErrors.CONNECT_FAIL.Message, "");
                return;
            }

            var srv = host.Split(':');

            if (srv.Length > 1)
                if (!int.TryParse(srv[1], out port))
                    throw new EClientException(EClientErrors.BAD_MESSAGE);


            ++redirectCount;

            if (redirectCount > Constants.REDIRECT_COUNT_MAX)
            {
                EDisconnect();
                wrapper.Error(clientId, EClientErrors.CONNECT_FAIL.Code, "Redirect count exceeded", "");
                return;
            }

            EDisconnect(false);
            EConnect(srv[0], port, clientId, extraAuth);

            return;
        }

        public override void EDisconnect(bool resetState = true)
        {
            if (resetState)
            {
                redirectCount = 0;
            }
            base.EDisconnect(resetState);            
        }
    }
}
