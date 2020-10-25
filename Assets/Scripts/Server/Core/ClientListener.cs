using UnityEngine;
using System.Collections;
using System.Net;

namespace Server.Core {

    public class ClientListener : ScriptableObject
    {
        private UdpSocket _udpSocket;

        public ClientListener(IPEndPoint endpoint)
        {
            _udpSocket = new UdpSocket(endpoint);
        }

        void Update()
        {

        }
    }
}
