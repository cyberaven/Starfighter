using System;
using System.Net;
using System.Runtime.CompilerServices;
using Client;
using UnityEngine;
using UnityEngine.Events;

namespace Net.Core
{
    public class NetEventStorage: IDisposable
    {
        private static NetEventStorage _instance;

        public StatePackageEvent updateWorldState = new StatePackageEvent();
        public PackageEvent newPackageRecieved = new PackageEvent();
        public PlayerMovementEvent serverMovedPlayer = new PlayerMovementEvent();
        public ClientEvent disconnectClient = new ClientEvent();
        public StarfighterUdpClientEvent sendMoves = new StarfighterUdpClientEvent();

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
        }
    }
}
