using System;
using System.Net;
using Net.Interfaces;
using Net.PackageData;
using Net.Utils;

namespace Net.Packages
{
    [Serializable]
    public class StatePackage : IPackage
    {
        public PackageType PackageType => PackageType.EventPackage;

        public object Data
        {
            get => Data as StateData;
            private set => Data = value;
        }

        public IPEndPoint IpEndPoint { get; set; }


        public StatePackage(StateData data)
        {
            Data = data;
        }
    }
}
