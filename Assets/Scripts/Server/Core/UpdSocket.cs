using Server.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;


namespace Server.Core
{

    public class UpdSocket
    {
        private UdpClient _udpClient;

        public UpdSocket(string address, int port)
        {
            _udpClient = new UdpClient(address, port);
        }

        public async Task SendPackage(IPackage pack)
        {
            var serializer = new BinaryFormatter();
            var stream = new NetworkStream(_udpClient.Client);
            serializer.Serialize(stream, pack);
        }

        public async Task<IPackage> RecievePackage()
        {
            var serializer = new BinaryFormatter();
            var stream = new NetworkStream(_udpClient.Client);
            var pack = (IPackage)serializer.Deserialize(stream);

            return pack;
        }

    }
}
