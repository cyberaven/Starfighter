using Server.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Recieve server port - 5000
/// Send server port - 5001
/// Recieve client port - 5001
/// Send client port - 5000
/// </summary>


namespace Server.Core
{

    public class UdpSocket
    {
        private UdpClient _udpClient;
        private IPEndPoint _endPoint;

        public UdpSocket(IPEndPoint endpoint)
        {
            _udpClient = new UdpClient();
            _endPoint = endpoint;
        }

        public UdpSocket()
        {
            _udpClient = new UdpClient();
            _endPoint = null;
        }

        public async Task SendPackageAsync(IPackage pack)
        {
            var serializer = new BinaryFormatter();
            _udpClient.Client.Bind(_endPoint);
            var stream = new NetworkStream(_udpClient.Client);
            serializer.Serialize(stream, pack);
            stream.Close();
        }

        public async Task<IPackage> RecievePackageAsync()
        {
            var serializer = new BinaryFormatter();
            var stream = new NetworkStream(_udpClient.Client);
            //Any async inside of deser? Does deser include awaiting?
            // возможно кстати нет, потому что может оно просто берет уже имеющийся буфер, который пуст до момента получения данных
            // тогда надо отдельно ждать и получать. Тогда Будет возможность получить и записать IPEndpoint
            var pack = (IPackage)serializer.Deserialize(stream); //отсюда надо вытащить IPEndPoint
            stream.Close();
            return pack;
        }

    }
}
