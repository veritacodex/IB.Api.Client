namespace IB.Api.Client.Model
{
    public class OrderBookLine
    {
        public int Position { get; set; }
        /// <summary>
        /// 0 - Insert
        /// 1 - Update
        /// 2 - Remove
        /// </summary>
        public int Operation { get; set; }
        public int Side { get; set; }
        public double Price { get; set; }
        public decimal Size { get; set; }
        public decimal PercentageOfBook { get; set; }
    }
}
