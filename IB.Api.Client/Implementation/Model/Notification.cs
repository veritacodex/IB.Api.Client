using System;

namespace IB.Api.Client.Implementation.Model
{
    public class Notification
    {
        public DateTime At { get; set; }
        public int Id { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public string AdvancedOrderRejectJson { get; internal set; }
    }
}