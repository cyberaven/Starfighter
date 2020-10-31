using System;
using UnityEngine;
using Server.Utils.Enums;
using Server.Interfaces;
using Server.PackageData;
using System.Net;

namespace Server.Packages
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