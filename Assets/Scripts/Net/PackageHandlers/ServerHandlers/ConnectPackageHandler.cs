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
                
                Debug.unityLogger.Log($"Acc {connectPack.data.accountType}:  {connectPack.data.login}:{connectPack.data.password}");
                
                if (ClientManager.instance.CheckAuthorization(connectPack))
                {  
                    Debug.Log($"Connection accepted: {pack.ipAddress.MapToIPv4()}");

                    connectPack.data.multicastGroupIp = Constants.MulticastAddress;
                    connectPack.data.portToSend = ClientManager.instance.GetNewPort();
                    connectPack.data.portToReceive = ClientManager.instance.GetNewPort();

                    ClientManager.instance.AddClient(connectPack);
                    ServerHelper.SendConnectionResponse(connectPack);
                    return;
                }
                
                Debug.Log("Connection declined (this endpoint already connected) or there is no such account");
                
                ServerHelper.SendConnectionResponse(new DeclinePackage(new DeclineData())
                {
                    ipAddress = connectPack.ipAddress,
                });
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogError("Server connect pack handler",ex);
            }
        }
    }
}