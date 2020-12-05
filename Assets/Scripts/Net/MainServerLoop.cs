using System;
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
using Net.Utils;
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

        private void Awake()
        {
            _eventBus = EventBus.GetInstance();
            _servManager = ServerManager.GetInstance();
            _handlerManager = HandlerManager.GetInstance();
            //Config init
            
            //Events binding
            _eventBus.sendDecline.AddListener(ServerResponse.SendDecline);
            _eventBus.sendAccept.AddListener(ServerResponse.SendAccept);
        }

        // Use this for initialization
        private void Start()
        {
            Task.Run( async () =>
            {
                var waiter = new UdpSocket(new IPEndPoint(IPAddress.Any, Constants.ServerSendingPort),
                    Constants.ServerReceivingPort);
                Debug.Log($"waiting connection from anyone: {waiter.GetAddress()}:{Constants.ServerReceivingPort}");
                var res =  await waiter.ReceivePackageAsync();
                Debug.Log($"received package: {res.packageType}");
                EventBus.GetInstance().newPackageRecieved.Invoke(res);
            });
        }

        private void Update()
        {
            try
            {
                _servManager.ConnectedClients.ForEach(client => client.Update());
                Debug.unityLogger.Log($"Clients count: {_servManager.ConnectedClients.Count}");
                //Send WorldState to every client
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

        private void OnDestroy()
        {
            _servManager.Dispose();
            _eventBus.Dispose();
        }
    }

}
