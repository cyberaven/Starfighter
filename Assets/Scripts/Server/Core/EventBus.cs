using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Server.Interfaces;
using Server.Packages;
using Server.PackageData;
using UnityEngine.Events;
using Core;
using System;

namespace Server.Core
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
