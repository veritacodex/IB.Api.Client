using System;
using IB.Api.Client.Client.Model;
using IB.Api.Client.Model;
using IBApi;

namespace IB.Api.Client
{
    /// <summary>
    /// IB API sends notifications through error methods, not all are errors, some of them are just user messages for information
    /// </summary>
    public partial class IBClient
    {
        public event EventHandler<Notification> NotificationReceived;
        public event EventHandler<UiNotification> UiNotificationReceived;
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
                Id = 0,
                Code = -1,
                Message = errorMsg,
                NotificationType = GetNotificationType(errorMsg),
                AdvancedOrderRejectJson = advancedOrderRejectJson
            };
            NotificationReceived?.Invoke(this, notification);
        }
        
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
        void IEWrapper.connectAck()
        {
            Notify("Connection Acknowledged");
        }
        void IEWrapper.connectionClosed()
        {
            Notify("Connection Closed");
        }

        public static NotificationType GetNotificationType(string message)
        {
            if (message.Contains("data farm connection is OK"))
                return NotificationType.OK;

            if (message.Contains("data farm connection is inactive"))
                return NotificationType.Error;

            return NotificationType.Information;
        }
    }
}
