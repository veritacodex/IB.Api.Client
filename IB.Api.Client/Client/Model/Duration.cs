using System;

namespace IB.Api.Client.Model
{
    /// <summary>
    /// Ref: https://interactivebrokers.github.io/tws-api/historical_bars.html#hd_duration
    /// </summary>
    public enum DurationType
    {
        S,
        D,
        W,
        M,
        Y
    }

    public static class Duration
    {
        public static string GetDuration(int unit, DurationType durationType){
            return $"{unit} {durationType}";
        }
    }
}
