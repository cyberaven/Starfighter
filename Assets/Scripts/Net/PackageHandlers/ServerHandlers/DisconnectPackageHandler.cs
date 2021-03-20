using System.Linq;
using System.Threading.Tasks;
using Net.Core;
using Net.Interfaces;
using Net.Packages;
using Net.Utils;
using UnityEngine;

namespace Net.PackageHandlers.ServerHandlers
{

    public class DisconnectPackageHandler : IPackageHandler
    {
        public async Task Handle(AbstractPackage pack)
        {
            Debug.unityLogger.Log("Server : Disconnection!");
            NetEventStorage.GetInstance().disconnectClient.Invoke(pack as DisconnectPackage);
        }
    }
}