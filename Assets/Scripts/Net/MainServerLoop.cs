using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Net.Core;
using Net.PackageData;
using Net.PackageHandlers.ServerHandlers;
using Net.Packages;
using Net.Utils;
using ScriptableObjects;
using UnityEngine;

namespace Net
{
    public class MainServerLoop : MonoBehaviour
    {

        private ClientManager _clientManager;
        private EventBus _eventBus;
        private HandlerManager _handlerManager;
        private StarfighterUdpClient _multicastUdpClient;
        public readonly List<ClientAccountObject> AccountObjects;
        
        private void Awake()
        {
            _eventBus = EventBus.GetInstance();
            _clientManager = ClientManager.GetInstance();
            _handlerManager = HandlerManager.GetInstance();

            ConfigInit();

            _eventBus.addClient.AddListener(AddNewClient);
            _eventBus.updateWorldState.AddListener(SendWorldState);
        }

        private void ConfigInit()
        {
            
        }

        private void ConfigSave()
        {
            
        }
        
        private void Start()
        {
            _multicastUdpClient = new StarfighterUdpClient(IPAddress.Parse(Constants.MulticastAddress),
                Constants.ServerSendingPort, Constants.ServerReceivingPort);
            Debug.Log($"start waiting connection packs from anyone: {Constants.ServerReceivingPort}");
            _multicastUdpClient.BeginReceivingPackage();
        }

        private void Update()
        {
            try
            {
                _eventBus.updateWorldState.Invoke(GetWorldStatePackage().Result);
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
            }
        }
        
        private void FixedUpdate()
        {
            Dispatcher.Instance.InvokePending();
        }

        private async Task<StatePackage> GetWorldStatePackage()
        {
            Debug.unityLogger.Log("MainServerLoop.GetWorldStatePackage");
            var gameObjects = GameObject.FindGameObjectsWithTag(Constants.DynamicTag);
            var worldData = new StateData()
            {
                worldState = gameObjects.Select(go => new WorldObject(go.name,go.transform)).ToArray()
            };
            return new StatePackage(worldData);
        }

        private void AddNewClient(ConnectPackage info)
        {
            _clientManager.AddClient(info);
        }

        private async void SendWorldState(StatePackage pack)
        {
            await _multicastUdpClient.SendPackageAsync(pack);
        }
        
        private void OnDestroy()
        {
            _clientManager.Dispose();
            _eventBus.Dispose();
            ConfigSave();
        }
    }

}
