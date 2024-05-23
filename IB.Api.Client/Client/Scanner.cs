﻿using System;
using System.IO;
using IBApi;

namespace IB.Api.Client;

//Scanner
public partial class IBClient
{
    private string _xmlFilePath;

    public void RequestScannerParameters(string xmlFilePath)
    {
        _xmlFilePath = xmlFilePath;
        Notify("Scanner parameters requested");
        ClientSocket.ReqScannerParameters();
    }

    public void ScannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
    {
        throw new NotImplementedException();
    }
    public void ScannerDataEnd(int reqId)
    {
        throw new NotImplementedException();
    }
    public void ScannerParameters(string xml)
    {
        File.WriteAllText(_xmlFilePath, xml);
        Notify($"Scanner parameters saved to {_xmlFilePath}");
    }
}