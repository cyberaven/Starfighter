using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Net.Interfaces;
using UnityEngine;

/// <summary>
/// Recieve server port - 5000
/// Send server port - 5001
/// Recieve client port - 5001
/// Send client port - 5000
/// </summary>


namespace Net.Core
{
    public class UdpSocket
    {
        private readonly IPEndPoint _endPoint;
        private readonly int _receivingPort;

        public UdpSocket(IPEndPoint endpoint, int receivingPort)
        {
            _endPoint = endpoint;
            _receivingPort = receivingPort;
        }

        public UdpSocket()
        {
            _endPoint = null;
        }
        
        public async Task<bool> SendPackageAsync(IPackage pack)
        {
            try
            {
                using (var udpClient = new UdpClient(_endPoint))
                {
                    udpClient.Connect(_endPoint);
                    var serializer = new BinaryFormatter();
                    var stream = new MemoryStream();
                    serializer.Serialize(stream, pack);
                    var data = stream.GetBuffer();
                    stream.Close();
                    Debug.unityLogger.Log("Before sending package");
                    var sendedBytesCount = await udpClient.SendAsync(data, data.Length);
                    Debug.unityLogger.Log(
                        $"{_endPoint.Address.MapToIPv4()}:{_endPoint.Port} sent package. Sent {sendedBytesCount} of {data.Length}");
                    //maybe some check for all bytes sended
                    return true;
                }
            }
            catch (SocketException ex)
            {
                Debug.unityLogger.LogException(ex);
                return false;
            }
        }

        public async Task<IPackage> ReceivePackageAsync()
        {
            try
            {
                using (var udpClient = new UdpClient(_receivingPort))
                {
                    udpClient.Connect(IPAddress.Loopback, _receivingPort);
                    var serializer = new BinaryFormatter();
                    Debug.unityLogger.Log($" waiting package from {_receivingPort}");
                    var result = await udpClient.ReceiveAsync();
                    var stream = new MemoryStream(result.Buffer);

                    var pack = (IPackage) serializer.Deserialize(stream);
                    pack.ipAddress = result.RemoteEndPoint.Address;

                    stream.Close();

                    return pack;
                }
            }
            catch (SocketException ex)
            {
                Debug.unityLogger.LogException(ex);
                return null;
            }
        }

        public IPAddress GetAddress()
        {
            return _endPoint.Address;
        }

        public int GetListeningPort()
        {
            return _receivingPort;
        }
    }
}
