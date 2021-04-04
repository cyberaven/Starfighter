using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Client;
using Core;
using Net.PackageData;
using Net.PackageData.EventsData;
using Net.Packages;
using Net.Utils;
using ScriptableObjects;
using UnityEngine;
using Utils;
using EventType = Net.Utils.EventType;

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
            try
            {
                NetEventStorage.GetInstance().serverMovedPlayer.AddListener(UpdateMovement);

                _accountType = account.type;

                _playerScript = InstantiateHelper.InstantiateServerShip(account.ship);

                // _myGameObjectId = Guid.Parse(_playerScript.gameObject.name.Split('_')[1]);

                _udpSocket = new StarfighterUdpClient(address, sendingPort, listeningPort);

                WorldInit();
                
                StartListenClient();
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
            }
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
        
        public async Task SendDecline(Guid id)
        {
            Debug.unityLogger.Log($"Gonna send decline to: {_udpSocket.GetSendingAddress()}");
            
            var result = await _udpSocket.SendPackageAsync(new DeclinePackage(new DeclineData(){eventId = id}));
        }

        public async Task SendAccept(Guid id)
        {
            Debug.unityLogger.Log($"Gonna send accept to: {_udpSocket.GetSendingAddress()}:{Constants.ServerSendingPort}");
            var result = await _udpSocket.SendPackageAsync(new AcceptPackage(new AcceptData(){eventId = id}));
        }

        public async Task SendWorldState(StateData data)
        {
            var result = await _udpSocket.SendPackageAsync(new StatePackage(data));
        }

        private async Task WorldInit()
        {
            var asteroids = GameObject.FindGameObjectsWithTag(Constants.AsteroidTag);
            await _udpSocket.SendEventPackage(asteroids.Length, EventType.InitEvent);
            MainServerLoop.instance.LaunchCoroutine(WorldInitCoroutine(asteroids.ToList()));
        }

        private IEnumerator WorldInitCoroutine(List<GameObject> asteroids, int rangeSize = 10)
        {
            while (asteroids.Count > 0)
            {
                var range = asteroids.GetRange(0, Mathf.Min(rangeSize, asteroids.Count));
                asteroids.RemoveRange(0, Mathf.Min(rangeSize, asteroids.Count));
                SendWorldState(new StateData()
                {
                    worldState = range.Select(x => new WorldObject(x.name, x.transform)).ToArray()
                });
                
                yield return null;
            }
        }
        
        
        public void Dispose()
        {
            _udpSocket.Dispose();
        }
    }
}
