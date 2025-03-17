using System;
using System.Collections.Generic;
using System.Linq;
using IB.Api.Client.Helper;
using IB.Api.Client.IBKR;
using IB.Api.Client.Implementation;
using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Examples;

public static class Options
{
    private static double _optionChainMidPoint;
    private static string _expiration;
    private static OptionParameterDefinition _parameterDefinition;
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
        _parameterDefinition = parametersList.First(x => x.Exchange == ContractExchange && x.TradingClass == ContractSymbol);
        _expiration = _parameterDefinition.Expirations.ToList()[1]; //Choosing the nearest available expiration for the example
        _optionChainMidPoint = _parameterDefinition.Strikes.ToList()[_parameterDefinition.Strikes.Count / 2];
        
        Console.WriteLine($"Mid Point: {_optionChainMidPoint}");
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