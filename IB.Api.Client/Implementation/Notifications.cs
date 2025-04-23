using System;
using IB.Api.Client.IBKR;
using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Implementation
{
    public partial class IbClient
    {
        public event EventHandler<Notification> NotificationReceived;

        private void Notify(string message)
        {
            var notification = new Notification
            {
                At = DateTime.Now,
                Id = 0,
                Code = 0,
                Message = message,
                NotificationType = GetNotificationType(message)
            };
            NotificationReceived?.Invoke(this, notification);
        }

        private static NotificationType GetNotificationType(string message)
        {
            if (message.Contains("data farm connection is OK"))
                return NotificationType.OK;

            return message.Contains("data farm connection is inactive") ? NotificationType.Error : NotificationType.Information;
        }
        void IEWrapper.error(Exception e)
        {
            var notification = new Notification
            {
                At = DateTime.Now,
                Id = 0,
                Code = 0,
                Message = e.Message,
                NotificationType = NotificationType.Error
            };
            NotificationReceived?.Invoke(this, notification);
        }
        void IEWrapper.error(string str)
        {
            var notification = new Notification
            {
                At = DateTime.Now,
                Id = 0,
                Code = -1,
                Message = str,
                NotificationType = GetNotificationType(str)
            };
            NotificationReceived?.Invoke(this, notification);
        }
        void IEWrapper.error(int id, int errorCode, string errorMsg, string advancedOrderRejectJson)
        {
            var notification = new Notification
            {
                At = DateTime.Now,
                Id = id,
                Code = errorCode,
                Message = errorMsg,
                NotificationType = GetNotificationType(errorMsg),
                AdvancedOrderRejectJson = advancedOrderRejectJson
            };
            NotificationReceived?.Invoke(this, notification);
        }
    }
}
