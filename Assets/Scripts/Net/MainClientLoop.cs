using System;
using System.Net;
using Client;
using Core;
using Core.InputManager;
using Net.Core;
using Net.PackageData;
using Net.PackageHandlers.ClientHandlers;
using Net.Packages;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Net
{
    [RequireComponent(typeof(ClientHandlerManager))]
    [RequireComponent(typeof(InputManager))]
    public class MainClientLoop : Singleton<MainClientLoop>
    {
        public ClientAccountObject accountObject;
        public string serverAddress;

        //Прием accept\decline пакетов, отправка данных и команд. Личный канал с сервером.
        private StarfighterUdpClient _udpClient;
        //Прием State пакетов от сервера. Общий канал
        private StarfighterUdpClient _multicastUdpClient;

        private PlayerScript _playerScript = null;
        
        private new void Awake()
        {
            base.Awake();

        }

        private void Start()
        {
            NetEventStorage.GetInstance().connectToServer.AddListener(ConnectToServer);
            CoreEventStorage.GetInstance().AxisValueChanged.AddListener(SendMove);
        }
        
        
        public void SendMove(string axis, float value)
        {
            NetEventStorage.GetInstance().sendMoves.Invoke(_udpClient);
        }

        public bool TryAttachPlayerControl(PlayerScript playerScript)
        {
            if (_playerScript is null && playerScript.gameObject.name.Split('_')[1] == accountObject.ship.shipId
            && accountObject.type != UserType.Navigator && accountObject.type != UserType.Spectator)
            {
                _playerScript = playerScript;
                
                //TODO: NetEventStorage.playerInit.Invoke();
                return true;
            }
            return false;
        }
        private void Update()
        {
            // if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) ||
            //     Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) ||
            //     Input.GetKeyDown(KeyCode.Space))
            // { 
            //     SendMove("", 0);
            // }
            //
            // if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) ||
            //     Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E) ||
            //     Input.GetKeyUp(KeyCode.Space))
            // {
            //     SendMove("", 0);
            // }
            // NetEventStorage.GetInstance().updateWorldState.Invoke(GetWorldStatePackage().Result);
        }
        
        private void FixedUpdate()
        {
            Dispatcher.Instance.InvokePending();
        }
        
        private void StartListenServer()
        {
            _multicastUdpClient.BeginReceivingPackage();
            _udpClient.BeginReceivingPackage();
        }
        
        private void ConnectToServer(ConnectPackage result)
        {
            //надо иметь два udp клиента. Для прослушки multicast и для прослушки личного порта от сервера.
            Debug.unityLogger.Log("Server accept our connection");
            try
            {
                _udpClient = new StarfighterUdpClient(IPAddress.Parse(serverAddress),
                    (result as ConnectPackage).data.portToSend,
                    (result as ConnectPackage).data.portToReceive);

                var multicastAddress = IPAddress.Parse((result as ConnectPackage).data.multicastGroupIp);
                _multicastUdpClient = new StarfighterUdpClient(multicastAddress,
                    Constants.ServerReceivingPort, Constants.ServerSendingPort);
                _multicastUdpClient.JoinMulticastGroup(multicastAddress);
                        
                StartListenServer();
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogError("Connect to Server error:", ex.Message);
            }
        }

        private void OnDestroy()
        {
            ClientHandlerManager.instance.Dispose();
        }

        private async void OnApplicationQuit()
        {
            await _udpClient.SendPackageAsync(new DisconnectPackage(new DisconnectData()
            {
                accountType = accountObject.type,
                login = accountObject.login,
                password = accountObject.password,
            }));
            
            ClientHandlerManager.instance.Dispose();
            _udpClient.Dispose();
        }
    }
}