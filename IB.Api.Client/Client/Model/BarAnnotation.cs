namespace IB.Api.Client.Model
{
    /// <summary>
    /// Annotation to hold Tradingview markers.
    /// Ref: https://tradingview.github.io/lightweight-charts/tutorials/how_to/series-markers
    /// </summary>
    public class BarAnnotation
    {
        //time should be set with the bar's time/date
        public string Position { get; set; }
        public string Color { get; set; }
        public string Shape { get; set; }
        public string Text { get; set; }
    }
}