using System;
using System.Net;
using System.Threading.Tasks;
using Net.Interfaces;
using Net.Packages;
using UnityEngine;

namespace Net.Core
{
    /// <summary>
    /// ClientListener слушает определенный адрес и порт (клиента). Принимает от него пакеты и отправляет пакеты ему.
    /// Подписан на событие updateWorldState - отправляет состояние мира своему клиенту
    /// При получении пакета от клиента - передает его на обработку вызовом события newPackageRecieved
    /// </summary>
    public class ClientListener: IDisposable
    {
        private StarfighterUdpClient _udpSocket;

        public ClientListener(IPAddress address, int sendingPort,  int listeningPort)
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

        public void Dispose()
        {
            _udpSocket.SendPackageAsync(new DisconnectPackage(null));
        }
    }
}
