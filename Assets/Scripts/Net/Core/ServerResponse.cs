using System.Net;
using Net.Interfaces;
using Net.PackageData;
using Net.Packages;
using UnityEngine;

namespace Net.Core
{
    public class ServerResponse
    {
        public static async void SendDecline(AbstractPackage pack)
        {
            Debug.unityLogger.Log($"Gonna send decline to: {pack.ipAddress}");
            var socket = new UdpSocket(pack.ipAddress, Constants.ServerSendingPort,
                pack.ipAddress, Constants.ServerReceivingPort);
            var result = await socket.SendPackageAsync(new DeclinePackage(new DeclineData(){eventId = pack.id}));
        }

        public static async void SendAccept(AbstractPackage pack)
        {
            Debug.unityLogger.Log($"Gonna send accept to: {pack.ipAddress}:{Constants.ServerSendingPort}");
            var socket = new UdpSocket(pack.ipAddress, Constants.ServerSendingPort,
                pack.ipAddress,Constants.ServerReceivingPort);
            var result = await socket.SendPackageAsync(new AcceptPackage(new AcceptData(){eventId = pack.id}));
        }
    }
}