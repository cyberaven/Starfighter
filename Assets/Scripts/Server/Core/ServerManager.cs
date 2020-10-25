using UnityEngine;
using System.Collections.Generic;
using Server.Interfaces;
using System.Net;
using Server.Utils.Enums;
using System.Threading.Tasks;

namespace Server.Core
{
    public class ServerManager
    {
        public static List<UdpSocket> sockets;

        private static ServerManager _instance;

        private ServerManager()
        {
            sockets = new List<UdpSocket>();
        }

        public static ServerManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ServerManager();
            }
            return _instance;
        }

        public async Task WaitForConnectionAsync(UdpSocket waiter)
        {
            var res =  await waiter.RecievePackageAsync();
            switch (res.PackageType)
            {
                case PackageType.ConnectPackage:
                    //init new user
                    //check for LoginPassword (decline if incorrect)
                    //check for already connection (decline if yes)
                    //create new async ClientListener for it
                    break;
                case PackageType.DisconnectPackage:
                case PackageType.AcceptPackage:
                case PackageType.DeclinePackage:
                case PackageType.EventPackage:
                case PackageType.StatePackage:
                    // отдать куда-то на обработку?
                    break;
            }
        }

        private UdpSocket CreateNewListener(IPEndPoint endpoint)
        {
            var socket = new UdpSocket(endpoint);
            sockets.Add(socket);
            return socket;
        }

        public void BroadcastSending(IPackage pack)
        {

        }
    }
}
