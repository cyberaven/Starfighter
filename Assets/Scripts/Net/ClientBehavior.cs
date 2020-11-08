using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Net.Core;
using Net.PackageData;
using Net.PackageHandlers;
using Net.Packages;
using Net.Utils;
using UnityEngine;
using UnityEngine.Serialization;

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
        private HandlerManager _handlerManager; //TODO: client handler manager should be here
        
        private void Awake()
        {
            _udpSocket = new UdpSocket(serverEndPoint, Constants.ClientReceivingPort);
            _listening = new Task(ListenServer);
            _handlerManager = HandlerManager.getInstance();
        }

        private void Start()
        {
            var connectData = new ConnectData()
            {
                Login = login, Password = password, AccountType = accType
            };
            
            var thread = new Thread(async () =>
            {
                
                await _udpSocket.SendPackageAsync(new ConnectPackage(connectData));
                Debug.unityLogger.Log($"connection package sent");
                var result = await _udpSocket.ReceivePackageAsync();
                Debug.unityLogger.Log($"response package received: {result.PackageType}");
                if (result.PackageType == PackageType.AcceptPackage)
                {
                    _listening.Start();
                }
                else if (result.PackageType == PackageType.DeclinePackage)
                {
                    //TODO: Return to login screen
                }
                //TODO: Reaction to others packs?
            });
            Debug.unityLogger.Log("thread start");
            thread.Start();
        }

        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
            Debug.unityLogger.Log($"ClientBehavior fixedUpdate. Task status - {_listening.Status}");
            if (_listening == null) return;

            if(_listening.IsCompleted)
            {
                _listening.Start();
            }
        }
        
        private async void ListenServer()
        {
            var package = await _udpSocket.ReceivePackageAsync();
            EventBus.getInstance().newPackageRecieved.Invoke(package);
        }
        
    }
}