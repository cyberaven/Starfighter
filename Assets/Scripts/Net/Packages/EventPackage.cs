using System;
using System.Net;
using Net.Interfaces;
using Net.PackageData;
using Net.Utils;
using UnityEngine;

namespace Net.Packages
{
    [Serializable]
    public class EventPackage : AbstractPackage
    {
        public new EventData data
        {
            get => base.data as EventData;
            private set => base.data = value;
        }

        public EventPackage(EventData data) : base(data, PackageType.EventPackage)
        { }
    }
}