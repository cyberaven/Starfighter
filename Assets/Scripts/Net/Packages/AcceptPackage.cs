using System;
using System.Net;
using Net.Interfaces;
using Net.Utils;

namespace Net.Packages
{
    [Serializable]
    public class AcceptPackage : IPackage
    {
        public PackageType PackageType => PackageType.AcceptPackage;

        public object Data => null;

        public IPEndPoint IpEndPoint { get; set; }


        public AcceptPackage()
        { }
    }
}