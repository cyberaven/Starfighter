using UnityEngine;
using UnityEditor;
using Server.Core;
using Server.Interfaces;
using Server.Packages;
using System.Threading.Tasks;

public class ServerResponse
{
    public static async void SendDecline(IPackage pack)
    {
        var socket = new UdpSocket(pack.IpEndPoint);
        var result = await socket.SendPackageAsync(new DeclinePackage());
    }

    public static async void SendAccept(IPackage pack)
    {
        var socket = new UdpSocket(pack.IpEndPoint);
        var result = await socket.SendPackageAsync(new AcceptPackage());
    }
}