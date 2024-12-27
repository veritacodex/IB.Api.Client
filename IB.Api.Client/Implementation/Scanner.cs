using System;
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
        ClientSocket.reqScannerParameters();
    }    
    void EWrapper.scannerParameters(string xml)
    {
        File.WriteAllText(_xmlFilePath, xml);
        Notify($"Scanner parameters saved to {_xmlFilePath}");
    }
}