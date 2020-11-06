using System.Linq;
using System.Threading.Tasks;
using Net.Core;
using Net.Interfaces;

namespace Net.PackageHandlers
{

    public class ConnectPackageHandler : IPackageHandler
    {
        public async Task Handle(IPackage pack)
        {
            //TODO:check for LoginPassword (decline if incorrect)
            //check for already connection (decline if yes)
            if(ServerManager.Instance.connectedClients.First(client => Equals(client.GetEndPoint(), pack.IpEndPoint)))
            {
                EventBus.Instance.sendDecline.Invoke(pack);
            }
            //create new async ClientListener for it
            ServerManager.Instance.connectedClients.Add(new ClientListener(pack.IpEndPoint, pack));
            //TODO:init new user
            //TODO:send World State to him. Should I?
        }
    }
}