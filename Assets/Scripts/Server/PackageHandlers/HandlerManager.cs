using UnityEngine;
using UnityEditor;
using Server.Interfaces;
using Server.PackageHandlers;
using Server.Core;
using System.Threading.Tasks;

public class HandlerManager : ScriptableObject
{
    public static IPackageHandler ConnectHandler;
    public static IPackageHandler DisconnectHandler;
    public static IPackageHandler EventHandler;
    public static IPackageHandler StateHandler;

    public void Awake()
    {
        ConnectHandler = new ConnectPackageHandler();
        DisconnectHandler = new DisconnectPackageHandler();
        EventHandler = new EventPackageHandler();
        StateHandler = new StatePackageHandler();

        EventBus.Instance.newPackageRecieved.AddListener(HandlePackage);
    }

    public static async void HandlePackage(IPackage pack)
    {
        switch (pack.PackageType)
        {
            case Server.Utils.Enums.PackageType.ConnectPackage:
                await ConnectHandler.Handle(pack);
                break;
            case Server.Utils.Enums.PackageType.DisconnectPackage:
                await DisconnectHandler.Handle(pack);
                break;
            case Server.Utils.Enums.PackageType.EventPackage:
                await EventHandler.Handle(pack);
                break;
            case Server.Utils.Enums.PackageType.StatePackage:
                await StateHandler.Handle(pack);
                break;
            case Server.Utils.Enums.PackageType.AcceptPackage:
            case Server.Utils.Enums.PackageType.DeclinePackage:
                //TODO: Предполагается, что этих пакетов не будет прилетать на сервер.
                break;
        }
    }
}