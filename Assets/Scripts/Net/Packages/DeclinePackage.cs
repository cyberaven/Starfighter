using System;
using System.Net;
using Net.Interfaces;
using Net.Utils;

namespace Net.Packages
{
    [Serializable]
    public class DeclinePackage : IPackage
    {
        public PackageType PackageType => PackageType.DeclinePackage;

        public object Data => null;

        public IPEndPoint IpEndPoint { get; set; }


        public DeclinePackage()
        { }
    }
}