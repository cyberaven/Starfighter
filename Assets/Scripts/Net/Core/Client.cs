using System;
using System.Net;
using Client;
using Core;
using Net.PackageData;
using Net.PackageData.EventsData;
using Net.Packages;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Net.Core
{
    /// <summary>
    /// Client слушает определенный адрес и порт (клиента). Принимает от него пакеты и отправляет пакеты ему.
    /// </summary>
    public class Client: IDisposable
    {
        private StarfighterUdpClient _udpSocket;
        private PlayerScript _playerScript;
        private Guid _myGameObjectId;
        private UserType _accountType;

        public Client(IPAddress address, int sendingPort,  int listeningPort, ClientAccountObject account)
        {
            NetEventStorage.GetInstance().serverMovedPlayer.AddListener(UpdateMovement);

            _accountType = account.type;
            
            _playerScript = InstantiateHelper.InstantiateServerShip(account.ship);
            
            // _myGameObjectId = Guid.Parse(_playerScript.gameObject.name.Split('_')[1]);
            
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

        private void StartListenClient()
        {
            _udpSocket.BeginReceivingPackage();
        }

        private void UpdateMovement(IPAddress address, MovementEventData data)
        {
            if (!Equals(GetIpAddress(), address)) return;
            _playerScript.ShipsBrain.UpdateMovementActionData(data);
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
            _udpSocket.Dispose();
        }
    }
}
