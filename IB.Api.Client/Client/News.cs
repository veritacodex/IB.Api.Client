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
        public void NewsProviders(NewsProvider[] newsProviders)
        {
            NewsProvidersUpdateReceived?.Invoke(this, newsProviders);
        }
        public void TickNews(int tickerId, long timeStamp, string providerCode, string articleId, string headline, string extraData)
        {
        }
        public void UpdateNewsBulletin(int msgId, int msgType, string message, string origExchange)
        {
            var output = $"MsgId:{msgId} MsType:{msgType} Message:{message} Origin:{origExchange}";
            Notify(output);
        }
    }
}
