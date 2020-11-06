using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Net.Interfaces;

/// <summary>
/// ServerManager занимается управлением ClientListener'ов.
/// Создает, удаляет, хранит список. 
/// Также отвечает за инициацию бродкаст отправки.
/// </summary>
namespace Net.Core
{
    public class ServerManager : Singleton<ServerManager>, IDisposable
    {
        public List<ClientListener> connectedClients;

        private ServerManager()
        {
            connectedClients = new List<ClientListener>();
            EventBus.Instance.sendBroadcast.AddListener(BroadcastSendingAsync);
        }

        //used only for first connection. Can be reduce
        public async Task WaitForConnectionAsync(UdpSocket waiter)
        {
            var res =  await waiter.ReceivePackageAsync();
            EventBus.Instance.newPackageRecieved.Invoke(res);
        }

        public async void BroadcastSendingAsync(IPackage pack)
        {
            var endPoints = connectedClients.Select(client => client.GetEndPoint()).ToList();
            var tasks = new Task[endPoints.Count];
            foreach (var endPoint in endPoints)
            {
                var udp = new UdpSocket(endPoint);
                tasks.Append(Task.Run(()=> udp.SendPackageAsync(pack)));
            }
            await Task.WhenAll(tasks);
        }

        public void Dispose()
        {
            connectedClients.ForEach(client => client.Dispose());
        }
    }
}
