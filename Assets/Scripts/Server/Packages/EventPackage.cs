using System;
using UnityEngine;
using Server.Utils.Enums;
using Server.Interfaces;
using Server.PackageData;
using System.Net;

namespace Server.Packages
{
    [Serializable]
    public class EventPackage : IPackage
    {
        public PackageType PackageType => PackageType.EventPackage;

        public object Data
        {
            get => Data as EventData;
            private set => Data = value;
        }

        public IPEndPoint IpEndPoint { get; set; }

        public EventPackage(EventData data)
        {
            Data = data;
        }
    }
}