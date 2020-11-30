using System;

namespace Net.Core
{
    public class EventBus: IDisposable
    {
        private static EventBus Instance = new EventBus();
        
        public StatePackageEvent updateWorldState = new StatePackageEvent();
        public PackageEvent newPackageRecieved = new PackageEvent();
        public PackageEvent sendBroadcast = new PackageEvent();
        public PackageEvent sendDecline = new PackageEvent();
        public PackageEvent sendAccept = new PackageEvent();

        public static EventBus getInstance()
        {
            return Instance;
        }
        
        
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
