using System.Collections.Generic;
using System.Threading;
using Net.Core;
using Net.Interfaces;
using Net.Packages;
using UnityEngine;

namespace Net
{
    public class MainServerLoop : MonoBehaviour
    {
        [SerializeField]
        private ServerManager servManager;
        [SerializeField]
        private EventBus eventBus;

        public static List<IPackage> _packagesToHandle = new List<IPackage>();


        private void Awake()
        {
            servManager = ServerManager.Instance;
            eventBus = EventBus.Instance;
            //Config init
            
            //Events binding
            EventBus.Instance.sendDecline.AddListener(ServerResponse.SendDecline);
            EventBus.Instance.sendAccept.AddListener(ServerResponse.SendAccept);
        }

        // Use this for initialization
        void Start()
        {
            //TODO: Вероятно намудрил чего-то не того.
            //servManager.connectedClients.Add(new ClientListener(null, null));

            var thread = new Thread(
                new ThreadStart(async () => { await servManager.WaitForConnectionAsync(new UdpSocket()); } ));
            thread.Start();
        }

        void FixedUpdate()
        {
            //Send WorldState to every client
            eventBus.updateWorldState.Invoke(GetWorldStatePackage());
        }

        StatePackage GetWorldStatePackage()
        {
            Debug.Log("TO DO: MainServerLoop.GetWorldStatePackage");
            return null;
        }

        private void OnDestroy()
        {
            servManager.Dispose();
            eventBus.Dispose();
        }
    }

}
