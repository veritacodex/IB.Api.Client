using System.IO;
using IB.Api.Client.IBKR;

namespace IB.Api.Client.Implementation;

//Scanner
public partial class IbClient
{
    private string _xmlFilePath;

    public void ReqScannerParameters(string xmlFilePath)
    {
        _xmlFilePath = xmlFilePath;
        Notify("Scanner parameters requested");
        ClientSocket.reqScannerParameters();
    }    
    void IEWrapper.scannerParameters(string xml)
    {
        File.WriteAllText(_xmlFilePath, xml);
        Notify($"Scanner parameters saved to {_xmlFilePath}");
    }
}