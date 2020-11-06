using System.Collections.Generic;
using System.Threading;
using Net.Core;
using Net.Interfaces;
using Net.PackageHandlers;
using Net.Packages;
using UnityEngine;
using UnityEngine.Serialization;

namespace Net
{
    public class MainServerLoop : MonoBehaviour
    {
        [SerializeField]
        private ServerManager _servManager;
        [SerializeField]
        private EventBus eventBus;
        [SerializeField]
        private HandlerManager _handlerManager;


        public static List<IPackage> _packagesToHandle = new List<IPackage>();


        private void Awake()
        {
            _servManager = ServerManager.Instance;
            eventBus = EventBus.Instance;
            _handlerManager = HandlerManager.Instance;
            //Config init
            
            //Events binding
            EventBus.Instance.sendDecline.AddListener(ServerResponse.SendDecline);
            EventBus.Instance.sendAccept.AddListener(ServerResponse.SendAccept);
        }

        // Use this for initialization
        private void Start()
        {
            //TODO: Вероятно намудрил чего-то не того.
            //servManager.connectedClients.Add(new ClientListener(null, null));

            var thread = new Thread(async () => { await _servManager.WaitForConnectionAsync(new UdpSocket()); });
            thread.Start();
        }

        private void FixedUpdate()
        {
            //Send WorldState to every client
            eventBus.updateWorldState.Invoke(GetWorldStatePackage());
        }

        private StatePackage GetWorldStatePackage()
        {
            Debug.Log("TO DO: MainServerLoop.GetWorldStatePackage");
            return null;
        }

        private void OnDestroy()
        {
            _servManager.Dispose();
            eventBus.Dispose();
        }
    }

}
