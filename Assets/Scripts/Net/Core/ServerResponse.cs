using System.Net;
using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;
using Net.Interfaces;
using Net.Packages;

namespace Net.Core
{
    public class ServerResponse
    {
        public static async void SendDecline(IPackage pack)
        {
            var socket = new UdpSocket(new IPEndPoint(pack.ipAddress, Constants.ServerSendingPort), Constants.ServerReceivingPort);
            var result = await socket.SendPackageAsync(new DeclinePackage());
        }

        public static async void SendAccept(IPackage pack)
        {
            var socket = new UdpSocket(new IPEndPoint(pack.ipAddress, Constants.ServerSendingPort), Constants.ServerReceivingPort);
            var result = await socket.SendPackageAsync(new AcceptPackage());
        }
    }
}