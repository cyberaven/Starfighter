using System;
using Net.PackageData;
using Net.Utils;

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
