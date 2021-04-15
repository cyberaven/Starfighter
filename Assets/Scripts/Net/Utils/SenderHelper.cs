using System;
using System.Threading.Tasks;
using Core;
using Net.Core;
using Net.PackageData;
using Net.Packages;
using UnityEngine;

namespace Net.Utils
{
    public static class SenderHelper
    {
        public static async Task<bool> SendEventPackage(this StarfighterUdpClient client, object data, EventType type)
        {
            var eventData = new EventData()
            {
                data = data,
                eventType = type,
                eventId = Guid.NewGuid(),
                timeStamp = DateTime.Now
            };
            var pack = new EventPackage(eventData);
            return await client.SendPackageAsync(pack);
        }
        
        public static async void SendConnectionResponse(AbstractPackage pack)
        {
            try
            {
                var socket = new StarfighterUdpClient(pack.ipAddress, Constants.ServerSendingPort, 0);
                var result = await socket.SendPackageAsync(pack);
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
            }

            Debug.unityLogger.Log($"Connection response sent to {pack.ipAddress}:{Constants.ServerSendingPort}");
        }
    }
}