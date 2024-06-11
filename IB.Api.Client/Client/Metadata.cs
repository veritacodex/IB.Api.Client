using System;
using System.Collections.Generic;
using IB.Api.Client.Model;
using IBApi;

namespace IB.Api.Client
{
    //Metadata
    public partial class IBClient
    {
        private List<ContractDetails> _contracts;
        public event EventHandler<List<ContractDetails>> ContractDetailsReceived;
        public event EventHandler<MarketRule> MarketRuleReceived;
        public void GetContractDetails(string symbol, SecurityType securityType)
        {
            _contracts = [];
            ClientSocket.reqContractDetails(1020, new Contract
            {
                Symbol = symbol,
                SecType = securityType.ToString()
            });
        }
        public void GetContractDetails(Contract contract)
        {
            _contracts = [];
            ClientSocket.reqContractDetails(1021, contract);
        }
        public void contractDetails(int reqId, ContractDetails contractDetails)
        {
            _contracts.Add(contractDetails);
        }
        public void contractDetailsEnd(int reqId)
        {
            ContractDetailsReceived?.Invoke(this, _contracts);
        }
        public void ReqMarketRule(int ruleId)
        {
            ClientSocket.reqMarketRule(ruleId);
        }

        public void marketRule(int marketRuleId, PriceIncrement[] priceIncrements)
        {
            var marketRule = new MarketRule
            {
                MarketRuleId = marketRuleId,
                PriceIncrements = priceIncrements
            };
            MarketRuleReceived?.Invoke(this, marketRule);
        }
    }
}
