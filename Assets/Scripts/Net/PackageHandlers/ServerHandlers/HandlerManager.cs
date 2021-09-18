using System;
using Net.Core;
using Net.Packages;
using Net.Utils;
using UnityEngine;

namespace Net.PackageHandlers.ServerHandlers
{
    public class HandlerManager: AbstractHandlerManager
    {
        private Guid _lastStateAction;
        public HandlerManager()
        {
            NetEventStorage.GetInstance().newPackageRecieved.AddListener(HandlePackage);
            
            ConnectHandler = new ConnectPackageHandler();
            DisconnectHandler = new DisconnectPackageHandler();
            EventHandler = new EventPackageHandler();
            StateHandler = new StatePackageHandler();
        }

        public override async void HandlePackage(AbstractPackage pack)
        {
            // Debug.unityLogger.Log($"Server Gonna handle some packs! {pack.packageType}");
            switch (pack.packageType)
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void AddToPendingList(AbstractPackage pack)
        {  }
    }
}