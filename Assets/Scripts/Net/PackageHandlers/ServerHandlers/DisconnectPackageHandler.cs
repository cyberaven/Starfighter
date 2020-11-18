using System.Linq;
using System.Threading.Tasks;
using Net.Core;
using Net.Interfaces;

namespace Net.PackageHandlers
{

    public class DisconnectPackageHandler : IPackageHandler
    {
        public async Task Handle(IPackage pack)
        {
            if(ServerManager.getInstance().ConnectedClients.Any(client => Equals(client.GetIpAddress(), pack.ipAddress)))
            {
                ServerManager.getInstance().ConnectedClients.RemoveAll(client => Equals(client.GetIpAddress(), pack.ipAddress));
                EventBus.getInstance().sendAccept.Invoke(pack);
                //TODO:delete all GO attached to this IPEndpoint
            }
            else
            {
                //There is no such client to Disconnect;
                EventBus.getInstance().sendDecline.Invoke(pack);
            }
        }
    }
}