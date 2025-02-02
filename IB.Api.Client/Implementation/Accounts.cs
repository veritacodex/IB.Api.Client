using System;
using System.Collections.Generic;
using IB.Api.Client.IBKR;
using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Implementation
{
    //Acounts
    public partial class IbClient
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
            Notify($"Default account updates requested");
            ClientSocket.reqAccountUpdates(true, null);
        }

        void IEWrapper.accountDownloadEnd(string account)
        {
            DiscardImplementation(account);
        }
        void IEWrapper.managedAccounts(string accountsList)
        {
            AccountIds = [.. accountsList.Split(',')];
            Notify($"Managed accounts ({accountsList})");
        }
        void IEWrapper.updateAccountTime(string timestamp)
        {
            _ = timestamp;
            _accountUpdate.UpdatedOn = DateTime.Now;
            AccountUpdateReceived?.Invoke(this, _accountUpdate);
        }
        void IEWrapper.updateAccountValue(string key, string value, string currency, string accountName)
        {
            _ = accountName;
            _accountUpdate.SetValue(key, value, currency);
        }
    }
}
