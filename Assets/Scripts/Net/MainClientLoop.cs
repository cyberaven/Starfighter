using System;
using System.Collections;
using System.Linq;
using System.Net;
using Client;
using Client.Core;
using Core;
using Core.InputManager;
using Net.Core;
using Net.PackageData;
using Net.PackageHandlers.ClientHandlers;
using Net.Packages;
using Net.Utils;
using ScriptableObjects;
using UnityEngine;
using Utils;
using EventType = Net.Utils.EventType;

namespace Net
{
    [RequireComponent(typeof(ClientHandlerManager))]
    [RequireComponent(typeof(ClientInitManager))]
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
            NetEventStorage.GetInstance().connectToServer.AddListener(ConnectToServer);
            CoreEventStorage.GetInstance().AxisValueChanged.AddListener(SendMove);
        }

        public void Init(string serverAddress)
        {
            this.serverAddress = serverAddress;
        }

        public void SendMove(string axis, float value)
        {
            NetEventStorage.GetInstance().sendMoves.Invoke(_udpClient);
        }

        public void LaunchCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
        
        public bool TryAttachPlayerControl(PlayerScript playerScript)
        {
            if (_playerScript is null && playerScript.gameObject.name.Split(Constants.Separator)[1] == accountObject.ship.shipId)
            {
                _playerScript = playerScript;
                switch (accountObject.type)
                {
                    case UserType.Admin:
                        ClientEventStorage.GetInstance().InitAdmin.Invoke(_playerScript);
                        break;
                    case UserType.Pilot:
                        ClientEventStorage.GetInstance().InitPilot.Invoke(_playerScript);
                        break;
                    case UserType.Navigator:
                        ClientEventStorage.GetInstance().InitNavigator.Invoke(_playerScript);
                        break;
                    case UserType.Spectator:
                        ClientEventStorage.GetInstance().InitSpectator.Invoke(_playerScript);
                        break;
                    case UserType.Moderator:
                        ClientEventStorage.GetInstance().InitModerator.Invoke(_playerScript);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return true;
            }
            return false;
        }
        private void Update()
        {
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
            try
            {
                accountObject = Resources.LoadAll<ClientAccountObject>(Constants.PathToAccounts)
                    .First(x=>x.login == result.data.login && x.password == result.data.password);
                
                _udpClient = new StarfighterUdpClient(IPAddress.Parse(serverAddress),
                    result.data.portToSend,
                    result.data.portToReceive);

                var multicastAddress = IPAddress.Parse(result.data.multicastGroupIp);
                _multicastUdpClient = new StarfighterUdpClient(multicastAddress,
                    Constants.ServerReceivingPort, Constants.ServerSendingPort);
                _multicastUdpClient.JoinMulticastGroup(multicastAddress);
                        
                StartListenServer();
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
            }
        }

        public async void Disconnect()
        {
            Debug.unityLogger.Log("Disconnection");
            await _udpClient.SendPackageAsync(new DisconnectPackage(new DisconnectData()
            {
                accountType = accountObject.type,
                login = accountObject.login,
                password = accountObject.password,
            }));
            
            _udpClient.Dispose();
            _multicastUdpClient.Dispose();
        }

        private void OnDestroy()
        {
            ClientHandlerManager.instance.Dispose();
            InputManager.instance.Dispose();
        }

        private void OnApplicationQuit()
        {
            Disconnect();
            _udpClient.Dispose();
        }
    }
}