using System;
using Net.Utils;

namespace Net.PackageData
{
    [Serializable]
    public class ConnectData
    {
        public string login;
        public string password;
        public AccountType accountType;
    }
}
