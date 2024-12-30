using IB.Api.Client.IBKR;

namespace IB.Api.Client.Implementation
{
    //Main
    public partial class IbClient : EWrapper
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

        void EWrapper.connectAck()
        {
            Notify("Connection Acknowledged");
        }
        void EWrapper.connectionClosed()
        {
            Notify("Connection Closed");
        }
    }
}