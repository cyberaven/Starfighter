using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Server.Interfaces;
using Server.Packages;
using Server.PackageData;
using UnityEngine.Events;
using Core;

namespace Server.Core
{
    public class EventBus : Singleton<EventBus>
    {
        public StatePackageEvent updateWorldState = new StatePackageEvent();
        public PackageEvent newPackageRecieved = new PackageEvent();
        public PackageEvent sendBroadcast = new PackageEvent();
    }
}
