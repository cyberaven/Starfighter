using System;
using Net.Core;
using Net.Interfaces;
using Net.Packages;
using Net.Utils;
using UnityEngine;

namespace Net.PackageHandlers.ClientHandlers
{
    public class ClientHandlerManager: IDisposable
    {
        private static ClientHandlerManager Instance = new ClientHandlerManager();
        
        public static IPackageHandler AcceptHandler;
        public static IPackageHandler DeclineHandler;
        public static IPackageHandler EventHandler;
        public static IPackageHandler StateHandler;

        private ClientHandlerManager()
        {
            AcceptHandler = new AcceptPackageHandler();
            DeclineHandler = new DeclinePackageHandler();
            EventHandler = new EventPackageHandler();
            StateHandler = new StatePackageHandler();

            EventBus.GetInstance().newPackageRecieved.AddListener(HandlePackage);
        }

        public static ClientHandlerManager GetInstance()
        {
            return Instance;
        }
        
        public async void HandlePackage(AbstractPackage pack)
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

        public void Dispose()
        {
            
        }
    }
}