using System;
using System.Collections.Generic;
using UnityEngine;
using Server.Utils.Enums;
using System.Net;

namespace Server.Interfaces
{
    public interface IPackage
    {
        IPEndPoint IpEndPoint { get; set; }
        PackageType PackageType { get; }
        object Data { get; }
    }
}