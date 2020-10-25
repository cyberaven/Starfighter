using UnityEngine;
using System.Collections;
using Server.Interfaces;
using Server.Utils.Enums;
using System;
using Server.PackageData;
using System.Net;

namespace Server.Packages
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
