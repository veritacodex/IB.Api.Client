using System.Collections.Generic;
using System.Text;

namespace IB.Api.Client.Model
{
    public class OptionParameterDefinition
    {
        public string Exchange { get; internal set; }
        public int UnderlyingConId { get; internal set; }
        public string TradingClass { get; internal set; }
        public string Multiplier { get; internal set; }
        public HashSet<string> Expirations { get; internal set; }
        public HashSet<double> Strikes { get; internal set; }

        public override string ToString()
        {
            var output = new StringBuilder();
            output.AppendLine($"Exchange:{Exchange} UnderlyingConId:{UnderlyingConId} TradingClass:{TradingClass} Multiplier:{Multiplier}");
            output.AppendLine("Expirations:");
            foreach (var expiration in Expirations)
            {
                output.AppendLine($"\t{expiration}");
            }
            output.AppendLine("Strikes:");
            foreach (var strike in Strikes)
            {
                output.AppendLine($"\t{strike}");
            }
            return output.ToString();
        }
    }
}
