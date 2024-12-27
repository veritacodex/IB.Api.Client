namespace IB.Api.Client.Implementation.Model
{
    public class RealTimeBarUpdate
    {
        public long Date { get; internal set; }
        public double Open { get; internal set; }
        public double High { get; internal set; }
        public double Low { get; internal set; }
        public double Close { get; internal set; }
        public decimal Volume { get; internal set; }
        public int Count { get; internal set; }
        public decimal Vwap { get; internal set; }
        public BarAnnotation BarAnnotation { get; internal set; }
    }
}
