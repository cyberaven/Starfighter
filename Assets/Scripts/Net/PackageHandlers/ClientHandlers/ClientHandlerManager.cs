using System;
using Net.Core;
using Net.Packages;
using Net.Utils;
using UnityEngine;

namespace Net.PackageHandlers.ClientHandlers
{
    public class ClientHandlerManager: AbstractHandlerManager
    {
        private Guid _lastStateAction;
        public ClientHandlerManager()
        {
            NetEventStorage.GetInstance().newPackageRecieved.AddListener(HandlePackage);
            AcceptHandler = new AcceptPackageHandler();
            DeclineHandler = new DeclinePackageHandler();
            EventHandler = new EventPackageHandler();
            StateHandler = new StatePackageHandler();
        }

        public override async void HandlePackage(AbstractPackage pack)
        {
            Debug.unityLogger.Log($"Client Gonna handle some packs! {pack.packageType}");
            switch (pack.packageType)
            {
                case PackageType.AcceptPackage:
                    await AcceptHandler.Handle(pack);
                    break;
                case PackageType.DeclinePackage:
                    await DeclineHandler.Handle(pack);
                    break;
                case PackageType.EventPackage:
                    await EventHandler.Handle(pack);
                    break;
                case PackageType.StatePackage:
                    await StateHandler.Handle(pack);
                    break;
                case PackageType.ConnectPackage:
                case PackageType.DisconnectPackage:
                    //Предполагается, что этих пакетов не будет прилетать на клиент.
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}