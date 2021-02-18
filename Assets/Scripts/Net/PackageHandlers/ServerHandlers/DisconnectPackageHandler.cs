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
            if(ClientManager.instance.ConnectedClients.Any(client => Equals(client.GetIpAddress(), pack.ipAddress)))
            {
                var clientToSend = ClientManager.instance.ConnectedClients
                    .FirstOrDefault(client => Equals(client.GetIpAddress(), pack.ipAddress));
                clientToSend?.SendAccept((pack as EventPackage).id);
                ClientManager.instance.ConnectedClients.Remove(clientToSend);
                //TODO:delete all GO associated with this IPEndpoint
            }
            else
            {
                //There is no such client to Disconnect;
            }
        }
    }
}