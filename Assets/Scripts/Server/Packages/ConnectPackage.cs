using System;
using UnityEngine;
using Server.Utils.Enums;
using Server.Interfaces;
using Server.PackageData;
using System.Net;

namespace Server.Packages
{
    [Serializable]
    public class ConnectPackage : IPackage
    {
        public PackageType PackageType => PackageType.ConnectPackage;

        public object Data
        {
            get => Data as ConnectData; 
            private set => Data = value;
        }

        public IPEndPoint IpEndPoint { get; set; }


        public ConnectPackage(ConnectData data)
        {
            Data = data;
        }


    }
}