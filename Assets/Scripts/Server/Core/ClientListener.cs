using UnityEngine;
using System.Net;
using Server.Interfaces;

namespace Server.Core {

    public class ClientListener : ScriptableObject
    {
        private UdpSocket _udpSocket;

        public ClientListener(IPEndPoint endpoint, IPackage pack)
        {
            _udpSocket = new UdpSocket(endpoint);
        }

        void Update()
        {

        }
    }
}
