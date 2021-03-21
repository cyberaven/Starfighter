using System;
using Core;
using Net.Utils;

namespace Net.PackageData
{
    [Serializable]
    public class DisconnectData
    {
        public string login;
        public string password;
        public UserType accountType;
    }
}
