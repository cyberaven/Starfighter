using System;
using System.Net;
using Net.Interfaces;
using Net.Utils;
using UnityEngine;

namespace Net.Packages
{
    [Serializable]
    public class AcceptPackage : AbstractPackage
    {
        public new object data = null;
        public AcceptPackage() : base(null, PackageType.AcceptPackage)
        { }
    }
}