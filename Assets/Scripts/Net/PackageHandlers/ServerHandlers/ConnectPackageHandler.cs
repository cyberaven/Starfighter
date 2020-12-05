using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Net.Core;
using Net.Interfaces;
using Net.Packages;
using UnityEngine;

namespace Net.PackageHandlers.ServerHandlers
{

    public class ConnectPackageHandler : IPackageHandler
    {
        public async Task Handle(AbstractPackage pack)
        {
            try
            {
                Debug.unityLogger.Log("Connection handle start");
                //TODO:check for LoginPassword (decline if incorrect)
                //TODO:check for already connection (decline if yes)
                if (ServerManager.GetInstance().ConnectedClients
                    .Any(client => Equals(client.GetIpAddress(), pack.ipAddress)))
                {
                    Debug.Log("Connection declined");
                    EventBus.GetInstance().sendDecline.Invoke(pack);
                    return;
                }
                
                Debug.Log("Connection accepted");
                ServerManager.GetInstance().ConnectedClients.Add(new ClientListener(
                    new IPEndPoint(pack.ipAddress, Constants.ServerSendingPort), Constants.ServerReceivingPort));
                //TODO:init new user
                EventBus.GetInstance().sendAccept.Invoke(pack);
            }
            catch (Exception ex)
            {
                Debug.unityLogger.Log(ex);
                return;
            }
        }
    }
}