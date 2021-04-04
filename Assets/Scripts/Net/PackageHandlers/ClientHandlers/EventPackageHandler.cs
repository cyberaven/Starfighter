using System;
using System.Threading.Tasks;
using Core;
using Net.Core;
using Net.Interfaces;
using Net.Packages;
using UnityEngine;
using Utils;
using EventType = Net.Utils.EventType;

namespace Net.PackageHandlers.ClientHandlers
{
    public class EventPackageHandler : IPackageHandler
    {
        public async Task Handle(AbstractPackage pack)
        {
            try
            {
                var eventPack = pack as EventPackage;
                Debug.unityLogger.Log($"Got the event: {eventPack.data.eventType.ToString()}");
                switch (eventPack.data.eventType)
                {
                    case EventType.InitEvent:
                        Debug.unityLogger.Log($"There are {eventPack.data.data} asteroids to spawn");
                        NetEventStorage.GetInstance().worldInit.Invoke((int) eventPack.data.data);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                Debug.unityLogger.LogException(ex);
            }
        }
    }
}