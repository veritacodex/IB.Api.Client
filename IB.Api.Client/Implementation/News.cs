using System;
using IB.Api.Client.IBKR;

namespace IB.Api.Client.Implementation
{
    //News
    public partial class IbClient
    {
        public event EventHandler<NewsProvider[]> NewsProvidersUpdateReceived;
        public void ReqNewsProviders()
        {
            ClientSocket.reqNewsProviders();
        }
        void IEWrapper.newsProviders(NewsProvider[] newsProviders)
        {
            NewsProvidersUpdateReceived?.Invoke(this, newsProviders);
        }        
        void IEWrapper.tickNews(int tickerId, long timeStamp, string providerCode, string articleId, string headline, string extraData)
        {
            DiscardImplementation(tickerId, timeStamp, providerCode, articleId, headline, extraData);
        }
        void IEWrapper.updateNewsBulletin(int msgId, int msgType, string message, string origExchange)
        {
            var output = $"MsgId:{msgId} MsType:{msgType} Message:{message} Origin:{origExchange}";
            Notify(output);
        }
    }
}
