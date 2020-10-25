using UnityEngine;
using System.Collections;
using Server.Interfaces;
using Server.Utils.Enums;
using System;
using Server.PackageData;

namespace Server.Packages
{
    [Serializable]
    public class StatePackage : IPackage
    {
        public PackageType PackageType => PackageType.StatePackage;

        public object Data => Data as StateData;
    }
}
