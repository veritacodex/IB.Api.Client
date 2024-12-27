using System;
using IBApi;

namespace IB.Api.Client.Implementation
{
    //News
    public partial class IBClient
    {
        public event EventHandler<NewsProvider[]> NewsProvidersUpdateReceived;
        public void GetNewsProviders()
        {
            ClientSocket.reqNewsProviders();
        }
        void EWrapper.newsProviders(NewsProvider[] newsProviders)
        {
            NewsProvidersUpdateReceived?.Invoke(this, newsProviders);
        }        
        void EWrapper.tickNews(int tickerId, long timeStamp, string providerCode, string articleId, string headline, string extraData)
        {
            _ = tickerId;
            _ = timeStamp;
            _ = providerCode;
            _ = articleId;
            _ = headline;
            _ = extraData;
            Notify("");
        }
        void EWrapper.updateNewsBulletin(int msgId, int msgType, string message, string origExchange)
        {
            var output = $"MsgId:{msgId} MsType:{msgType} Message:{message} Origin:{origExchange}";
            Notify(output);
        }
    }
}
