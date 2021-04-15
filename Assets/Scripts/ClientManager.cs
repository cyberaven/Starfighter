using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Core;
using Net.PackageData;
using Net.Packages;
using Net.Utils;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

/// <summary>
/// ClientManager занимается управлением Client'ов.
/// Создает, удаляет, хранит список. 
/// Также отвечает за инициацию бродкаст отправки.
/// </summary>
namespace Net.Core
{
    public class ClientManager : Singleton<ClientManager>, IDisposable
    {
        private int _lastGivenPort = 12000;

        public List<Client> ConnectedClients;
        [SerializeField] private List<ClientAccountObject> accountObjects;

        private new void Awake()
        {
            base.Awake();
            
            ConnectedClients = new List<Client>();
            
            NetEventStorage.GetInstance().disconnectClient.AddListener(DisconnectClient);
            NetEventStorage.GetInstance().connectClient.AddListener(ConnectClient);
            NetEventStorage.GetInstance().worldInitDone.AddListener((client) => { ConnectedClients.Add(client);});
            NetEventStorage.GetInstance().wayPointSetted.AddListener(SendWayPointInfo);
        }

        private void AddClient(ConnectPackage info)
        {
            var account = accountObjects.Find(acc => acc.login == info.data.login && acc.password == info.data.password);
            var client = new Client(
                info.ipAddress, info.data.portToReceive, info.data.portToSend, account);
        }
        
        private int GetNewPort()
        {
            return ++_lastGivenPort;
        }
        
        private bool CheckAuthorization(ConnectPackage pack)
        {
            return !ConnectedClients.Any(client => Equals(client.GetIpAddress(), pack.ipAddress)) &&
                   accountObjects.Any(acc => acc.login == pack.data.login && acc.password == pack.data.password);
        }

        private void ConnectClient(ConnectPackage pack)
        {
            Debug.unityLogger.Log("Connection handle start");

            Debug.unityLogger.Log($"Acc {pack.data.accountType}:  {pack.data.login}:{pack.data.password}");
                
            if (CheckAuthorization(pack))
            {  
                Debug.Log($"Connection accepted: {pack.ipAddress.MapToIPv4()}");

                pack.data.multicastGroupIp = Constants.MulticastAddress;
                pack.data.portToSend = GetNewPort();
                pack.data.portToReceive = GetNewPort();
                
                Dispatcher.Instance.Invoke(() => AddClient(pack));
                
                SenderHelper.SendConnectionResponse(pack);
                return;
            }
                
            Debug.Log("Connection declined (this endpoint already connected) or there is no such account");
                
            SenderHelper.SendConnectionResponse(new DeclinePackage(new DeclineData())
            {
                ipAddress = pack.ipAddress,
            });
        }

        private void DisconnectClient(DisconnectPackage pack)
        {
            if (ConnectedClients.Any(cl => Equals(cl.GetIpAddress(), pack.ipAddress)))
            {
                var client = ConnectedClients
                    .FirstOrDefault(cl => Equals(cl.GetIpAddress(), pack.ipAddress));
                    
                Debug.unityLogger.Log($"Disconnection: {client?.GetIpAddress()}:{client?.GetListeningPort()}");
                ConnectedClients.Remove(client);
                client?.Dispose();
            }
            else
            {
                //There is no such client to Disconnect;
                Debug.unityLogger.Log("Server : There is no such client to Disconnect!");
            }
        }
        
        public void RegisterAccount(ClientAccountObject acc)
        {
            accountObjects.Add(acc);
        }

        public void UnregisterAccount(ClientAccountObject acc)
        {
            accountObjects.Remove(acc);
        }
        
        public async void SendToAll(AbstractPackage package, IPAddress sender = null)
        {
            foreach (var client in ConnectedClients)
            {
                if (Equals(client.GetIpAddress(), sender)) continue;
                await client.SendPackage(package);
            }
        }

        private async void SendWayPointInfo(IPAddress address, WorldObject waypoint)
        {
            var shipName = ConnectedClients.First(x => Equals(x.GetIpAddress(), address)).GetShipGOName();
            var clientToSend = ConnectedClients.First(x => Equals(x.GetShipGOName(), shipName) && !Equals(x.GetIpAddress(), address));
            await clientToSend.SendPackage(new StatePackage(new StateData()
            {
                worldState = new []{waypoint}
            }));
        }

        public void Dispose()
        {
            ConnectedClients.ForEach(client => client.Dispose());
        }
        
    }
}
