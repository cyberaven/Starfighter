﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Net.Packages;
using Net.Utils;
using UnityEngine;

namespace Net.Core
{
    public sealed class StarfighterUdpClient: IDisposable
    {
        private readonly IPAddress _sendingAddress;
        private readonly int _sendingPort;
        private readonly UdpClient _receivingClient;
        private bool isContinueReceiving = true;

        
        public StarfighterUdpClient(IPAddress sendingAddress, int sendingPort, int receivingPort)
        {
            _sendingPort = sendingPort;
            _sendingAddress = sendingAddress;
            _receivingClient = new UdpClient(receivingPort);
        }

        public void JoinMulticastGroup(IPAddress address)
        {
            _receivingClient.JoinMulticastGroup(address);
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
                    var sentBytesCount = await udpClient.SendAsync(data, data.Length, _sendingAddress.ToString(), _sendingPort);
                    //maybe some check for all bytes sent
                    return true;
                }
            }
            catch (SocketException ex)
            {
                Debug.unityLogger.LogWarning("Sending", ex);
                return false;
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
                return false;
            }
        }

        public async Task<AbstractPackage> ReceiveOnePackageAsync(int timeout = 10000)
        {
            try
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
                // Debug.unityLogger.Log(
                //     $" waiting package from {((IPEndPoint) _receivingClient.Client.LocalEndPoint).Port}");
                IPEndPoint remoteEndPoint = null;
                _receivingClient.Client.ReceiveTimeout = timeout;
                var result = _receivingClient.Receive(ref remoteEndPoint);
                var stream = new MemoryStream(result);

                var pack = (AbstractPackage) serializer.Deserialize(stream);
                pack.ipAddress = remoteEndPoint.Address;

                stream.Close();
                return pack;
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.TimedOut)
                {
                    Debug.unityLogger.Log("Remote node doesn't respond");
                }
                Debug.unityLogger.LogException(ex);
            }

            return null;
        }
        
        public void BeginReceivingPackage()
        {
            try
            {
                var asyncResult = _receivingClient.BeginReceive(EndReceivePackage, isContinueReceiving);
            }
            catch (SocketException ex)
            {
                Debug.unityLogger.LogWarning("Receiving:", ex);
            }
            catch (ObjectDisposedException ex)
            {
                Debug.unityLogger.Log("Connection is closed");
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
            }
        }

        private void EndReceivePackage(IAsyncResult asyncResult)
        {
            try
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
                // Debug.unityLogger.Log(
                //     $"waiting package from port {((IPEndPoint) _receivingClient.Client.LocalEndPoint).Port}");
                IPEndPoint remoteEndPoint = null;
                var result = _receivingClient.EndReceive(asyncResult, ref remoteEndPoint);

                // Debug.unityLogger.Log($"received package from {remoteEndPoint.Address}");

                var stream = new MemoryStream(result);

                var pack = (AbstractPackage) serializer.Deserialize(stream);
                pack.ipAddress = remoteEndPoint.Address;

                stream.Close();

                NetEventStorage.GetInstance().newPackageRecieved.Invoke(pack);
                if((bool)asyncResult.AsyncState)
                    BeginReceivingPackage();
            }
            catch(Exception ex)
            {
                Debug.unityLogger.LogError("shit on receiving", ex);
            }
        }

        public IPAddress GetSendingAddress()
        {
            return _sendingAddress;
        }

        public int GetListeningPort()
        {
            return ((IPEndPoint)_receivingClient.Client.LocalEndPoint).Port;
        }

        public void Dispose()
        {
            isContinueReceiving = false;
            _receivingClient.Close();
            _receivingClient?.Dispose();
        }
    }
}
