using System;
using System.Collections.Generic;
using IB.Api.Client.IBKR;
using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Implementation
{
    //Metadata
    public partial class IbClient
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
        public void ReqMarketRule(int ruleId)
        {
            ClientSocket.reqMarketRule(ruleId);
        }

        void EWrapper.contractDetails(int reqId, ContractDetails contractDetails)
        {
            _ = reqId;
            _contracts.Add(contractDetails);
        }
        void EWrapper.contractDetailsEnd(int reqId)
        {
            _ = reqId;
            ContractDetailsReceived?.Invoke(this, _contracts);
        }
        void EWrapper.marketRule(int marketRuleId, PriceIncrement[] priceIncrements)
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
