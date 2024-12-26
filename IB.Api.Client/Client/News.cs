using System;
using IB.Api.Client.Client.Model;
using IBApi;

namespace IB.Api.Client
{
    //News
    public partial class IBClient
    {
        public event EventHandler<NewsProvider[]> NewsProvidersUpdateReceived;
        public event EventHandler<NewsItem> NewsItemReceived;
        public void GetNewsProviders()
        {
            ClientSocket.reqNewsProviders();
        }

        void IEWrapper.newsProviders(NewsProvider[] newsProviders)
        {
            NewsProvidersUpdateReceived?.Invoke(this, newsProviders);
        }
        void IEWrapper.tickNews(int tickerId, long timeStamp, string providerCode, string articleId, string headline, string extraData)
        {
            var newsItem = new NewsItem
            {
                TickerId = tickerId,
                TimeStamp = timeStamp,
                ProviderCode = providerCode,
                ArticleId = articleId,
                Headline = headline,
                ExtraData = extraData
            };
            NewsItemReceived?.Invoke(this, newsItem);
        }
        void IEWrapper.updateNewsBulletin(int msgId, int msgType, string message, string origExchange)
        {
            var output = $"MsgId:{msgId} MsType:{msgType} Message:{message} Origin:{origExchange}";
            Notify(output);
        }
    }
}
