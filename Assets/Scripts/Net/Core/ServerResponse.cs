using System;
using System.Net;
using Net.Interfaces;
using Net.PackageData;
using Net.Packages;
using UnityEngine;

namespace Net.Core
{
    public static class ServerResponse
    {
        //TODO: Fix SendDecline and SendAccept for new architecture
        public static async void SendDecline(AbstractPackage pack)
        {
            Debug.unityLogger.Log($"Gonna send decline to: {pack.ipAddress}");
            var socket = new StarfighterUdpClient(pack.ipAddress, Constants.ServerSendingPort, Constants.ServerReceivingPort);
            var result = await socket.SendPackageAsync(new DeclinePackage(new DeclineData(){eventId = pack.id}));
        }

        public static async void SendAccept(AbstractPackage pack)
        {
            Debug.unityLogger.Log($"Gonna send accept to: {pack.ipAddress}:{Constants.ServerSendingPort}");
            var socket = new StarfighterUdpClient(pack.ipAddress, Constants.ServerSendingPort, Constants.ServerReceivingPort);
            var result = await socket.SendPackageAsync(new AcceptPackage(new AcceptData(){eventId = pack.id}));
        }

        public static async void SendConnectionResponse(ConnectPackage pack)
        {
            Debug.unityLogger.Log($"Sending Connection Response to {pack.ipAddress}:{Constants.ServerSendingPort}");
            try
            {
                var socket = new StarfighterUdpClient(pack.ipAddress, Constants.ServerSendingPort, 0);
                var result = await socket.SendPackageAsync(pack);
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogError("SendConnectionResponse error:", ex.Message);
            }

            Debug.unityLogger.Log($"Connection response sent to {pack.ipAddress}:{Constants.ServerSendingPort}");
        }
    }
}