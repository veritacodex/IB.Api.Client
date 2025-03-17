using System.Collections.Generic;
using System.Text;

namespace IB.Api.Client.Implementation.Model
{
    public class OptionParameterDefinition
    {
        public string Exchange { get; internal init; }
        public int UnderlyingConId { get; internal init; }
        public string TradingClass { get; internal init; }
        public string Multiplier { get; internal init; }
        public HashSet<string> Expirations { get; internal init; }
        public HashSet<double> Strikes { get; internal init; }
        public int ReqId { get; internal set; }

        public override string ToString()
        {
            var output = new StringBuilder();
            output.AppendLine($"Exchange:{Exchange} UnderlyingConId:{UnderlyingConId} TradingClass:{TradingClass} Multiplier:{Multiplier}");
            output.AppendLine("Expirations:");
            output.AppendLine($"\t{string.Join('|', Expirations)}");
            output.AppendLine("Strikes:");
            output.AppendLine($"\t{string.Join('|', Strikes)}");
            return output.ToString();
        }
    }
}
