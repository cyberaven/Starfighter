using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;
using Core;
using Net.Core;
using Net.Interfaces;
using Net.PackageHandlers;
using Net.Utils;

namespace Net.PackageHandlers
{
    public class HandlerManager: Singleton<HandlerManager>
    {
        public static IPackageHandler ConnectHandler;
        public static IPackageHandler DisconnectHandler;
        public static IPackageHandler EventHandler;
        public static IPackageHandler StateHandler;

        public HandlerManager()
        {
            ConnectHandler = new ConnectPackageHandler();
            DisconnectHandler = new DisconnectPackageHandler();
            EventHandler = new EventPackageHandler();
            StateHandler = new StatePackageHandler();

            EventBus.Instance.newPackageRecieved.AddListener(HandlePackage);
        }

        public async void HandlePackage(IPackage pack)
        {
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
                    //TODO: Предполагается, что этих пакетов не будет прилетать на сервер.
                    break;
            }
        }
    }
}