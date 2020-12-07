using System;
using System.Net;
using Net.Interfaces;
using Net.PackageData;
using Net.Utils;
using UnityEngine;

namespace Net.Packages
{
    [Serializable]
    public class DeclinePackage : AbstractPackage
    {
        public new DeclineData data
        {
            get => base.data as DeclineData; 
            private set => base.data = value;
        }
        public DeclinePackage(DeclineData data): base(data, PackageType.DeclinePackage)
        { }
    }
}