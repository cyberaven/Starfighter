using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Net.Core;
using Net.Interfaces;
using Net.PackageData;
using Net.Packages;
using Net.Utils;
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
                var loginData = ((ConnectPackage) pack).data;
                

                if (ClientManager.GetInstance().ConnectedClients
                    .Any(client => Equals(client.GetIpAddress(), pack.ipAddress)))
                {
                    Debug.Log("Connection declined (this endpoint already connected)");
                    
                    ServerHelper.SendConnectionResponse(new DeclinePackage(new DeclineData()));
                    return;
                }
                
                Debug.Log($"Connection accepted: {pack.ipAddress.MapToIPv4()}");

                var connectPack = (ConnectPackage)pack;
                
                connectPack.data.multicastGroupIp = Constants.MulticastAddress;
                connectPack.data.portToSend = ClientManager.GetInstance().GetNewPort();
                connectPack.data.portToReceive = ClientManager.GetInstance().GetNewPort();
                
                EventBus.GetInstance().addClient.Invoke(connectPack);
                ServerHelper.SendConnectionResponse(connectPack);
                //TODO:init new user
                //TODO: instantiate user's Go, if necessary
                //TODO: bind it with client's object
            }
            catch (Exception ex)
            {
                Debug.unityLogger.Log(ex);
                return;
            }
        }
    }
}