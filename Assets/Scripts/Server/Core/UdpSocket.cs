using Server.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            var stream = new MemoryStream();
            serializer.Serialize(stream, pack);
            var data = stream.GetBuffer();
            stream.Close();

            var sendedBytesCount =  await _udpClient.SendAsync(data, data.Length, _endPoint);
            //maybe some check for all bytes sended
        }

        public async Task<IPackage> RecievePackageAsync()
        {
            var serializer = new BinaryFormatter();
            var result = await _udpClient.ReceiveAsync();
            var stream = new MemoryStream(result.Buffer);

            var pack = (IPackage)serializer.Deserialize(stream);
            pack.IpEndPoint = result.RemoteEndPoint;

            stream.Close();

            return pack;
        }

        public IPEndPoint GetEndPoint()
        {
            return _endPoint;
        }

    }
}
