using System;
using Core;

namespace Net.Core
{
    public class NetEventStorage: IDisposable
    {
        private static NetEventStorage _instance;

        public StatePackageEvent updateWorldState = new StatePackageEvent();
        public PackageEvent newPackageRecieved = new PackageEvent();
        public PlayerMovementEvent serverMovedPlayer = new PlayerMovementEvent();
        public DisconnectPackageEvent disconnectClient = new DisconnectPackageEvent();
        public ConnectPackageEvent connectClient = new ConnectPackageEvent();
        public ConnectPackageEvent connectToServer = new ConnectPackageEvent();
        public IntEvent worldInit = new IntEvent();
        public ClientEvent worldInitDone = new ClientEvent();
        public WayPointEvent wayPointSetted = new WayPointEvent();
        public PackageEvent dockEvent = new PackageEvent();
        public PackageEvent acceptPackageRecieved = new PackageEvent();
        public PackageEvent declinePackageRecieved = new PackageEvent();
        

        public static NetEventStorage GetInstance()
        {
            return _instance ?? (_instance = new NetEventStorage());
        }

        public void Dispose()
        {
            updateWorldState.RemoveAllListeners();
            newPackageRecieved.RemoveAllListeners();
            serverMovedPlayer.RemoveAllListeners();
            disconnectClient.RemoveAllListeners();
            connectClient.RemoveAllListeners();
            worldInit.RemoveAllListeners();
            worldInitDone.RemoveAllListeners();
            wayPointSetted.RemoveAllListeners();
            acceptPackageRecieved.RemoveAllListeners();
            declinePackageRecieved.RemoveAllListeners();
        }
    }
}
