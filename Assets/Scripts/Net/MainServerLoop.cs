using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Net.Core;
using Net.Interfaces;
using Net.PackageData;
using Net.PackageHandlers.ServerHandlers;
using Net.Packages;
using UnityEngine;

namespace Net
{
    public class MainServerLoop : MonoBehaviour
    {
        [SerializeField]
        private ServerManager _servManager;
        [SerializeField]
        private EventBus _eventBus;
        [SerializeField]
        private HandlerManager _handlerManager;


        public static List<IPackage> _packagesToHandle = new List<IPackage>();
        
        private void Awake()
        {
            _eventBus = EventBus.getInstance();
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
            _eventBus.updateWorldState.Invoke(GetWorldStatePackage().Result);
        }

        private async Task<StatePackage> GetWorldStatePackage()
        {
            //TODO: Get World state package
            Debug.unityLogger.Log("MainServerLoop.GetWorldStatePackage");
            var gameObjects = GameObject.FindGameObjectsWithTag(Constants.DynamicTag);
            var worldData = new StateData()
            {
                worldState = gameObjects.Select(go => new WorldObject(go.name,go.transform)).ToArray()
            };
            return new StatePackage(worldData);
        }

        private void OnDestroy()
        {
            _servManager.Dispose();
            _eventBus.Dispose();
        }
    }

}
