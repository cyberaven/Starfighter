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
    [RequireComponent(typeof(ClientManager))]
    [RequireComponent(typeof(HandlerManager))]
    public class MainServerLoop : MonoBehaviour
    {
        private StarfighterUdpClient _multicastUdpClient;

        private void Awake()
        {
            ConfigInit();
            NetEventStorage.GetInstance().updateWorldState.AddListener(SendWorldState);
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
                NetEventStorage.GetInstance().updateWorldState.Invoke(GetWorldStatePackage().Result);
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
            // Debug.unityLogger.Log("MainServerLoop.GetWorldStatePackage");
            var gameObjects = GameObject.FindGameObjectsWithTag(Constants.DynamicTag);
            var worldData = new StateData()
            {
                worldState = gameObjects.Select(go => new WorldObject(go.name, go.transform)).ToArray()
            };
            return new StatePackage(worldData);
        }

        private async void SendWorldState(StatePackage pack)
        {
            await _multicastUdpClient.SendPackageAsync(pack);
        }
        
        private void OnDestroy()
        {
            ClientManager.instance.Dispose();
            HandlerManager.instance.Dispose();
            ConfigSave();
        }

        private void OnApplicationQuit()
        {
            ClientManager.instance.Dispose();
            HandlerManager.instance.Dispose();
            _multicastUdpClient.Dispose();
        }
    }

}
