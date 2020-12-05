using System;
using System.Net;
using Net.Interfaces;
using Net.Utils;
using UnityEngine;

namespace Net.Packages
{
    [Serializable]
    public class DeclinePackage : AbstractPackage
    {
        public new object data => null;

        public DeclinePackage(): base(null, PackageType.DeclinePackage)
        { }
    }
}