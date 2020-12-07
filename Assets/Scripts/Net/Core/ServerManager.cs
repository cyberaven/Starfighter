using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Net.Interfaces;
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
        
        public List<ClientListener> ConnectedClients;

        private ServerManager()
        {
            ConnectedClients = new List<ClientListener>();
            EventBus.GetInstance().sendBroadcast.AddListener(BroadcastSendingAsync);
        }

        public static ServerManager GetInstance()
        {
            return _instance;
        }

        public async void BroadcastSendingAsync(AbstractPackage pack)
        {
            var ipAddresses = ConnectedClients.Select(client => client.GetIpAddress()).ToList();
            var tasks = new Task[ipAddresses.Count];
            foreach (var ipAddress in ipAddresses)
            {
                var udp = new UdpSocket(ipAddress, Constants.ServerSendingPort,
                    ipAddress, Constants.ServerReceivingPort);
                tasks.Append(Task.Run(()=> udp.SendPackageAsync(pack)));
            }
            await Task.WhenAll(tasks);
        }

        public void Dispose()
        {
            ConnectedClients.ForEach(client => client.Dispose());
        }
    }
}
