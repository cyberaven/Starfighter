using System;
using Core;

namespace Net.Core
{
    public class EventBus : Singleton<EventBus>, IDisposable
    {
        public StatePackageEvent updateWorldState = new StatePackageEvent();
        public PackageEvent newPackageRecieved = new PackageEvent();
        public PackageEvent sendBroadcast = new PackageEvent();
        public PackageEvent sendDecline = new PackageEvent();
        public PackageEvent sendAccept = new PackageEvent();

        public void Dispose()
        {
            updateWorldState.RemoveAllListeners();
            newPackageRecieved.RemoveAllListeners();
            sendBroadcast.RemoveAllListeners();
            sendDecline.RemoveAllListeners();
            sendAccept.RemoveAllListeners();
        }
    }
}
