using IBApi;

namespace IB.Api.Client.Implementation.Model
{
    public class BarUpdate
    {
        public int RequestId { get; set; }
        public Bar Bar { get; set; }
    }
}