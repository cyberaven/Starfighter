using System;
using System.Net;
using Net.Interfaces;
using Net.PackageData;
using Net.Utils;
using UnityEngine;

namespace Net.Packages
{
    [Serializable]
    public class ConnectPackage : IPackage
    {
        [SerializeField]
        private object data;
        
        [SerializeField]
        public PackageType PackageType => PackageType.ConnectPackage;

        public object Data
        {
            get => data as ConnectData; 
            private set => data = value;
        }

        public IPAddress ipAddress { get; set; }


        public ConnectPackage(ConnectData data)
        {
            Data = data;
        }


    }
}