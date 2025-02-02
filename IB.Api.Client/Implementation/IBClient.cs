using IB.Api.Client.IBKR;

namespace IB.Api.Client.Implementation
{
    //Main
    public partial class IbClient : IEWrapper
    {
        internal readonly EClientSocket ClientSocket;
        internal readonly EReaderSignal Signal;
        public IbClient()
        {
            Signal = new EReaderMonitorSignal();
            ClientSocket = new EClientSocket(this, Signal);
        }

        public bool IsConnected()
        {
            return ClientSocket.IsConnected();
        }

        void IEWrapper.connectAck()
        {
            Notify("Connection Acknowledged");
        }
        void IEWrapper.connectionClosed()
        {
            Notify("Connection Closed");
        }
    }
}