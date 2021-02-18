using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Net.PackageData;
using Net.Packages;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// ClientManager занимается управлением Client'ов.
/// Создает, удаляет, хранит список. 
/// Также отвечает за инициацию бродкаст отправки.
/// </summary>
namespace Net.Core
{
    public class ClientManager : Singleton<ClientManager>, IDisposable
    {
        private int _lastGivenPort = 8000;

        public List<Client> ConnectedClients;
        [SerializeField] private List<ClientAccountObject> accountObjects;

        private void Awake()
        {
            ConnectedClients = new List<Client>();
        }

        public void AddClient(ConnectPackage info)
        {
            var account = accountObjects.Find(acc => acc.login == info.data.login && acc.password == info.data.password);
            
            ConnectedClients.Add(new Client(
                info.ipAddress, info.data.portToReceive, info.data.portToSend, account));
        }

        public bool CheckAuthorization(ConnectPackage pack)
        {
            return ConnectedClients.Any(client => Equals(client.GetIpAddress(), pack.ipAddress))
                   && accountObjects.Any(acc => acc.login == pack.data.login && acc.password == pack.data.password);
        }

        public void RegisterAccount(ClientAccountObject acc)
        {
            accountObjects.Add(acc);
        }

        public void UnregisterAccount(ClientAccountObject acc)
        {
            accountObjects.Remove(acc);
        }
        
        public int GetNewPort()
        {
            return ++_lastGivenPort;
        }

        public void Dispose()
        {
            ConnectedClients.ForEach(client => client.Dispose());
        }
        
    }
}
