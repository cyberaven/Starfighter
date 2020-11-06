using System;
using System.Net;
using Net.Interfaces;
using Net.PackageData;
using Net.Utils;

namespace Net.Packages
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