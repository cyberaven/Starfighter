using System;
using System.Net;
using Net.Interfaces;
using Net.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Net.Packages
{
    [Serializable]
    public abstract class AbstractPackage
    {
        public IPAddress ipAddress;
        public PackageType packageType;
        [SerializeField]
        protected object data;

        public AbstractPackage(object data, PackageType type)
        {
            this.data = data;
            packageType = type;
        }
    }
}