using System.Linq;
using System.Threading.Tasks;
using Net.Core;
using Net.Interfaces;
using Net.Packages;

namespace Net.PackageHandlers.ServerHandlers
{

    public class DisconnectPackageHandler : IPackageHandler
    {
        public async Task Handle(AbstractPackage pack)
        {
            if(ClientManager.GetInstance().ConnectedClients.Any(client => Equals(client.GetIpAddress(), pack.ipAddress)))
            {
                var clientToSend = ClientManager.GetInstance().ConnectedClients
                    .FirstOrDefault(client => Equals(client.GetIpAddress(), pack.ipAddress));
                clientToSend?.SendAccept((pack as EventPackage).id);
                ClientManager.GetInstance().ConnectedClients.Remove(clientToSend);
                //TODO:delete all GO attached to this IPEndpoint
            }
            else
            {
                //There is no such client to Disconnect;
            }
        }
    }
}