using System;
using System.Net;
using System.Threading.Tasks;
using Client;
using Net.Interfaces;
using Net.PackageData;
using Net.Packages;
using UnityEngine;

namespace Net.Core
{
    /// <summary>
    /// ClientListener слушает определенный адрес и порт (клиента). Принимает от него пакеты и отправляет пакеты ему.
    /// Подписан на событие updateWorldState - отправляет состояние мира своему клиенту
    /// При получении пакета от клиента - передает его на обработку вызовом события newPackageRecieved
    /// </summary>
    public class Client: IDisposable
    {
        private StarfighterUdpClient _udpSocket;
        private PlayerScript playerScript;
        
        public Client(IPAddress address, int sendingPort,  int listeningPort)
        {
            _udpSocket = new StarfighterUdpClient(address, sendingPort, listeningPort);
            StartListenClient();
        }

        public IPAddress GetIpAddress()
        {
            return _udpSocket.GetSendingAddress();
        }

        public int GetListeningPort()
        {
            return _udpSocket.GetListeningPort();
        }
        
        public void Update()
        {

        }

        private void StartListenClient()
        {
            _udpSocket.BeginReceivingPackage();
        }

        public async void SendDecline(Guid id)
        {
            Debug.unityLogger.Log($"Gonna send decline to: {_udpSocket.GetSendingAddress()}");
            
            var result = await _udpSocket.SendPackageAsync(new DeclinePackage(new DeclineData(){eventId = id}));
        }

        public async void SendAccept(Guid id)
        {
            Debug.unityLogger.Log($"Gonna send accept to: {_udpSocket.GetSendingAddress()}:{Constants.ServerSendingPort}");
            var result = await _udpSocket.SendPackageAsync(new AcceptPackage(new AcceptData(){eventId = id}));
        }

        public async void SendEvent(EventData data)
        {
            Debug.unityLogger.Log($"Gonna send event to: {_udpSocket.GetSendingAddress()}:{Constants.ServerSendingPort}");
            var result = await _udpSocket.SendPackageAsync(new EventPackage(data));
        }
        
        public void Dispose()
        {
            _udpSocket.SendPackageAsync(new DisconnectPackage(null));
        }
    }
}
