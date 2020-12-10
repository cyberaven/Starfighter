using System;
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
        public string ipAddress;

        private bool _connected;
        private UdpSocket _udpSocket;
        private ClientHandlerManager _handlerManager; //client handler manager should be here
        private EventBus _eventBus;
        
        private void Awake()
        {
            var serverAddress = IPAddress.Parse(ipAddress);
            
            _udpSocket = new UdpSocket(serverAddress,Constants.ClientSendingPort,
                serverAddress,Constants.ClientReceivingPort);
            _handlerManager = ClientHandlerManager.GetInstance();
            _eventBus = EventBus.GetInstance();

            _connected = false;
        }

        private void Start()
        {
            var sphere = Resources.Load<GameObject>("Prefabs/Sphere");
            sphere.name += "_" + Guid.NewGuid();
            sphere.transform.position = Vector3.zero;
            sphere.tag = Constants.PlayerTag;
            Instantiate(sphere);
            
            
            var connectData = new ConnectData()
            {
                login = login, password = password, accountType = accType
            };
            
            Task.Run(async () =>
            {
                var x = new ConnectPackage(new ConnectData());
                _eventBus.updateWorldState.AddListener(SendWorldState);
                await _udpSocket.SendPackageAsync(new ConnectPackage(connectData));
                Debug.unityLogger.Log($"connection package sent");
                var result = await _udpSocket.ReceiveOnePackage();
                Debug.unityLogger.Log($"response package received: {result.packageType}");
                if (result.packageType == PackageType.AcceptPackage)
                {
                    Debug.unityLogger.Log("Server accept our connection");
                    StartListenServer();
                    _connected = true;
                }
                else if (result.packageType == PackageType.DeclinePackage)
                {
                    Debug.unityLogger.Log("Server decline our connection");
                    //TODO: Return to login screen
                }
            });
        }

       

        private void Update()
        {

            // if(_listening != null 
            //    && (_listening.Status == TaskStatus.RanToCompletion
            //        || _listening.Status == TaskStatus.Canceled
            //        || _listening.Status == TaskStatus.Faulted)
            //    )
            // {
            //     _listening = Task.Run(StartListenServer);
            // }
            if(_connected) _eventBus.updateWorldState.Invoke(GetWorldStatePackage().Result);
        }
        
        private void FixedUpdate()
        {
            Dispatcher.Instance.InvokePending();
        }
         
        private async Task<StatePackage> GetWorldStatePackage()
        {
            Debug.unityLogger.Log("ClientBehavior.GetWorldStatePackage");
            var gameObjects = GameObject.FindGameObjectsWithTag(Constants.PlayerTag);
            var worldData = new StateData()
            {
                worldState = gameObjects.Select(go => new WorldObject(go.name, go.transform)).ToArray()
            };
            return new StatePackage(worldData);
        }
        
        private void StartListenServer()
        {
            _udpSocket.BeginReceivingPackagesAsync();
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

        private void OnApplicationQuit()
        {
            _udpSocket.SendPackageAsync(new DisconnectPackage(new DisconnectData()
            {
                accountType = accType,
                login = login,
                password = password,
            }));
        }
    }
}