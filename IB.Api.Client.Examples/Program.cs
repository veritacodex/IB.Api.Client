using IB.Api.Client.Model;

namespace IB.Api.Client.Examples
{
    static class Program
    {
        static void Main()
        {
            //double check your connection details within your TWS/Gateway settings
            var connectionDetails = new ConnectionDetails
            {
                Host = "127.0.0.1",
                Port = 4002,
                ClientId = 0
            };
            RequestNews.RunGetNewsProviders(connectionDetails);
            Common.KeepConsoleAlive();
        }
    }
}
