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
            if(ServerManager.Instance.connectedClients.First(client => Equals(client.GetEndPoint(), pack.IpEndPoint)))
            {
                ServerManager.Instance.connectedClients.RemoveAll(client => Equals(client.GetEndPoint(), pack.IpEndPoint));
                EventBus.Instance.sendAccept.Invoke(pack);
                //TODO:delete all GO attached to this IPEndpoint
            }
            else
            {
                //There is no such client to Disconnect;
                EventBus.Instance.sendDecline.Invoke(pack);
            }
        }
    }
}