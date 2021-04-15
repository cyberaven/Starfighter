using System;
using Enums;

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
