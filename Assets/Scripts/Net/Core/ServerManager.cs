using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Client;
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
        
        public List<Client> ConnectedClients;

        private ServerManager()
        {
            ConnectedClients = new List<Client>();
            EventBus.GetInstance().serverMovePlayer.AddListener(MoveEvent);
        }

        public static ServerManager GetInstance()
        {
            return _instance;
        }

        public void AddClient(ConnectPackage info)
        {
            ConnectedClients.Add(new Client(
                info.ipAddress, info.data.portToReceive, info.data.portToSend));
        }
        
        public int GetNewPort()
        {
            return ++_lastGivenPort;
        }

        public void MoveEvent(IPAddress address, EngineState state)
        {
            var client = ConnectedClients.First(x => Equals(x.GetIpAddress(), address));
            //TODO: turn on\off engines for define client's GO
            // client.SetEngines(state);
        }
        
        public void Dispose()
        {
            ConnectedClients.ForEach(client => client.Dispose());
        }
        
    }
}
