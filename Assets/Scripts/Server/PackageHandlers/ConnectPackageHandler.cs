using UnityEngine;
using System.Collections;
using Server.Interfaces;
using Server.Core;
using System.Linq;
using System.Threading.Tasks;

namespace Server.PackageHandlers
{

    public class ConnectPackageHandler : IPackageHandler
    {
        //TODO: Как подписать на событие? Сделать статическим и подписать не тут? Сделать динамическим и реализовать конструктор?

        public async Task Handle(IPackage pack)
        {
            //check for LoginPassword (decline if incorrect)
            //check for already connection (decline if yes)
            if(ServerManager.Instance.connectedClients.First(client => client.GetEndPoint() == pack.IpEndPoint))
            {
                EventBus.Instance.sendDecline.Invoke(pack);
            }
            //create new async ClientListener for it
            ServerManager.Instance.connectedClients.Add(new ClientListener(pack.IpEndPoint, pack));
            //init new user
            //send World State to him
        }
    }
}