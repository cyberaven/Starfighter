using System;
using System.Net;
using Net.Interfaces;
using Net.PackageData;
using Net.Utils;
using UnityEngine;

namespace Net.Packages
{
    [Serializable]
    public class StatePackage : IPackage
    {
        [SerializeField]
        private object data;
        
        [SerializeField]
        public PackageType PackageType => PackageType.EventPackage;

        public object Data
        {
            get => data as StateData;
            private set => data = value;
        }

        public IPAddress ipAddress { get; set; }


        public StatePackage(StateData data)
        {
            Data = data;
        }
    }
}
