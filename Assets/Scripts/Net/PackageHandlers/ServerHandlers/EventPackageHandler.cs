using System;
using System.Threading.Tasks;
using Client;
using Net.Core;
using Net.Interfaces;
using Net.PackageData.EventsData;
using Net.Packages;
using Net.Utils;

namespace Net.PackageHandlers.ServerHandlers
{
    public class EventPackageHandler : IPackageHandler
    {
        public async Task Handle(AbstractPackage pack)
        {
            //TODO: EventHandling
            var eventPack = pack as EventPackage;
            switch (eventPack.data.eventType)
            {
                case EventType.MoveEvent:
                    var movement = (MovementEventData)eventPack.data.data;
                    EventBus.GetInstance().serverMovePlayer.Invoke(pack.ipAddress, movement);
                    break;
                case EventType.DockEvent:
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