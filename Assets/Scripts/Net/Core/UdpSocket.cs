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
        private readonly IPAddress _sendingAddress;
        private readonly int _sendingPort;
        private readonly IPAddress _receivingAddress;
        private readonly int _receivingPort;
        private static UdpClient _receivingClient;

        public UdpSocket(IPAddress sendingAddress, int sendingPort, IPAddress receivingAddress, int receivingPort)
        {
            _sendingPort = sendingPort;
            _sendingAddress = sendingAddress;
            _receivingAddress = receivingAddress;
            _receivingPort = receivingPort;
            _receivingClient = new UdpClient(receivingPort);
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
                    var sendedBytesCount = udpClient.Send(data, data.Length, _sendingAddress.ToString(), _sendingPort);
                    Debug.unityLogger.Log(
                        $"{_sendingAddress.MapToIPv4()}:{_sendingPort} package sent. Sent {sendedBytesCount} of {data.Length}");
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

        public async Task<AbstractPackage> ReceiveOnePackage()
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
            Debug.unityLogger.Log($" waiting package from {_receivingAddress}:{_receivingPort}");
            var remoteEndPoint = new IPEndPoint(_receivingAddress, _receivingPort);
            var result = _receivingClient.Receive(ref remoteEndPoint);
            var stream = new MemoryStream(result);

            var pack = (AbstractPackage) serializer.Deserialize(stream);
            pack.ipAddress = remoteEndPoint.Address;

            stream.Close();
            return pack;
        }
        
        public void BeginReceivingPackagesAsync()
        {
            try
            {
                var asyncResult = _receivingClient.BeginReceive(EndReceivePack, new object());
            }
            catch (SocketException ex)
            {
                Debug.unityLogger.LogWarning("Receiving", ex);
                // Debug.unityLogger.LogException(ex);
                return;
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
                return;
            }
        }

        private void EndReceivePack(IAsyncResult asyncResult)
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
            Debug.unityLogger.Log($" waiting package from {_receivingAddress}:{_receivingPort}");
            var remoteEndPoint = new IPEndPoint(_receivingAddress, _receivingPort);
            var result = _receivingClient.EndReceive(asyncResult, ref remoteEndPoint);
            var stream = new MemoryStream(result);

            var pack = (AbstractPackage) serializer.Deserialize(stream);
            pack.ipAddress = remoteEndPoint.Address;

            stream.Close();
            
            //New pack received event invoke
            EventBus.GetInstance().newPackageRecieved.Invoke(pack);

            BeginReceivingPackagesAsync();
        }
        
        public IPAddress GetSendingAddress()
        {
            return _sendingAddress;
        }

        public IPAddress GetReceivingAddress()
        {
            return _receivingAddress;
        }
        
        public int GetListeningPort()
        {
            return _receivingPort;
        }
    }
}
