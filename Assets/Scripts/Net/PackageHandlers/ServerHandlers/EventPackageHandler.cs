using System;
using System.Threading.Tasks;
using Net.Core;
using Net.Interfaces;
using Net.PackageData.EventsData;
using Net.Packages;
using UnityEngine;
using EventType = Net.Utils.EventType;

namespace Net.PackageHandlers.ServerHandlers
{
    public class EventPackageHandler : IPackageHandler
    {
        public async Task Handle(AbstractPackage pack)
        {
            var eventPack = pack as EventPackage;
            switch (eventPack.data.eventType)
            {
                case EventType.MoveEvent:
                    var movement = (MovementEventData)eventPack.data.data;
                    NetEventStorage.GetInstance().serverMovedPlayer.Invoke(pack.ipAddress, movement);
                    break;
                case EventType.DockEvent:
                    var data = eventPack.data.data.ToString();
                    Debug.unityLogger.Log($"Got the event: {data}");
                    break;
                case EventType.FireEvent:
                    break;
                case EventType.OtherEvent:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}