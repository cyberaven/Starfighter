using System;
using UnityEngine;
using Server.Utils.Enums;
using Server.Interfaces;
using Server.PackageData;

namespace Server.Packages
{
    [Serializable]
    public class ConnectPackage : IPackage
    {
        public PackageType PackageType => PackageType.ConnectPackage;

        public object Data => Data as ConnectData;
    }
}