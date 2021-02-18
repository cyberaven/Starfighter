using System;
using Net.PackageHandlers;
using Net.PackageHandlers.ClientHandlers;
using Net.PackageHandlers.ServerHandlers;
using UnityEngine;

namespace Net.Core
{
    public class ManagerStorage: MonoBehaviour, IDisposable
    {
        private static ManagerStorage _instance;
        
        [NonSerialized] public ClientManager ClientManager;
        [NonSerialized] public NetEventStorage NetEventStorage;
        [NonSerialized] public AbstractHandlerManager HandlerManager;

        [SerializeField] private bool isServer;

        
        public ManagerStorage GetInstance()
        {
            if (_instance is null)
            {
                _instance = GetComponent<ManagerStorage>() ?? gameObject.AddComponent<ManagerStorage>();
            }

            return _instance;
        }
        
        private void Start()
        {
            NetEventStorage = NetEventStorage.GetInstance();
            
            if (isServer)
            {
                ClientManager = GetComponent<ClientManager>()?? gameObject.AddComponent<ClientManager>();
                HandlerManager = GetComponent<HandlerManager>()?? gameObject.AddComponent<HandlerManager>();
            }
            else
            {
                HandlerManager = GetComponent<ClientHandlerManager>()?? gameObject.AddComponent<ClientHandlerManager>();
            }
        }

        public void Dispose()
        {
            ClientManager.Dispose();
            NetEventStorage.Dispose();
        }
    }
}