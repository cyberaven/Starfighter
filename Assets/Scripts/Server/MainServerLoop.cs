using UnityEngine;
using System.Collections;
using Server.Core;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.Events;
using Server.Packages;
using Server.PackageData;
using System.Collections.Generic;
using Server.Interfaces;

namespace Server
{

    public class MainServerLoop : ScriptableObject
    {
        public ServerManager servManager;
        public EventBus eventBus;

        public static List<IPackageHandler> _packageHandlers = new List<IPackageHandler>(); //вероятно не нужен

        public static List<IPackage> _packagesToHandle = new List<IPackage>();

        // Use this for initialization
        void Start()
        {
            servManager = ServerManager.Instance;
            eventBus = EventBus.Instance;
            //Config init

            //Events binding
            EventBus.Instance.sendDecline.AddListener(ServerResponse.SendDecline);
            EventBus.Instance.sendAccept.AddListener(ServerResponse.SendAccept);

            //TODO: Вероятно намудрил чего-то не того.
            servManager.connectedClients.Add(new ClientListener(null, null));

            var thread = new Thread(
                new ThreadStart(async () => { await servManager.WaitForConnectionAsync(new UdpSocket()); } ));
            thread.Start();
            
        }

        void FixedUpdate()
        {
            //SendBroadcast WorldState
            eventBus.updateWorldState.Invoke(GetWorldStatePackage());
        }

        StatePackage GetWorldStatePackage()
        {
            return null;
        }

        private void OnDestroy()
        {
            servManager.Dispose();
            eventBus.Dispose();
        }
    }

}
