namespace IB.Api.Client.Implementation.Model;

public class NewsItem
{
    public int TickerId { get; internal set; }
    public long TimeStamp { get; internal set; }
    public string ProviderCode { get; internal set; }
    public string ArticleId { get; internal set; }
    public string Headline { get; internal set; }
    public string ExtraData { get; internal set; }
}
