using System;
using System.Net;
using Net.Interfaces;
using Net.PackageData;
using Net.Utils;

namespace Net.Packages
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