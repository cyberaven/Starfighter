using UnityEngine;
using System.Collections.Generic;
using Server.Interfaces;
using System.Net;
using Server.Utils.Enums;
using System.Threading.Tasks;
using System.Linq;
using Core;

/// <summary>
/// ServerManager занимается управлением ClientListener'ов.
/// Создает, удаляет, хранит список. 
/// Также отвечает за инициацию бродкаст отправки.
/// </summary>
namespace Server.Core
{
    public class ServerManager : Singleton<ServerManager>
    {
        public List<ClientListener> connectedClients;

        private ServerManager()
        {
            connectedClients = new List<ClientListener>();
            EventBus.Instance.sendBroadcast.AddListener(BroadcastSendingAsync);
        }

        public async Task WaitForConnectionAsync(UdpSocket waiter)
        {
            var res =  await waiter.RecievePackageAsync();
            switch (res.PackageType)
            {
                case PackageType.ConnectPackage:
                    //check for LoginPassword (decline if incorrect)
                    //check for already connection (decline if yes)
                    //create new async ClientListener for it
                    //init new user
                    break;
                case PackageType.DisconnectPackage:
                case PackageType.AcceptPackage:
                case PackageType.DeclinePackage:
                case PackageType.EventPackage:
                case PackageType.StatePackage:
                    // отдать куда-то на обработку?
                    break;
            }
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
    }
}
