using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Net.Core;
using Net.Interfaces;
using UnityEngine;

namespace Net.PackageHandlers.ServerHandlers
{

    public class ConnectPackageHandler : IPackageHandler
    {
        public async Task Handle(IPackage pack)
        {
            try
            {
                Debug.unityLogger.Log("Connection handle start");
                //TODO:check for LoginPassword (decline if incorrect)
                //check for already connection (decline if yes)
                if (ServerManager.getInstance().ConnectedClients
                    .Any(client => Equals(client.GetIpAddress(), pack.ipAddress)))
                {
                    Debug.Log("Connection declined");
                    EventBus.getInstance().sendDecline.Invoke(pack);
                    return;
                }

                //create new async ClientListener for it
                Debug.Log("Connection accepted");
                ServerManager.getInstance().ConnectedClients.Add(new ClientListener(
                    new IPEndPoint(pack.ipAddress, Constants.ServerSendingPort), Constants.ServerReceivingPort, pack));
                //TODO:init new user
                EventBus.getInstance().sendAccept.Invoke(pack);
            }
            catch (Exception ex)
            {
                Debug.unityLogger.Log(ex);
                return;
            }
        }
    }
}