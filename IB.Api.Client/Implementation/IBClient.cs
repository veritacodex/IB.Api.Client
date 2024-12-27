using IBApi;

namespace IB.Api.Client
{
    //Main
    public partial class IBClient : EWrapper
    {
        internal readonly EClientSocket ClientSocket;
        internal readonly IEReaderSignal Signal;
        public IBClient()
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