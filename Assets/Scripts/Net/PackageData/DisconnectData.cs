using System;
using Core;

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
