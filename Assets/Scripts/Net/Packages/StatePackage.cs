using System;
using System.Net;
using Net.Interfaces;
using Net.PackageData;
using Net.Utils;
using UnityEngine;

namespace Net.Packages
{
    [Serializable]
    public class StatePackage : AbstractPackage
    {
        public new StateData data
        {
            get => base.data as StateData;
            private set => base.data = value;
        }
        
        public StatePackage(StateData data): base(data, PackageType.StatePackage)
        { }
    }
}
