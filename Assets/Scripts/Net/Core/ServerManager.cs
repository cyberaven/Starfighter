using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Net.Interfaces;
using Net.PackageData;
using Net.Packages;
using UnityEngine;

/// <summary>
/// ServerManager занимается управлением ClientListener'ов.
/// Создает, удаляет, хранит список. 
/// Также отвечает за инициацию бродкаст отправки.
/// </summary>
namespace Net.Core
{
    public class ServerManager : IDisposable
    {
        private static ServerManager _instance = new ServerManager();
        private int _lastGivenPort = 8000;
        
        public List<ClientListener> ConnectedClients;

        private ServerManager()
        {
            ConnectedClients = new List<ClientListener>();
        }

        public static ServerManager GetInstance()
        {
            return _instance;
        }

        public void AddClient(ConnectPackage info)
        {
            ConnectedClients.Add(new ClientListener(
                info.ipAddress, info.data.portToReceive, info.data.portToSend));
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
