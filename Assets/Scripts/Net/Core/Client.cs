using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Client;
using Core;
using Enums;
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
        private string _myGameObjectName;
        private UserType _accountType;

        public Client(IPAddress address, int sendingPort,  int listeningPort, ClientAccountObject account)
        {
            try
            {
                NetEventStorage.GetInstance().serverMovedPlayer.AddListener(UpdateMovement);

                _accountType = account.type;

                _playerScript = InstantiateHelper.InstantiateServerShip(account.ship);

                _myGameObjectName = _playerScript.gameObject.name;

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

        public string GetShipGOName()
        {
            return _myGameObjectName;
        }

        private void StartListenClient()
        {
            _udpSocket.BeginReceivingPackage();
        }

        private void UpdateMovement(IPAddress address, MovementEventData data)
        {
            if (!Equals(GetIpAddress(), address)) return;
            _playerScript.ShipsBrain.UpdateMovementActionData(data);
            ClientManager.instance.SendToAll(new EventPackage(new EventData()
            {
                data = (_myGameObjectName, data),
                eventType = EventType.MoveEvent
            }), GetIpAddress());
        }

        private async Task SendDecline(DeclineData data)
        {
            Debug.unityLogger.Log($"Gonna send decline to: {_udpSocket.GetSendingAddress()}");
            
            var result = await _udpSocket.SendPackageAsync(new DeclinePackage(data));
        }

        private async Task SendAccept(AcceptData data)
        {
            Debug.unityLogger.Log($"Gonna send accept to: {_udpSocket.GetSendingAddress()}:{Constants.ServerSendingPort}");
            var result = await _udpSocket.SendPackageAsync(new AcceptPackage(data));
        }

        private async Task SendWorldState(StateData data)
        {
            var result = await _udpSocket.SendPackageAsync(new StatePackage(data));
        }

        private async Task SendEvent(EventData data)
        {
            var result = await _udpSocket.SendEventPackage(data.data, data.eventType);
        }

        private async Task SendDisconnect(DisconnectData data)
        {
            var result = await _udpSocket.SendPackageAsync(new DisconnectPackage(data));
        }
        
        public async Task SendPackage(AbstractPackage pack)
        {
            switch (pack.packageType)
            {
                case PackageType.DisconnectPackage:
                    await SendDisconnect((pack as DisconnectPackage).data);
                    break;
                case PackageType.AcceptPackage:
                    await SendAccept((pack as AcceptPackage).data);
                    break;
                case PackageType.DeclinePackage:
                    await SendDecline((pack as DeclinePackage).data);
                    break;
                case PackageType.EventPackage:
                    await SendEvent((pack as EventPackage).data);
                    break;
                case PackageType.StatePackage:
                    await SendWorldState((pack as StatePackage).data);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async void WorldInit()
        {
            var asteroids = GameObject.FindGameObjectsWithTag(Constants.AsteroidTag);
            Debug.unityLogger.Log(await _udpSocket.SendEventPackage(asteroids.Length, EventType.InitEvent));
            MainServerLoop.instance.LaunchCoroutine(WorldInitCoroutine(asteroids.ToList(), 100));
        }

        private IEnumerator WorldInitCoroutine(List<GameObject> asteroids, int rangeSize = 10)
        {
            while (asteroids.Count > 0)
            {
                var range = asteroids.GetRange(0, Mathf.Min(rangeSize, asteroids.Count));
                asteroids.RemoveRange(0, Mathf.Min(rangeSize, asteroids.Count));
                Debug.unityLogger.Log($"sended {range.Count} asteroids, {asteroids.Count} remains");
                SendWorldState(new StateData()
                {
                    worldState = range.Select(x => new Asteroid(x.name, x.transform)).ToArray()
                });
                
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(0.5f);
            NetEventStorage.GetInstance().worldInitDone.Invoke(this);
        }
        
        public void Dispose()
        {
            NetEventStorage.GetInstance().serverMovedPlayer.RemoveListener(UpdateMovement);
            _udpSocket.Dispose();
        }
    }
}
