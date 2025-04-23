using System;
using System.Collections.Generic;
using System.Linq;
using IB.Api.Client.IBKR;
using IB.Api.Client.Implementation;
using IB.Api.Client.Implementation.Helper;
using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Examples;

public static class Options
{
    private const string ContractSymbol = "SPX";
    private const string ContractExchange = "CBOE";
    
    public static void Run(ConnectionDetails connectionDetails)
    {
        var ibClient = new IbClient();
        ibClient.NotificationReceived += ConnectionHelper.NotificationReceived;
        ibClient.OptionParametersReceived += OptionParametersReceived;
        
        ConnectionHelper.StartIbClient(ibClient, connectionDetails);

        var contractDetails = new ContractDetails
        {
            Contract = new Contract
            {
                ConId = 416904, // Contract ids can be found with the GetContractDetails method
                Symbol = ContractSymbol,
                Exchange = ContractExchange,
                SecType = "IND",
                Currency = "USD",
                LocalSymbol = ContractSymbol
            }
        };
        
        ibClient.ReqOptionParameters(1010, contractDetails);
        Common.KeepConsoleAlive();
    }

    private static void OptionParametersReceived(object sender, List<OptionParameterDefinition> parametersList)
    {
        // PrintDefinitions(parametersList);
        var parameterDefinition = parametersList.First(x => x.Exchange == ContractExchange && x.TradingClass == ContractSymbol);
        var expiration = parameterDefinition.Expirations.ToList()[1]; //Choosing the nearest available expiration for the example
        var optionChainMidPoint = parameterDefinition.Strikes.ToList()[parameterDefinition.Strikes.Count / 2];
        
        Console.WriteLine($"Mid Point: {optionChainMidPoint}");
    }

    private static void PrintDefinitions(List<OptionParameterDefinition> parametersList)
    {
        //the collection comes with several trading classes and exchanges, remove the filter to display all
        foreach (var parameter in parametersList.Where(x=> x.Exchange == ContractExchange && x.TradingClass == ContractSymbol))
        {
            Console.WriteLine(parameter);
        }
    }
}