using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Net.Interfaces;
using Net.Packages;
using Net.Utils;
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
        
        public async Task<bool> SendPackageAsync(AbstractPackage pack)
        {
            try
            {
                using (var udpClient = new UdpClient())
                {
                    var selector = new SurrogateSelector();
                    selector.AddSurrogate(
                        typeof(Vector3),
                        new StreamingContext(StreamingContextStates.All), 
                        new Vector3SerializationSurrogate());
                    selector.AddSurrogate(
                        typeof(Quaternion),
                        new StreamingContext(StreamingContextStates.All), 
                        new QuaternionSerializationSurrogate());
                    var serializer = new BinaryFormatter {SurrogateSelector = selector};
                    var stream = new MemoryStream();
                    serializer.Serialize(stream, pack);
                    var data = stream.GetBuffer();
                    stream.Close();
                    Debug.unityLogger.Log("Before sending package");
                    var sendedBytesCount = await udpClient.SendAsync(data, data.Length, _endPoint.Address.ToString(), _endPoint.Port);
                    Debug.unityLogger.Log(
                        $"{_endPoint.Address.MapToIPv4()}:{_endPoint.Port} package sent. Sent {sendedBytesCount} of {data.Length}");
                    //maybe some check for all bytes sended
                    return true;
                }
            }
            catch (SocketException ex)
            {
                Debug.unityLogger.LogWarning("Sending", ex);
                // Debug.unityLogger.LogException(ex);
                return false;
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
                return false;
            }
        }

        public async Task<AbstractPackage> ReceivePackageAsync()
        {
            try
            {
                using (var udpClient = new UdpClient(_receivingPort))
                {
                    var selector = new SurrogateSelector();
                    selector.AddSurrogate(
                        typeof(Vector3),
                        new StreamingContext(StreamingContextStates.All), 
                        new Vector3SerializationSurrogate());
                    selector.AddSurrogate(
                        typeof(Quaternion),
                        new StreamingContext(StreamingContextStates.All), 
                        new QuaternionSerializationSurrogate());
                    var serializer = new BinaryFormatter {SurrogateSelector = selector};
                    Debug.unityLogger.Log($" waiting package from {_receivingPort}");
                    var result = await udpClient.ReceiveAsync();
                    var stream = new MemoryStream(result.Buffer);

                    var pack = (AbstractPackage) serializer.Deserialize(stream);
                    pack.ipAddress = result.RemoteEndPoint.Address;

                    stream.Close();

                    return pack;
                }
            }
            catch (SocketException ex)
            {
                Debug.unityLogger.LogWarning("Receiving", ex);
                // Debug.unityLogger.LogException(ex);
                return null;
            }
            catch (Exception ex)
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
