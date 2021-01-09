using System;
using System.Net;
using Client;
using UnityEngine;
using UnityEngine.Events;

namespace Net.Core
{
    public class EventBus: IDisposable
    {
        private static EventBus Instance = new EventBus();
        
        public StatePackageEvent updateWorldState = new StatePackageEvent();
        public PackageEvent newPackageRecieved = new PackageEvent();
        public ConnectPackageEvent addClient = new ConnectPackageEvent();
        public PlayerMovementEvent serverMovePlayer = new PlayerMovementEvent();
        public static EventBus GetInstance()
        {
            return Instance;
        }
        
        
        public void Dispose()
        {
            updateWorldState.RemoveAllListeners();
            newPackageRecieved.RemoveAllListeners();
        }
    }
}
