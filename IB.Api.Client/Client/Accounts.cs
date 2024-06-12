using System;
using System.Collections.Generic;
using IB.Api.Client.Model;

namespace IB.Api.Client
{
    //Acounts
    public partial class IBClient
    {
        public List<string> AccountIds { get; set; }
        private AccountUpdate _accountUpdate;
        public event EventHandler<AccountUpdate> AccountUpdateReceived;

        public void SubscribeToAccountUpdates(string accountId)
        {
            _accountUpdate = new AccountUpdate();
            Notify($"Account:{accountId} updates requested");
            ClientSocket.reqAccountUpdates(true, accountId);
        }
        public void SubscribeToDefaultAccountUpdates()
        {
            _accountUpdate = new AccountUpdate();
            ClientSocket.reqAccountUpdates(true, null);
        }

        public void accountDownloadEnd(string account)
        {
            _ = string.Empty;
        }
        public void managedAccounts(string accountsList)
        {
            AccountIds = [.. accountsList.Split(',')];
            Notify($"Managed accounts ({accountsList})");
        }
        public void updateAccountTime(string timestamp)
        {
            _accountUpdate.UpdatedOn = DateTime.Now;
            AccountUpdateReceived?.Invoke(this, _accountUpdate);
        }
        public void updateAccountValue(string key, string value, string currency, string accountName)
        {
            _accountUpdate.SetValue(key, value, currency);
        }
    }
}
