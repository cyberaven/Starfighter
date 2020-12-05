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
            if(ServerManager.GetInstance().ConnectedClients.Any(client => Equals(client.GetIpAddress(), pack.ipAddress)))
            {
                ServerManager.GetInstance().ConnectedClients.RemoveAll(client => Equals(client.GetIpAddress(), pack.ipAddress));
                EventBus.GetInstance().sendAccept.Invoke(pack);
            }
            else
            {
                //There is no such client to Disconnect;
                EventBus.GetInstance().sendDecline.Invoke(pack);
            }
        }
    }
}