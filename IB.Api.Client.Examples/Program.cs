using IB.Api.Client.Implementation.Model;

namespace IB.Api.Client.Examples
{
    internal static class Program
    {
        private static void Main()
        {
            //double-check your connection details within your TWS/Gateway settings
            var connectionDetails = new ConnectionDetails
            {
                Host = "127.0.0.1",
                Port = 4001,
                ClientId = 0
            };
            Options.Run(connectionDetails);
            Common.KeepConsoleAlive();
        }
    }
}
