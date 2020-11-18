using System.Collections.Generic;
using System.Net;
using System.Threading;
using Net.Core;
using Net.Interfaces;
using Net.PackageData;
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
            eventBus = EventBus.getInstance();
            _servManager = ServerManager.getInstance();
            _handlerManager = HandlerManager.getInstance();
            //Config init
            
            //Events binding
            EventBus.getInstance().sendDecline.AddListener(ServerResponse.SendDecline);
            EventBus.getInstance().sendAccept.AddListener(ServerResponse.SendAccept);
        }

        // Use this for initialization
        private void Start()
        {
            var thread = new Thread(async () =>
            {
                await _servManager.WaitForConnectionAsync(
                    new UdpSocket(new IPEndPoint(IPAddress.Loopback, Constants.ServerSendingPort), Constants.ServerReceivingPort));
            });
            thread.Start();
        }

        private void FixedUpdate()
        {
            ServerManager.getInstance().ConnectedClients.ForEach(client=>client.Update());
            Debug.unityLogger.Log($"Clients count: {ServerManager.getInstance().ConnectedClients.Count}");
            //Send WorldState to every client
            eventBus.updateWorldState.Invoke(GetWorldStatePackage());
        }

        private StatePackage GetWorldStatePackage()
        {
            //TODO: Get World state package
            Debug.Log("TO DO: MainServerLoop.GetWorldStatePackage");
            return new StatePackage(new StateData());
        }

        private void OnDestroy()
        {
            _servManager.Dispose();
            eventBus.Dispose();
        }
    }

}
