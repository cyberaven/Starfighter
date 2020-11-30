using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Net.Core;
using Net.PackageData;
using Net.PackageHandlers.ClientHandlers;
using Net.Packages;
using Net.Utils;
using UnityEngine;

namespace Net
{
    public class ClientBehavior : MonoBehaviour
    {
        public AccountType accType;
        public string login;
        public string password;
        public IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, Constants.ClientSendingPort);

        private UdpSocket _udpSocket;
        private Task _listening;
        private ClientHandlerManager _handlerManager; //client handler manager should be here
        private EventBus _eventBus;
        
        private void Awake()
        {
            _udpSocket = new UdpSocket(serverEndPoint, Constants.ClientReceivingPort);
            
            _handlerManager = ClientHandlerManager.getInstance();
            _eventBus = EventBus.getInstance();
        }

        private void Start()
        {
            var connectData = new ConnectData()
            {
                login = login, password = password, accountType = accType
            };
            
            var thread = new Thread(async () =>
            {
                _eventBus.updateWorldState.AddListener(SendWorldState);
                await _udpSocket.SendPackageAsync(new ConnectPackage(connectData));
                Debug.unityLogger.Log($"connection package sent");
                var result = await _udpSocket.ReceivePackageAsync();
                Debug.unityLogger.Log($"response package received: {result.PackageType}");
                if (result.PackageType == PackageType.AcceptPackage)
                {
                    _listening = Task.Run(ListenServer);
                }
                else if (result.PackageType == PackageType.DeclinePackage)
                {
                    //TODO: Return to login screen
                }
            });
            Debug.unityLogger.Log("thread start");
            thread.Start();
        }

        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
            Debug.unityLogger.Log($"ClientBehavior fixedUpdate. Task status - {_listening?.Status}");
            if (_listening == null) return;

            if(_listening != null 
               && (_listening.Status == TaskStatus.RanToCompletion
                   || _listening.Status == TaskStatus.Canceled
                   || _listening.Status == TaskStatus.Faulted)
               )
            {
                _listening = Task.Run(ListenServer);
            }
            
            _eventBus.updateWorldState.Invoke(GetWorldStatePackage().Result);
        }
        
        private async Task<StatePackage> GetWorldStatePackage()
        {
            //TODO: Get World state package
            Debug.unityLogger.Log("MainServerLoop.GetWorldStatePackage");
            var gameObjects = GameObject.FindGameObjectsWithTag("Dynamic");
            var worldData = new StateData()
            {
                worldState = gameObjects.Select(go => new WorldObject(go.name, go.transform)).ToArray()
            };
            return new StatePackage(worldData);
        }
        
        private async void ListenServer()
        {
            var package = await _udpSocket.ReceivePackageAsync();
            _eventBus.newPackageRecieved.Invoke(package);
        }
        
        private async void SendWorldState(StatePackage worldState)
        {
            await _udpSocket.SendPackageAsync(worldState);
        }
        
        private void OnDestroy()
        {
            _handlerManager.Dispose();
            _eventBus.Dispose();
        }
    }
}