using System;
using System.Net;
using Net.Interfaces;
using Net.PackageData;
using Net.Utils;
using UnityEngine;

namespace Net.Packages
{
    [Serializable]
    public class ConnectPackage : AbstractPackage
    {
        public new ConnectData data
        {
            get => base.data as ConnectData; 
            private set => base.data = value;
        }
        
        public ConnectPackage(ConnectData data): base(data, PackageType.ConnectPackage)
        {  }


    }
}