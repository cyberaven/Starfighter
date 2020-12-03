using System;
using Net.Core;
using Net.Interfaces;
using Net.Utils;
using UnityEngine;

namespace Net.PackageHandlers.ServerHandlers
{
    public class HandlerManager: IDisposable
    {
        private static HandlerManager Instance = new HandlerManager();
        
        public static IPackageHandler ConnectHandler;
        public static IPackageHandler DisconnectHandler;
        public static IPackageHandler EventHandler;
        public static IPackageHandler StateHandler;

        private HandlerManager()
        {
            ConnectHandler = new ConnectPackageHandler();
            DisconnectHandler = new DisconnectPackageHandler();
            EventHandler = new EventPackageHandler();
            StateHandler = new StatePackageHandler();

            EventBus.getInstance().newPackageRecieved.AddListener(HandlePackage);
        }

        public static HandlerManager getInstance()
        {
            return Instance;
        }
        
        public async void HandlePackage(IPackage pack)
        {
            Debug.unityLogger.Log($"Server Gonna handle some packs! {pack.PackageType}");
            switch (pack.PackageType)
            {
                case PackageType.ConnectPackage:
                    await ConnectHandler.Handle(pack);
                    break;
                case PackageType.DisconnectPackage:
                    await DisconnectHandler.Handle(pack);
                    break;
                case PackageType.EventPackage:
                    await EventHandler.Handle(pack);
                    break;
                case PackageType.StatePackage:
                    await StateHandler.Handle(pack);
                    break;
                case PackageType.AcceptPackage:
                case PackageType.DeclinePackage:
                    //Предполагается, что этих пакетов не будет прилетать на сервер.
                    break;
            }
        }

        public void Dispose()
        {
            
        }
    }
}