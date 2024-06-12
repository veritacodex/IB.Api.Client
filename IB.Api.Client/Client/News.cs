using System;
using IBApi;

namespace IB.Api.Client
{
    //News
    public partial class IBClient
    {
        public event EventHandler<NewsProvider[]> NewsProvidersUpdateReceived;
        public void GetNewsProviders()
        {
            ClientSocket.ReqNewsProviders();
        }

        void IEWrapper.NewsProviders(NewsProvider[] newsProviders)
        {
            NewsProvidersUpdateReceived?.Invoke(this, newsProviders);
        }
        void IEWrapper.TickNews(int tickerId, long timeStamp, string providerCode, string articleId, string headline, string extraData)
        {
            _ = string.Empty;
        }
        void IEWrapper.UpdateNewsBulletin(int msgId, int msgType, string message, string origExchange)
        {
            var output = $"MsgId:{msgId} MsType:{msgType} Message:{message} Origin:{origExchange}";
            Notify(output);
        }
    }
}
