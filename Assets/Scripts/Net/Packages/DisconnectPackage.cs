using System;
using Net.PackageData;
using Net.Utils;

namespace Net.Packages
{
    [Serializable]
    public class DisconnectPackage : AbstractPackage
    {
        public new DisconnectData data
        {
            get => base.data as DisconnectData; 
            private set => base.data = value;
        }

        public DisconnectPackage(DisconnectData data): base(data, PackageType.DisconnectPackage)
        { }


    }
}