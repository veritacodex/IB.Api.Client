/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */
using System.IO;
using System.Net.Security;

namespace IBApi
{
    /**
    * @brief Implements a Secure Socket Layer (SSL) on top of the EClientSocket class.
    */
    public class EClientSocketSSL : EClientSocket
    {
        public EClientSocketSSL(IEWrapper wrapper, IEReaderSignal signal) :
            base(wrapper, signal)
        { }

        protected override Stream CreateClientStream(string host, int port)
        {
            var rval = new SslStream(base.CreateClientStream(host, port), false, (_, __, ___, ____) => true);

            rval.AuthenticateAsClient(host);

            return rval;
        }
    }
}
