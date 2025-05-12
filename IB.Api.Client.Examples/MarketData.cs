using System;
using System.Collections.Generic;
using System.Linq;
using IB.Api.Client.IBKR;
using IB.Api.Client.Implementation;
using IB.Api.Client.Implementation.Helper;
using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Examples;

public static class MarketData
{
    private static IbClient _ibClient;

    public static void Run(ConnectionDetails connectionDetails)
    {
        _ibClient = new IbClient();
        _ibClient.NotificationReceived += ConnectionHelper.NotificationReceived;
        _ibClient.ContractDetailsReceived += ContractDetailsReceived;
        _ibClient.PriceUpdateReceived += PriceUpdate; 
            
        ConnectionHelper.StartIbClient(_ibClient, connectionDetails);

        if (_ibClient.IsConnected())
        {
            _ibClient.ReqContractDetails("SPY", SecurityType.STK);
        }            
        Common.KeepConsoleAlive();
    }

    private static void ContractDetailsReceived(object sender, List<ContractDetails> contractDetailsList)
    {
        var contractDetails = contractDetailsList.FirstOrDefault(x => x.Contract.Exchange == "SMART");
        if (contractDetails != null) _ibClient.ReqMktData(1010, contractDetails.Contract, "");
    }

    private static void PriceUpdate(object sender, PriceUpdate priceUpdate)
    {
        Console.WriteLine(priceUpdate.ToRefString());
    }
}