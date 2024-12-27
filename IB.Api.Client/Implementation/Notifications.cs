using System;
using IB.Api.Client.Implementation.Model;
using IBApi;

namespace IB.Api.Client
{
    public partial class IBClient
    {
        public event EventHandler<Notification> NotificationReceived;
        public event EventHandler<UiNotification> UiNotificationReceived;        
        
        public void Notify(string message)
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
        public void NotifyUI(string message)
        {
            var uiNotification = new UiNotification
            {
                At = DateTime.Now,
                Message = message
            };
            UiNotificationReceived?.Invoke(this, uiNotification);
        }        

        public static NotificationType GetNotificationType(string message)
        {
            if (message.Contains("data farm connection is OK"))
                return NotificationType.OK;

            if (message.Contains("data farm connection is inactive"))
                return NotificationType.Error;

            return NotificationType.Information;
        }

        void EWrapper.error(Exception e)
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
        void EWrapper.error(string str)
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
        void EWrapper.error(int id, int errorCode, string errorMsg, string advancedOrderRejectJson)
        {
            var notification = new Notification
            {
                At = DateTime.Now,
                Id = 0,
                Code = -1,
                Message = errorMsg,
                NotificationType = GetNotificationType(errorMsg),
                AdvancedOrderRejectJson = advancedOrderRejectJson
            };
            NotificationReceived?.Invoke(this, notification);
        }
    }
}
