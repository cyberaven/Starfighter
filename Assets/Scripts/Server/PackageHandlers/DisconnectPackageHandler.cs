using UnityEngine;
using System.Collections;
using Server.Interfaces;
using Server.Core;
using System.Linq;
using System.Threading.Tasks;

namespace Server.PackageHandlers
{

    public class DisconnectPackageHandler : IPackageHandler
    {
        //TODO: Как подписать на событие? Сделать статическим и подписать не тут? Сделать динамическим и реализовать конструктор?

        public async Task Handle(IPackage pack)
        {
            if(ServerManager.Instance.connectedClients.First(client => client.GetEndPoint() == pack.IpEndPoint))
            {
                ServerManager.Instance.connectedClients.RemoveAll(client => client.GetEndPoint() == pack.IpEndPoint);
                EventBus.Instance.sendAccept.Invoke(pack);
                //delete all GO attached to this IPEndpoint
            }
            else
            {
                //There is no such client to Disconnect;
                EventBus.Instance.sendDecline.Invoke(pack);
            }
        }
    }
}