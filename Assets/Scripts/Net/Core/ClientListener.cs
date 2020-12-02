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
        private UdpSocket _udpSocket;
        
        private Task _listening;


        public ClientListener(IPEndPoint endpoint, int listeningPort, IPackage pack)
        {
            _udpSocket = new UdpSocket(endpoint, listeningPort);
            _listening = Task.Run(ListenClient);
            EventBus.getInstance().updateWorldState.AddListener(SendWorldState);
        }

        public IPAddress GetIpAddress()
        {
            return _udpSocket.GetAddress();
        }

        public int GetListeningPort()
        {
            return _udpSocket.GetListeningPort();
        }
        
        public void Update()
        {
            Debug.unityLogger.Log($"ClientListener fixedUpdate. Task status - {_listening?.Status}");
            if (_listening == null) return;

            if(_listening != null 
               && (_listening.Status == TaskStatus.RanToCompletion
                   || _listening.Status == TaskStatus.Canceled
                   || _listening.Status == TaskStatus.Faulted)
            )
            {
                _listening = Task.Run(ListenClient);
            }
        }
        
        private async void SendWorldState(StatePackage worldState)
        {
            await _udpSocket.SendPackageAsync(worldState);
        }

        private async void ListenClient()
        {
            var package = await _udpSocket.ReceivePackageAsync();
            EventBus.getInstance().newPackageRecieved.Invoke(package);
        }

        public void Dispose()
        {
            EventBus.getInstance().updateWorldState.RemoveListener(SendWorldState);
        }
    }
}
