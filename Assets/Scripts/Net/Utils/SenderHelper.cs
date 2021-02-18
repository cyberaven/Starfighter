using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Net.Core;
using Net.PackageData;
using Net.Packages;

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
    }
}