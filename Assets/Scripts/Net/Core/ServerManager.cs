using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Net.Interfaces;
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
            EventBus.getInstance().sendBroadcast.AddListener(BroadcastSendingAsync);
        }

        public static ServerManager getInstance()
        {
            return _instance;
        }
        
        //used only for first connection. Can be reduce
        public async Task WaitForConnectionAsync(UdpSocket waiter)
        {
            Debug.Log($"waiting connection from anyone: {waiter.GetAddress()}:{Constants.ServerReceivingPort}");
            var res =  await waiter.ReceivePackageAsync();
            Debug.Log($"received package");
            EventBus.getInstance().newPackageRecieved.Invoke(res);
        }

        public async void BroadcastSendingAsync(IPackage pack)
        {
            var ipAddresses = ConnectedClients.Select(client => client.GetIpAddress()).ToList();
            var tasks = new Task[ipAddresses.Count];
            foreach (var ipAddress in ipAddresses)
            {
                var udp = new UdpSocket(new IPEndPoint(ipAddress, Constants.ServerSendingPort), Constants.ServerReceivingPort);
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
