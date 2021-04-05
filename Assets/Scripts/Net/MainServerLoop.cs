using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Core;
using Core.InputManager;
using Net.Core;
using Net.PackageData;
using Net.PackageHandlers.ServerHandlers;
using Net.Packages;
using Net.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Net
{
    [RequireComponent(typeof(ClientManager))]
    [RequireComponent(typeof(HandlerManager))]
    [RequireComponent(typeof(ServerInitializeHelper))]
    [RequireComponent(typeof(InputManager))]
    public class MainServerLoop : Singleton<MainServerLoop>
    {
        private StarfighterUdpClient _multicastUdpClient;

        private Coroutine currentCoroutine, previousCoroutine;
        
        private new void Awake()
        {
            base.Awake();
            
            NetEventStorage.GetInstance().updateWorldState.AddListener(SendWorldState);
        }

        private void ConfigInit()
        {
            StartCoroutine(ServerInitializeHelper.instance.InitServer());
            var initCoroutine = StartCoroutine(Importer.AddAsteroidsOnScene(Importer.ImportAsteroids(Constants.PathToAsteroids)));
        }

        private void ConfigSave()
        {
            ServerInitializeHelper.instance.SaveServer();
        }
        
        private void Start()
        {
            ConfigInit();

            try
            {
                _multicastUdpClient = new StarfighterUdpClient(IPAddress.Parse(Constants.MulticastAddress),
                    Constants.ServerSendingPort, Constants.ServerReceivingPort);
                Debug.Log($"start waiting connection packs from anyone: {Constants.ServerReceivingPort}");
                _multicastUdpClient.BeginReceivingPackage();
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
            }
        }

        private void Update()
        {
            try
            {
                var collection = CollectWorldObjects();
                currentCoroutine = StartCoroutine(collection);
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
            }
        }

        public void LaunchCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
        
        private void FixedUpdate()
        {
            Dispatcher.Instance.InvokePending();
        }

        private IEnumerator CollectWorldObjects()
        {
            yield return previousCoroutine;
            previousCoroutine = currentCoroutine;
            var worldObjects = new List<WorldObject>();
            var allGameObjects = SceneManager.GetActiveScene().GetRootGameObjects()
                .Where(obj => obj.CompareTag(Constants.DynamicTag));
            foreach (var go in allGameObjects)
            {
                worldObjects.Add(new WorldObject(go.name, go.transform));
                yield return null;
            }

            var statePackage = new StatePackage(new StateData()
            {
                worldState = worldObjects.ToArray()
            });
            
            NetEventStorage.GetInstance().updateWorldState.Invoke(statePackage);
        }
        
        private async void SendWorldState(StatePackage pack)
        {
            foreach (var client in ClientManager.instance.ConnectedClients)
            {
                await client.SendWorldState(pack.data);
            }

            // var result = await _multicastUdpClient.SendPackageAsync(pack);
        }
        
        private void OnDestroy()
        {
            ClientManager.instance.Dispose();
            HandlerManager.instance.Dispose();
        }

        private void OnApplicationQuit()
        {
            ConfigSave();
            ClientManager.instance.Dispose();
            HandlerManager.instance.Dispose();
            _multicastUdpClient.Dispose();
        }
    }

}
