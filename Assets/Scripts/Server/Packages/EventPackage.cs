using System;
using UnityEngine;
using Server.Utils.Enums;
using Server.Interfaces;

namespace Server.Packages
{
    [Serializable]
    public class EventPackage : IPackage
    {
        public PackageType PackageType => PackageType.EventPackage;

        public object Data => Data as EventData;
    }
}