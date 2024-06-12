using IBApi;

namespace IB.Api.Client
{
    //Main
    public partial class IBClient : IEWrapper
    {
        internal readonly EClientSocket ClientSocket;
        internal readonly EReaderSignal Signal;
        public IBClient()
        {
            Signal = new EReaderMonitorSignal();
            ClientSocket = new EClientSocket(this, Signal);
        }

        public bool IsConnected()
        {
            return ClientSocket.IsConnected();
        }

        void IEWrapper.ConnectAck()
        {
            Notify("Connection Acknowledged");
        }
        void IEWrapper.ConnectionClosed()
        {
            Notify("Connection Closed");
        }
    }
}