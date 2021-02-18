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
                var connectPack = (ConnectPackage)pack;
                
                if (ClientManager.instance.CheckAuthorization(connectPack))
                {
                    Debug.Log("Connection declined (this endpoint already connected) or there is no such account");
                    
                    ServerHelper.SendConnectionResponse(new DeclinePackage(new DeclineData()));
                    return;
                }
                
                Debug.Log($"Connection accepted: {pack.ipAddress.MapToIPv4()}");

                connectPack.data.multicastGroupIp = Constants.MulticastAddress;
                connectPack.data.portToSend = ClientManager.instance.GetNewPort();
                connectPack.data.portToReceive = ClientManager.instance.GetNewPort();
                
                ClientManager.instance.AddClient(connectPack);
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