namespace IB.Api.Client.Model
{
    /// <summary>
    /// Ref: https://interactivebrokers.github.io/tws-api/historical_bars.html#hd_barsize
    /// </summary>
    public enum BarSizeType
    {
        secs,
        min,
        mins,
        hour,
        hours,
        day,
        week,
        month
    }
    public static class BarSize
    {
        public static string GetBarSize(int unit, BarSizeType barSizeType)
        {
            return $"{unit} {barSizeType}";
        }
    }
}
