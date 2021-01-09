using System;
using System.Net;
using Net.Utils;
using UnityEngine;

namespace Net.Packages
{
    [Serializable]
    public abstract class AbstractPackage
    {
        public Guid id;
        public IPAddress ipAddress;
        public PackageType packageType;
        
        [SerializeField]
        protected object data;

        public AbstractPackage(object data, PackageType type)
        {
            this.data = data;
            packageType = type;
            id = Guid.NewGuid();
        }
    }
}