using System.Net;
using Net.Interfaces;
using Net.Packages;

namespace Net.Core
{
    public class ServerResponse
    {
        public static async void SendDecline(AbstractPackage pack)
        {
            var socket = new UdpSocket(new IPEndPoint(pack.ipAddress, Constants.ServerSendingPort), Constants.ServerReceivingPort);
            var result = await socket.SendPackageAsync(new DeclinePackage());
        }

        public static async void SendAccept(AbstractPackage pack)
        {
            var socket = new UdpSocket(new IPEndPoint(pack.ipAddress, Constants.ServerSendingPort), Constants.ServerReceivingPort);
            var result = await socket.SendPackageAsync(new AcceptPackage());
        }
    }
}